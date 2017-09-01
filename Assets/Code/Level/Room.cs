using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CameraMovement
{
    Fixed, Free, Horizontal, Vertical
}

public class Room : MonoBehaviour {

    private bool active, visited;
    private List<GameObject> enemies;
    private GameObject activeImage, mainCamera, player;

    public CameraMovement roomType;

	// Use this for initialization
	void Start () {
        activeImage = transform.Find("Active").gameObject;
        enemies = new List<GameObject>();
        mainCamera = Camera.main.gameObject;
        player = GameObject.Find("Player");
            
        IEnemy[] enem = GetComponentsInChildren<IEnemy>();

        foreach(IEnemy e in enem)
        {
            enemies.Add(e.gameObject);
        }

        UpdateContent();
        //gameObject.SetActive(active);
	}


    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            if (roomType != CameraMovement.Fixed)
            {
                StopDoors();
                FollowPlayer();     
            }
        }
    }

    private void FollowPlayer()
    {
        Vector3 temp = mainCamera.transform.position;

        mainCamera.transform.position = Vector2.Lerp(mainCamera.transform.position, player.transform.position, 0.6f);

        switch (roomType)
        {
            case CameraMovement.Free:
                mainCamera.transform.position = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y, temp.z);
                break;

            case CameraMovement.Horizontal:
                mainCamera.transform.position = new Vector3(mainCamera.transform.position.x, temp.y, temp.z);
                break;

            case CameraMovement.Vertical:
                mainCamera.transform.position = new Vector3(temp.x, mainCamera.transform.position.y, temp.z);
                break;

            default:
                break;
        }

        
    }

    public void StopDoors()
    {
        Door[] doors = GetComponentsInChildren<Door>();

        foreach(Door d in doors)
        {
            d.canMove = false;
        }
    }

    public void Enter()
    {
        active = true;
        visited = true;

        UpdateContent();
    }

    public void Exit()
    {
        active = false;

        UpdateContent();
    }

    void UpdateContent()
    {
        activeImage = transform.Find("Active").gameObject;

        activeImage.SetActive(!active);

        if (enemies != null)
        {
            foreach (GameObject enemy in enemies)
            {
                enemy.SetActive(active);
            }
        }
    }
	
}
