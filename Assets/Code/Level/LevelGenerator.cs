﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {

	[Header("Dungeon Preferences")]
	const int size = 8;
	[SerializeField] private int numberOfSanctums = 4;
	[SerializeField] private int sideRoomChance;
	[SerializeField] private bool canHaveLockedDoors;

	[Header("Dungeon Tiles")]
	[SerializeField] private List<GameObject> obstacleTiles;
	[SerializeField] private List<GameObject> mainRoom;
	[SerializeField] private List<GameObject> sideRoom;
	[SerializeField] private List<GameObject> rooms;

	static public Vector2 roomSize;
	static public float roomScale;

    bool firstRoom;

    [Header("Dungeon Map")]
	GameObject[,] map = new GameObject[size,size];
	int[,] mapModel = new int[size,size];

	private void Start(){
        firstRoom = true;
		GetRoomSize();
		CreateMainPath();
	}
	private void CreateMainPath(){
		int hori, vert;
		hori = vert = size-1;

		while (hori + vert > 0){
			if(Random.Range(0,2) > 0 && hori > 0){
                hori--;
				if(Random.Range(0,10) > sideRoomChance){
					CreateSidePath(hori, vert, Vector2.left);
				}

			}else if (vert > 0){
                vert--;
				if(Random.Range(0,10) > sideRoomChance){
					CreateSidePath(hori, vert, Vector2.up);
				}
			}
			mapModel[vert,hori] = 1;
            int roomNumber = Random.Range(0, mainRoom.Count);
            GameObject g = Instantiate(mainRoom[roomNumber],
                                  Vector3.up * vert * roomSize.y + Vector3.right * hori * roomSize.x,
                                  Quaternion.identity,
                                  transform);
            rooms.Add(g);
		}

        rooms[rooms.Count-1].GetComponentInChildren<ParticleSystem>().gameObject.SetActive(false);
	}
	private void CreateSidePath(int h, int v, Vector2 dir){
		int numberOfSideRooms = Random.Range(2,4);
		int actualX = h, actualY = v;
		int x = (int) dir.x;
		int y = (int) dir.y;
		while(numberOfSideRooms > 0){
			int dif_h = Mathf.Clamp(actualX-x,0,size-1);
			int dif_v = Mathf.Clamp(actualY-y,0,size-1);

			if(mapModel[dif_h,dif_v] == 0){
				InstantiateSideRoom(dif_h,dif_v);
				numberOfSideRooms--;
				actualX = dif_h;
				actualY = dif_v;
				mapModel[dif_h,dif_v] = 2;
			}else{
				return;
			}
		}
	}
	private void InstantiateSideRoom(int horizontal, int vertical){
        int roomNumber = Random.Range(0, sideRoom.Count);
		var temp_go = Instantiate(sideRoom[roomNumber],
							  	  Vector3.up*vertical*roomSize.y + Vector3.right*horizontal*roomSize.x,
								  Quaternion.identity,
								  transform);
		rooms.Add(temp_go);
	}
	private void GetRoomSize(){
		roomSize = mainRoom[0].GetComponent<SpriteRenderer>().size*mainRoom[0].transform.localScale.x;
		roomScale = mainRoom[0].transform.localScale.x;
	}

}