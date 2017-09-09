using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour {

    private GameObject mainCam, player;

	// Use this for initialization
	void Start () {
        mainCam = Camera.main.gameObject;
        player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
        FollowPlayer();
	}

    void FollowPlayer()
    {
        mainCam.transform.position = new Vector3(player.transform.position.x, 
                                                 player.transform.position.y, 
                                                 mainCam.transform.position.z);
    }
}
