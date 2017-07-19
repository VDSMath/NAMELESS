using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GosmaVermelha : IEnemy {

    
    [SerializeField]private float moveDistance, timeBetweenCrawls;
    [SerializeField]private GameObject target;


    private float timer;                        

	// Use this for initialization
	void Start () {
        target = GameObject.Find("Player");
        timer = 0;
        currentHP = maxHP;
	}
	
	// Update is called once per frame
	void Update () {
        Move();
	}

    void Move()
    {
        timer += Time.deltaTime;
        if(timer >= timeBetweenCrawls)
        {
            Crawl();
            timer = 0;
        }
    }

    private void Crawl()
    {
        Vector3 targetDirection = target.transform.position - transform.position;
        targetDirection = targetDirection.normalized;

        GetComponent<Rigidbody2D>().AddForce(targetDirection * moveDistance);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            collision.gameObject.GetComponent<PlayerStatus>().TakeDamage(gameObject, knockback, damage);
        }
    }     
}
