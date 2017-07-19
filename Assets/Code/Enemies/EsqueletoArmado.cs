using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EsqueletoArmado : MonoBehaviour {

    private Vector2 moveDirection, moveTarget;
    private float moveDistance, startTime, journeyLength;
    private bool canMove;

    [SerializeField]
    private float maxMoveDistance, moveSpeed, timeBetweenMoves;
    private float timer;

	// Use this for initialization
	void Start () {
        canMove = false;
        timer = 0;
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if(timer >= timeBetweenMoves)
        {
            timer = 0;
            PickDirection();
        }

        if (canMove)
        {
            Move();
        }
	}

    void PickDirection()
    {
        moveDirection = Random.insideUnitCircle;
        moveDistance = Random.Range(maxMoveDistance/2, maxMoveDistance);

        RaycastHit check;

        if(Physics.Raycast(transform.position, moveDirection, out check, moveDistance))
        {
            if(check.transform.gameObject.tag == "Obstacle" || check.transform.gameObject.tag == "Wall")
            {
                startTime = Time.time;
                moveTarget = (Vector2)transform.position + (moveDirection * moveDistance);
                journeyLength = Vector3.Distance(transform.position, moveTarget);
                canMove = true;
            }
        }

        else
        {
            startTime = Time.time;
            moveTarget = (Vector2)transform.position + (moveDirection * moveDistance);
            journeyLength = Vector3.Distance(transform.position, moveTarget);
            canMove = true;
        }
    }

    void Move()
    {
        float distCovered = (Time.time - startTime) * moveSpeed;
        float fracJourney = distCovered / journeyLength;
        transform.position = Vector3.Lerp(transform.position, moveTarget, fracJourney);

        if(fracJourney >= 1)
        {
            canMove = false;
        }
    }
}
