using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Altar : MonoBehaviour, IInteractable {

    [SerializeField]
    private GameObject altarCanvasObject;

    private GameObject player;

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    public void Interact()
    {
        player.GetComponent<PlayerMovement>().SetMove(false);
        altarCanvasObject.SetActive(true);
    }
}
