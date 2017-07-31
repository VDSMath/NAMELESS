using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fantasma : IEnemy {

    private GameObject player;

    [SerializeField]
    private float followSpeed;
    [SerializeField]
    private float playerSpeedModifier;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
        currentHP = maxHP;
	}
	
	// Update is called once per frame
	void Update () {
        Move();
	}

    void Move()
    {
        float originalZ = transform.position.z;

        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, followSpeed);
        transform.position = new Vector3(transform.position.x, transform.position.y, originalZ);

        transform.position += new Vector3(0, Mathf.Sin(Time.time * 3)/120 ,0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Weapon"))
        {
            TakeDamage(collision.gameObject, collision.gameObject.GetComponent<IWeapon>().GetKnockback(), collision.gameObject.GetComponent<IWeapon>().GetWeaponDamage());

            if (collision.gameObject.name == "PlayerArrow(Clone)")
            {
                GameObject.Destroy(collision.gameObject);
            }
        }

        if (collision.gameObject.name == "Player")
        {
            collision.gameObject.GetComponent<PlayerMovement>().walkSpeed /= playerSpeedModifier;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            collision.gameObject.GetComponent<PlayerMovement>().walkSpeed *= playerSpeedModifier;
        }
    }
}
