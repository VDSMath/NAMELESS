using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour,IWeapon {
    private int shootingDamage;
   
    [SerializeField]
    private float knockbackForce;
    [SerializeField]
    private int damage;

    

	public void SetShootingDamage(int shootingD){
		shootingDamage = shootingD;
	}
	public int GetWeaponDamage(){
		return shootingDamage;
	}

    public float GetKnockback()
    {
        return knockbackForce;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            collision.gameObject.GetComponent<PlayerStatus>().TakeDamage(gameObject, knockbackForce, damage);
        }

        GameObject.Destroy(this.gameObject);
    }
}
