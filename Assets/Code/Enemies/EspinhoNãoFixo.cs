using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EspinhoNãoFixo : EspinhoFixo {

    [SerializeField]
    private Sprite retracted;
    private Sprite exposed;

    [SerializeField]
    private float activeTime;

    private bool active;

	// Use this for initialization
	void Start () {
        active = true;
        exposed = gameObject.GetComponent<SpriteRenderer>().sprite;
        StartCoroutine(Switch());
	}

    private IEnumerator Switch()
    {
        while (true)
        {
            active = !active;

            if (active)
                gameObject.GetComponent<SpriteRenderer>().sprite = exposed;
            else
                gameObject.GetComponent<SpriteRenderer>().sprite = retracted;

            gameObject.GetComponent<Collider2D>().enabled = active;

            yield return new WaitForSeconds(activeTime);
        }
    }
}
