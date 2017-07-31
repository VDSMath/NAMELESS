using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour {

	[Header("Visual Objects")]
	[SerializeField] private GameObject head;
	[SerializeField] private GameObject body;

	[Header("Movement Properties")]
	public float walkSpeed;
	[SerializeField] private float linearDrag;
	[SerializeField] private float dashDistance;
	public Vector2 lastDirection;

	private Rigidbody2D myRB2D;
	private PlayerStatus myPS;

    [HideInInspector]
    public bool canMove;

	private void Start(){
        canMove = true;
		GetRigidbody();
		GetPlayerStatus();
		UsePhysicsProperties();
	}
	private void FixedUpdate(){
        if (canMove)
        {
            Walking();
            Dash();
            GetLastDirection();
        }
	}
	private void Walking(){
		Vector2 direction = new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical")).normalized;

		myRB2D.AddForce(direction*walkSpeed);
	}
	private void Dash(){
		if(Input.GetKeyDown("space") && myPS.CanAct()){
			Vector2 direction = new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical")).normalized;
			myRB2D.AddForce(direction*walkSpeed*dashDistance);
			myPS.LoseEnergy();
		}
	}
	private void GetRigidbody(){
		myRB2D = GetComponent<Rigidbody2D>();
	}
	private void GetPlayerStatus(){
		myPS = GetComponent<PlayerStatus>();
	}
	private void UsePhysicsProperties(){
		myRB2D.drag = linearDrag;
	}
	public Vector2 GetLastDirection(){
		if(new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical")).magnitude != 0){
			lastDirection = new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical")).normalized;
		}
		return lastDirection;
	}
}
