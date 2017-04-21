using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagBehaviour : MonoBehaviour
{
    public const float timeToStealFlag = 2.0f;

    public GameObject ImpulsePrefab;

    [HideInInspector] public CharacterV3 targetPlayer;

    [HideInInspector] public List<PlayerInfo> PlayersInCollider = new List<PlayerInfo>();

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
            if(PlayersInCollider[i].TimeInCollider >= timeToStealFlag)
            {
                setPlayerOwner(PlayersInCollider[i].playerTransform);
            }
        }


        if (targetPlayer == null) return;

        transform.position = targetPlayer.transform.position;
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
