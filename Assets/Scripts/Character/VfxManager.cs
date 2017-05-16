using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VfxManager : MonoBehaviour
{

    public CharacterV3 cv3;

    public Transform reactor_small_1;
    public Transform reactor_small_2;
    Renderer[] reactor_smalls_renderers;

    public Transform reactor_big;
    Renderer[] reactor_big_renderers;

    public Transform boostFx;
    Renderer[] turbo_renderers;

    float albedoAlpha = 0;
    float emissiveAlpha = 0;

    float ColorLerpSpeed = 5;

    // Use this for initialization
    void Awake ()
    {
        reactor_smalls_renderers = new Renderer[4];

        Renderer[] rend = reactor_small_1.GetComponentsInChildren<Renderer>();
        reactor_smalls_renderers[0] = rend[0]; reactor_smalls_renderers[1] = rend[1];
        rend = reactor_small_2.GetComponentsInChildren<Renderer>();
        reactor_smalls_renderers[2] = rend[0]; reactor_smalls_renderers[3] = rend[1];

        reactor_big_renderers = reactor_big.GetComponentsInChildren<Renderer>();
        turbo_renderers = boostFx.GetComponentsInChildren<Renderer>();

        albedoAlpha = reactor_big_renderers[0].material.color.a;
        emissiveAlpha = reactor_big_renderers[0].material.GetColor("_EmissionColor").a;
    }

    // Update is called once per frame
    void Update ()
    {
        float speedRatio = cv3.getSpeedRatio();
        bool useTurbo = cv3.previous_I_forwardBoost == 1;
        float flickerSize = 0.5f * speedRatio;

        float small_Reactor_sizeX = Mathf.Lerp(0.1f, 1.5f, speedRatio);
        reactor_small_1.transform.localScale = new Vector3(reactor_small_1.transform.localScale.x, reactor_small_1.transform.localScale.y, small_Reactor_sizeX + Random.Range(-flickerSize, flickerSize));
        reactor_small_2.transform.localScale = new Vector3(reactor_small_2.transform.localScale.x, reactor_small_2.transform.localScale.y, small_Reactor_sizeX + Random.Range(-flickerSize, flickerSize));

        Color albedoColor = Color.Lerp(reactor_big_renderers[0].material.color, (useTurbo)?Color.red:Color.blue, Time.deltaTime * ColorLerpSpeed);
        Color emissiveColor = Color.Lerp(reactor_big_renderers[0].material.GetColor("_EmissionColor"), (useTurbo) ? Color.red : Color.blue, Time.deltaTime * ColorLerpSpeed);
        albedoColor.a = 0.5f;
        //emissiveColor.a = emissiveAlpha;

        for (int i = 0; i < reactor_smalls_renderers.Length; i++)
        {
            reactor_smalls_renderers[i].material.color = albedoColor;
            reactor_smalls_renderers[i].material.SetColor("_EmissionColor", albedoColor);

        }

        flickerSize = 3f * speedRatio;
        float big_reactor_size = Mathf.Lerp(0.1f, 8, speedRatio);
        reactor_big.transform.localScale = new Vector3(reactor_big.transform.localScale.x, reactor_big.transform.localScale.y, big_reactor_size + Random.Range(-flickerSize, flickerSize));

        for (int i = 0; i < reactor_big_renderers.Length; i++)
        {
            reactor_big_renderers[i].material.color = albedoColor;
            reactor_big_renderers[i].material.SetColor("_EmissionColor", albedoColor);

        }


        float fadeSpeed = 3;
        for (int i = 0; i < turbo_renderers.Length; i++)
        {
            Color c0 = turbo_renderers[i].materials[0].color;
            Color c1 = turbo_renderers[i].materials[1].color;
            
            c0.a = Mathf.Clamp(c0.a + Time.deltaTime * ((useTurbo) ? fadeSpeed : -fadeSpeed), 0, 92f / 255f);
            c1.a = Mathf.Clamp(c0.a + Time.deltaTime * ((useTurbo) ? fadeSpeed : -fadeSpeed), 0, 200f / 255f);

            turbo_renderers[i].materials[0].color = c0;
            turbo_renderers[i].materials[1].color = c1;
        }

    }
}
