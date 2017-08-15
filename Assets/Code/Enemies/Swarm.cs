using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swarm : IEnemy {

    private GameObject player;
    private bool swarming;
    private float timer;
    [SerializeField]
    private float speed, timeBetweenHits;

	// Use this for initialization
	void Start () {
        currentHP = maxHP;
        player = GameObject.Find("Player");
        timer = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (swarming)
        {
            SwarmDamage();
        }
        else
        {
            timer = 0;
            Follow();
        }
	}

    void Follow()
    {
        float originalZ = transform.position.z;

        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed);
        transform.position = new Vector3(transform.position.x, transform.position.y, originalZ);
    }

    void SwarmDamage()
    {
        transform.position = player.transform.position;
        timer += Time.deltaTime;

        if (timer >= timeBetweenHits)
        {
            timer = 0;
            player.GetComponent<PlayerStatus>().TakeDamage(gameObject, knockback, damage);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            swarming = true;
        }

        if (collision.gameObject.CompareTag("Weapon"))
        {
            swarming = false;
            TakeDamage(collision.gameObject, collision.gameObject.GetComponent<IWeapon>().GetKnockback(), collision.gameObject.GetComponent<IWeapon>().GetWeaponDamage());

            if (collision.gameObject.name == "PlayerArrow(Clone)")
            {
                GameObject.Destroy(collision.gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            swarming = false;
        }
    }
}
