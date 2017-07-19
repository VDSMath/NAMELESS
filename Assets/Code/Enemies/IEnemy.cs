using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class IEnemy : MonoBehaviour {
    public int maxHP, currentHP;
    public int damage, knockback;

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
    }

    public void TakeDamage(GameObject dealer, float knockbackDistance, int damageAmount)
    {
        Vector2 knockbackDirection = dealer.transform.position - transform.position;
        GetComponent<Rigidbody2D>().AddForce(-knockbackDirection.normalized * knockbackDistance);
        currentHP -= damageAmount;

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
