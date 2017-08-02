using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Collider2D))]
public class IPickup : MonoBehaviour
{
    [Tooltip("Define o tipo de item: armor, cure, defense, key")]
    [SerializeField]
    private string itemType;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            collision.gameObject.GetComponent<PlayerStatus>().PickUp(itemType);

            GameObject.Destroy(gameObject);
        }
    }
}