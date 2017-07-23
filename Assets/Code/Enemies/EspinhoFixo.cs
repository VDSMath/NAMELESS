using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EspinhoFixo : MonoBehaviour {

    [SerializeField]
    private float knockback;
    [SerializeField]
    private int damage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            collision.gameObject.GetComponent<PlayerStatus>().TakeDamage(gameObject, knockback, damage);
        }
    }
}
