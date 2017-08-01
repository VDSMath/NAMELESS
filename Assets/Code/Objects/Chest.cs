using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable {

    [SerializeField]
    private Sprite closedSprite, openSprite;
    [SerializeField]
    private GameObject content;

    private bool isOpen;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Interact()
    {
        if (!isOpen)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = openSprite;

            Instantiate(content, new Vector3(transform.position.x, transform.position.y - 1, transform.position.z),Quaternion.identity, transform);
        }
    }
}
