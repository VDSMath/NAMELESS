using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction{
	UP,DOWN,LEFT,RIGHT
}
public class Door : MonoBehaviour,IInteractable {

	[SerializeField]private Direction myDir;
	[SerializeField]private Door otherDoor;
	private GameObject mainCamera;

	const float rayDistance = 5f;

	private void Start(){
		GetCamera();
		ExistDoor();
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
			otherDoor = ray.collider.gameObject.GetComponent<Door>();
		}
	}
	public void Interact(){
		GameObject player = GameObject.Find("Player");
		player.transform.position = otherDoor.transform.position;
		ChangeCameraPosition();
	}
	private void GetCamera(){
		mainCamera = GameObject.Find("Main Camera");
	}
	private void ChangeCameraPosition(){
		Vector2 direction;
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
		mainCamera.transform.position += (Vector3)direction;
	}
}
