using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleReactorManager : MonoBehaviour
{

    private CharacterV3 characterv3;
    private ControllerV3 controlerv3;
    private ParticleSystem reactor;
    private ParticleSystem.EmissionModule reactorEmission;

    void Awake ()
    {
        characterv3 = GetComponentInParent<CharacterV3>();
        controlerv3 = transform.parent.parent.GetComponentInChildren<ControllerV3>();
        reactor = transform.Find("reactor-particle").GetComponent<ParticleSystem>();
        reactorEmission = reactor.emission;
	}
	
	void Update ()
    {

        
        reactorEmission.enabled = Input.GetAxisRaw(controlerv3.Get_AccelAxisInput()) != 0;

	}
}
