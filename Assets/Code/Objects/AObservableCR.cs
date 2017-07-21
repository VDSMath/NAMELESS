using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class AObservableCR : MonoBehaviour {

    private GameObject observer;

    [SerializeField]
    private float speed;

    private float journeyLength, startTime;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Observe(GameObject observerT)
    {
        observer = observerT;

    }
}
