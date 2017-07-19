using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EspinhoRolador : IEnemy {

    public int tipo;

    [SerializeField]
    private float speed;

	// Use this for initialization
	void Start () {
        Vector2 frce;
        switch (tipo)
        {
            case 0:
                frce = new Vector2(0,1);
                break;
            case 1:
                frce = new Vector2(1, 0);
                break;
            case 2:
                frce = new Vector2(1,1);
                break;

            default:
                frce = Vector2.zero;
                break;
        }

        GetComponent<Rigidbody2D>().AddForce(frce * speed);
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            collision.gameObject.GetComponent<PlayerStatus>().TakeDamage(gameObject, knockback, damage);
        }
    }
}
