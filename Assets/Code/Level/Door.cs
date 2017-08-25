using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction{
	UP,DOWN,LEFT,RIGHT
}
public class Door : MonoBehaviour,IInteractable {

	[SerializeField]private Direction myDir;
	[SerializeField]private Door otherDoor;
    private float moveSpeed = 10f;
    private GameObject mainCamera;

    private bool canMove;
    private Vector3 target;

    Vector2 direction;

    const float rayDistance = 5f;

	private void Start(){        
        canMove = false;

        mainCamera = Camera.main.gameObject;
    }
    private void Update()
    {
        if (canMove)
        {
            Move();
        }
    }
    private void ExistDoor(){
		Vector2 origin = transform.position;
		Vector2 direction;

		switch(myDir){
			case Direction.UP:
				direction = Vector2.up;
				break;
			case Direction.DOWN:
				direction = Vector2.down;
				break;
			case Direction.LEFT:
				direction = Vector2.left;
				break;
			case Direction.RIGHT:
				direction = Vector2.right;
				break;
			default:
				direction = Vector2.up;
				break;
		}

		RaycastHit2D ray = Physics2D.Raycast(origin,direction,rayDistance,1<<LayerMask.NameToLayer("Door"));
		if(ray.collider == null){
			Destroy(this.gameObject);
		}else{
            //minimapImage = ray.collider.transform.parent.Find("New Sprite").gameObject;
            //minimapImage.SetActive(false);
            //fog = ray.collider.transform.parent.Find("Fog").gameObject;
            otherDoor = ray.collider.gameObject.GetComponent<Door>();
		}
	}
	public void Interact(){
        //fog.SetActive(false);
        //minimapImage.SetActive(true);
		GameObject player = GameObject.Find("Player");

        switch (myDir)
        {
            case Direction.UP:
                direction = Vector2.up;
                break;
            case Direction.DOWN:
                direction = Vector2.down;
                break;
            case Direction.LEFT:
                direction = Vector2.left;
                break;
            case Direction.RIGHT:
                direction = Vector2.right;
                break;
            default:
                direction = Vector2.up;
                break;
        }

        transform.parent.parent.parent.GetComponent<RoomManager>().SwitchCurrentRoom(otherDoor.transform.parent.parent.gameObject);
        player.transform.position = otherDoor.transform.position + new Vector3(direction.x * 1.2f, direction.y * 1.2f);
		ChangeCameraPosition();
	}

	private void ChangeCameraPosition(){
		switch(myDir){
			case Direction.UP:
				direction = Vector2.up*LevelGenerator.roomSize.y;
				break;
			case Direction.DOWN:
				direction = Vector2.down*LevelGenerator.roomSize.y;
				break;
			case Direction.LEFT:
				direction = Vector2.left*LevelGenerator.roomSize.x;
				break;
			case Direction.RIGHT:
				direction = Vector2.right*LevelGenerator.roomSize.x;
				break;
			default:
				direction = Vector2.up*LevelGenerator.roomSize.y;
				break;
		}

		target = otherDoor.transform.parent.transform.position;
        canMove = true;
    }
    void Move()
    {
        mainCamera.transform.position = Vector2.Lerp(mainCamera.transform.position, target, .12f);
        mainCamera.transform.position = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y, -10);

        if (Vector2.Distance(mainCamera.transform.position, target) <= .01f)
        {
            mainCamera.transform.position = new Vector3(target.x, target.y, mainCamera.transform.position.z);
            canMove = false;
        }
    }
}
