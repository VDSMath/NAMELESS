using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GosmaVermelha : MonoBehaviour {

    
    [SerializeField]private float moveDistance, knockbackDistance, timeBetweenCrawls;
    [SerializeField]private GameObject target;
    [SerializeField]
    private int damage;


    private float timer;

    public float maxHP;
    [SerializeField]private float currentHP;

	// Use this for initialization
	void Start () {
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
        if (collision.gameObject.tag == "Bullet")
        {
            GameObject.Destroy(collision.gameObject);
            TakeDamage(target.GetComponent<PC>().bulletDamage, knockbackDistance * 0.1f);
        }

        if(collision.gameObject.name == "Player")
        {
            collision.gameObject.GetComponent<PlayerStatus>().TakeDamage(gameObject, knockbackDistance, damage);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Weapon"))
        {
            TakeDamage(collision.gameObject.GetComponent<IWeapon>().GetWeaponDamage(), knockbackDistance * 10f);
        }
    }

    private void TakeDamage(float amount, float knockback)
    {
        Vector2 knockbackV = -transform.up.normalized;
        GetComponent<Rigidbody2D>().AddForce(knockbackV * knockback);
        currentHP -= amount;

        if (currentHP <= 0)
        {
            Kill();
        }
    }

    private void Kill()
    {
        gameObject.SetActive(false);
    }
}
