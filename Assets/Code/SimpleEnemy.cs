using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemy : MonoBehaviour {

    public float moveMaxSpeed;

    public float knockbackDistance,
                 maxHP;
    private float currentHP;

    public GameObject target;
    public AudioSource oh;

	// Use this for initialization
	void Start () {
        currentHP = maxHP;
	}
	
	// Update is called once per frame
	void Update () {
        FollowTarget();
	}
    void FollowTarget()
    {
        LookAt2D();      
	    transform.eulerAngles = new Vector3(0,0, transform.eulerAngles.z);
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, moveMaxSpeed);
    }

    private void LookAt2D()
    {
        Vector3 diff = target.transform.position - transform.position;
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            GameObject.Destroy(collision.gameObject);
            TakeDamage(target.GetComponent<PC>().bulletDamage, knockbackDistance * 0.1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Sword")
        {
            TakeDamage(target.GetComponent<PC>().swordDamage, knockbackDistance * 10f	);
        }
    }

    private void TakeDamage(float amount, float knockback)
    {
        Vector2 knockbackV = -transform.up.normalized;
        Debug.Log(knockbackV);
        GetComponent<Rigidbody2D>().AddForce(knockbackV * knockback);
        currentHP -= amount;

        if(currentHP <= 0)
        {
            Kill();
        }
    }

    private void Kill()
    {

        oh.Play();
        gameObject.SetActive(false);
    }
}
