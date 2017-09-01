using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [Header("Search Parameters")]
    [SerializeField]
    private float radius;
    [SerializeField]
    private LayerMask filter;

    private GameObject player;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
        Search();   
	}

    void Search()
    {
        RaycastHit2D check = Physics2D.Raycast(transform.position, player.transform.position - transform.position, radius, filter);

        Debug.Log(check.transform.gameObject);
    }

    

    
}
