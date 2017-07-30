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
            collision.gameObject.GetComponent<PlayerMovement>().canMove = false;
            collision.transform.position = new Vector3(transform.position.x, transform.position.y, collision.transform.position.z);
           StartCoroutine(Fall(collision.gameObject));
        }
    }

    private IEnumerator Fall(GameObject fallen)
    {
        float i;
        Vector2 scale = fallen.transform.localScale;
        for (i = 0; i <= fallScaleDifference; i += 0.1f)
        {
            fallen.transform.localScale = new Vector2(scale.x - i, scale.y - i);

            yield return new WaitForSeconds(0.001f);
        }
    }
}
