using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagBehaviour : MonoBehaviour
{
    public const float timeToStealFlag = 0.01f;
    public const float flagSizeIncrease = 3f;
    public const float distanceRegisterPos = 5f;

    public GameObject ImpulsePrefab;

    [HideInInspector] public CharacterV3 targetPlayer;

    [HideInInspector] public List<PlayerInfo> PlayersInCollider = new List<PlayerInfo>();

    Vector3 initialSize = Vector3.one;

    ParticleSystem trail;

    float timeSinceLastSteal = 0f;

    [HideInInspector] public List<Vector3> previousPos = new List<Vector3>(50);

    [HideInInspector] public Transform[] indicators;

    public AnimationCurve indicatorColorByDistance;
    public Gradient colorGradient;

    private void Awake()
    {
        initialSize = transform.localScale;
        trail = transform.Find("Trail").GetComponentInChildren<ParticleSystem>();
        indicators = new Transform[4] { transform.Find("flagModel").Find("Indicator-1"), transform.Find("flagModel").Find("Indicator-2"), transform.Find("flagModel").Find("Indicator-3"), transform.Find("flagModel").Find("Indicator-4") };
        ToggleIndicators();
    }

    private void Update()
    {
        if(previousPos.Count == 0 || Vector3.Distance(transform.position, previousPos[0]) > distanceRegisterPos)
        {
            RegisterPos();
        }
        UpdateIndicators();
    }

    private void LateUpdate()
    {
        for (int i = 0; i < PlayersInCollider.Count; i++)
        {
            if(PlayersInCollider[i].playerTransform == targetPlayer.transform)
            {
                PlayersInCollider.RemoveAt(i);
                continue;
            }

            PlayersInCollider[i].TimeInCollider += Time.deltaTime;
            if(PlayersInCollider[i].TimeInCollider >= timeToStealFlag && timeSinceLastSteal > 1.5f)
            {
                setPlayerOwner(PlayersInCollider[i].playerTransform);
            }
        }

        transform.localScale = initialSize;



        if (targetPlayer == null) return;

        timeSinceLastSteal += Time.deltaTime;

        transform.localScale = initialSize * flagSizeIncrease;

        transform.position = targetPlayer.transform.position;
        transform.rotation = targetPlayer.transform.rotation;
        targetPlayer.currentScore += targetPlayer.speedScoreGain * Time.deltaTime;    

    }


    private void OnTriggerEnter(Collider other)
    {
        
        if(other.tag == "PlayerCharacter")
        {
            if (targetPlayer == null)
            {
                setPlayerOwner(other.transform);
                return;
            }

            if(!isPlayerInList(other.transform))
            PlayersInCollider.Add(new PlayerInfo(other.transform));

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "PlayerCharacter")
        {
            if (targetPlayer == null)
            {
                return;
            }

            removePlayerFromList(other.transform);

        }
    }

    public void setPlayerOwner(Transform play)
    {
        if(targetPlayer != null)
            PlayersInCollider.Add(new PlayerInfo(targetPlayer.transform));

        targetPlayer = play.GetComponent<CharacterV3>();
        play.GetComponent<CharacterV3>().RefillBoost();
        removePlayerFromList(play);

        transform.position = targetPlayer.transform.position;

        Instantiate(ImpulsePrefab, transform.position, Quaternion.identity);
        timeSinceLastSteal = 0f;
        trail.Play();
        ToggleIndicators();
        SoundManager.instance.PlaySoundFlagCaptured();
    }

    public void removePlayerFromList(Transform play)
    {
        for (int i = 0; i < PlayersInCollider.Count; i++)
        {
            if (PlayersInCollider[i].playerTransform == play)
            {
                PlayersInCollider.RemoveAt(i);
                return;
            }
        }
    }

    public bool isPlayerInList(Transform play)
    {
        for (int i = 0; i < PlayersInCollider.Count; i++)
        {
            if(play == PlayersInCollider[i].playerTransform)
            {
                return true;
            }
        }
        return false;
    }

    public void Drop()
    {
        targetPlayer = null;
        trail.Stop();
        ToggleIndicators();
    }

    public void RegisterPos()
    {
        if (previousPos.Count >= previousPos.Capacity) previousPos.RemoveAt(previousPos.Count - 1);

        previousPos.Insert(0, transform.position);
    }

    private void OnDrawGizmosSelected()
    {
        if (targetPlayer == null) return;

        for (int i = 0; i < previousPos.Count; i++)
        {
            Gizmos.color = new Color(0, 1, 0, 0.2f);
            Gizmos.DrawSphere(previousPos[i], i );
        }
    }

    private void ToggleIndicators()
    {
        for (int i = 0; i < indicators.Length; i++)
        {
            if(targetPlayer == null)
            {
                indicators[i].gameObject.SetActive(false);
                continue;
            }

            if (i >= PlayerManager.get().playerNumber)
            {
                indicators[i].gameObject.SetActive(false);
                continue;
            }

            if (targetPlayer.controlerSet.playerNumero == i)
            {
                indicators[i].gameObject.SetActive(false);
            }
            else
            {
                indicators[i].gameObject.SetActive(true);
            }
        }
    }

    private void UpdateIndicators()
    {
        if (PlayerManager.get() == null || PlayerManager.get().characters == null) return;
        for (int i = 0; i < PlayerManager.get().characters.Length; i++)
        {
            if (PlayerManager.get().characters[i] == targetPlayer) continue;

            if (i >= PlayerManager.get().playerNumber) continue;

            indicators[i].LookAt(PlayerManager.get().characters[i].transform);

            indicators[i].GetComponentInChildren<Renderer>().material.color = colorGradient.Evaluate(indicatorColorByDistance.Evaluate(Vector3.Distance(transform.position, PlayerManager.get().characters[i].transform.position)));
        }
    }

}

[System.Serializable]
public class PlayerInfo
{
    public Transform playerTransform;
    public float TimeInCollider;

    public PlayerInfo(Transform tr)
    {
        playerTransform = tr;
        TimeInCollider = 0;
    }
}
