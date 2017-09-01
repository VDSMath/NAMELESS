using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour {

    public float walkSpeed; 
    private Rigidbody2D myRB2D;

    // Use this for initialization
    void Start () {
        myRB2D = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
        Walking();
    }

    private void Walking()
    {
        Vector2 direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        myRB2D.AddForce(direction * walkSpeed);
    }
}
