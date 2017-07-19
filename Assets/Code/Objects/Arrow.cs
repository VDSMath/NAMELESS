using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour,IWeapon {
	private float shootingDamage;

    [SerializeField]
    private float knockback;
    [SerializeField]
    private int damage;

	public void SetShootingDamage(float shootingD){
		shootingDamage = shootingD;
	}
	public float GetWeaponDamage(){
		return shootingDamage;
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            collision.gameObject.GetComponent<PlayerStatus>().TakeDamage(gameObject, knockback, damage);
        }
        if(collision.gameObject.tag == "Enemy")
        {

        }
    }
}
