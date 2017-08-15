using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EsqueletoArmado : IEnemy {

    private Vector2 moveDirection, moveTarget;
    private float moveDistance, startTime, journeyLength;
    private bool attacking, canMove;
    
    private GameObject player;

    [SerializeField]
    private GameObject sword;
    [SerializeField]
    private float aggroRange, delayBeforeAttack, maxMoveDistance, moveSpeed, timeBetweenMoves;
    private float timer;
    private bool aggro;

	// Use this for initialization
	void Start () {
        attacking = false;
        currentHP = maxHP;
        aggro = false;
        canMove = false;
        timer = 0;
        player = GameObject.Find("Player");
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
            if(Mathf.Abs((transform.position - player.transform.position).magnitude) <= 4f && !attacking)
            {
                attacking = true;
                StartCoroutine(Attack());
            }

            else
            {
                if(!attacking)
                    Follow();
            }
        }       
	}

    private IEnumerator Attack()
    {
        Color temp = GetComponent<SpriteRenderer>().color;
        GetComponent<SpriteRenderer>().color = Color.red;

        yield return new WaitForSeconds(delayBeforeAttack);
        GameObject s = Instantiate(sword);
        s.transform.position = transform.position - (transform.position - player.transform.position).normalized * 2;
        Destroy(s, .2f);

        GetComponent<SpriteRenderer>().color = temp;

        yield return new WaitForSeconds(1f);
        attacking = false;
    }

    void Follow()
    {
        float originalZ = transform.position.z;

        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed * 1.5f);
        transform.position = new Vector3(transform.position.x, transform.position.y, originalZ);
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
                moveTarget = (Vector2)transform.position + (moveDirection * (moveDistance - 1));
                canMove = true;
            }
        }

        else
        {
            moveTarget = (Vector2)transform.position + (moveDirection * moveDistance);
            canMove = true;
        }
    }

    void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, moveTarget, moveSpeed);

        if (Vector3.Distance(transform.position, moveTarget) <= 0.1f)
        {
            canMove = false;
            moveDirection = transform.position;
        }        
    }
}
