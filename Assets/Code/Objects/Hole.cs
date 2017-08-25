using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour {

    [SerializeField]
    private float fallScaleDifference;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            collision.gameObject.GetComponent<PlayerMovement>().SetMove(false);
            collision.transform.position = new Vector3(transform.position.x, transform.position.y, collision.transform.position.z);
           StartCoroutine(Fall(collision.gameObject));
        }
    }

    private IEnumerator Fall(GameObject fallen)
    {
        fallen.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        Transform[] children = fallen.GetComponentsInChildren<Transform>();

        foreach(Transform t in children)
        {
            if(t.gameObject.name != "Player")
                t.gameObject.SetActive(false);
        }
            

        float i;
        Vector2 scale = fallen.transform.localScale;
        Color initialColor = fallen.GetComponent<SpriteRenderer>().color;
        for (i = 0; i <= fallScaleDifference; i += 0.1f)
        {
            fallen.transform.localScale = new Vector2(scale.x - i, scale.y - i);
            initialColor.a = 1 - i / (fallScaleDifference * 1.2f);
            fallen.GetComponent<SpriteRenderer>().color = initialColor; 

            yield return new WaitForSeconds(0.001f);
        }

        fallen.GetComponent<PlayerStatus>().GameOver();
    }
}
