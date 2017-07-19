using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class LevelGenerator : MonoBehaviour {

	[Header("Dungeon Preferences")]
	const int size = 8;
	[SerializeField] private int numberOfSanctums = 4;
	[SerializeField] private bool canHaveLockedDoors;

	[Header("Dungeon Tiles")]
	[SerializeField] private List<GameObject> roomTiles;
	[SerializeField] private List<GameObject> rooms;

	static public Vector2 roomSize;
	static public float roomScale;

	[Header("Dungeon Map")]
	GameObject[,] map = new GameObject[size,size];
	int[,] mapModel = new int[size,size];

	private void Start(){
		GetRoomSize();
		CreateMainPath();
	}
	private void CreateMainPath(){
		int hori, vert;
		hori = vert = size-1;

		while (hori + vert > 0){
			if(Random.Range(0,2) > 0 && hori > 0){
				hori--;

			}else if (vert > 0){
				vert--;
			}
			mapModel[vert,hori] = 1;
			rooms.Add(Instantiate(roomTiles[Random.Range(0,roomTiles.Count)],
								  Vector3.zero + Vector3.up*vert*roomSize.y + Vector3.right*hori*roomSize.x,
								  Quaternion.identity));
		}
	}
	private void GetRoomSize(){
		roomSize = roomTiles[0].GetComponent<SpriteRenderer>().size*roomTiles[0].transform.localScale.x;
		roomScale = roomTiles[0].transform.localScale.x;
	}

}
