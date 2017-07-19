using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EsqueletoArmado : IEnemy {

    private Vector2 moveDirection, moveTarget;
    private float moveDistance, startTime, journeyLength;
    private bool canMove;

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private float aggroRange, maxMoveDistance, moveSpeed, timeBetweenMoves;
    private float timer;
    private bool aggro;

	// Use this for initialization
	void Start () {
        currentHP = maxHP;
        aggro = false;
        canMove = false;
        timer = 0;
	}
	
	// Update is called once per frame
	void Update () {
        aggro = (Mathf.Abs((transform.position - player.transform.position).magnitude) <= aggroRange);

        if (!aggro)
        {
            Wander();
        }
        else
        {
            Attack();
        }       
	}

    private void Attack()
    {

    }

    private void Wander()
    {
        timer += Time.deltaTime;
        if (timer >= timeBetweenMoves)
        {
            timer = 0;
            PickDirection();
        }

        if (canMove)
        {
            Move();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Weapon"))
        {
            canMove = false;
            TakeDamage(collision.gameObject, collision.gameObject.GetComponent<IWeapon>().GetKnockback(), collision.gameObject.GetComponent<IWeapon>().GetWeaponDamage());

            if (collision.gameObject.name == "PlayerArrow(Clone)")
            {
                GameObject.Destroy(collision.gameObject);
            }
        }
    }

    void PickDirection()
    {
        moveDirection = Random.insideUnitCircle;
        moveDistance = Random.Range(maxMoveDistance/2, maxMoveDistance);

        RaycastHit2D check;
        check = Physics2D.Raycast(transform.position, moveDirection, moveDistance);
        if(check.collider != null)
        {
            if(check.collider.gameObject.tag != "Obstacle" && check.collider.gameObject.tag != "Wall")
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
