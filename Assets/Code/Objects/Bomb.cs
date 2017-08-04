using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

    [SerializeField]
    private Sprite explosion;
    [SerializeField]
    private float  explosionKnockback, timeBeforeExplosion;
    [SerializeField]
    private Collider2D explosionCollider;
    [SerializeField]
    private int explosionDamage;

    public void Explode()
    {
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
        StartCoroutine(Countdown());
    }

     IEnumerator Countdown()
    {
        yield return new WaitForSeconds(timeBeforeExplosion);
        GetComponent<SpriteRenderer>().sprite = explosion;
        explosionCollider.enabled = true;
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            collision.gameObject.GetComponent<PlayerStatus>().TakeDamage(gameObject, explosionKnockback, explosionDamage);
        }

        if(collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<IEnemy>().TakeDamage(gameObject, explosionKnockback, explosionDamage);
        }
    }
}
