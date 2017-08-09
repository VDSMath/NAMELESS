using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {

	[SerializeField]
	private int mapSquareSize;

	private int[,] mapBase; //0 = Empty.

	// Use this for initialization
	void Start () {
		mapBase = new int[mapSquareSize, mapSquareSize];

		for(int i = 0; i <= mapSquareSize - 1; i++) {
			for(int j = 0; j <= mapSquareSize - 1; j++) {
				mapBase[i, j] = 0;
			}
		}

		InitiateMap();
		for(int i = 0; i <= mapSquareSize - 1; i++) {
			for(int j = 0; j <= mapSquareSize - 1; j++) {
				Debug.Log(mapBase[i, j]);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void InitiateMap() {
		int currentH = mapSquareSize - 1;
		int currentV = mapSquareSize - 1;

		int horizontalMapSize = Random.Range(mapSquareSize/2, mapSquareSize - 1);
		int verticalMapSize = Random.Range(mapSquareSize / 2, mapSquareSize - 1);

		int direction;
		while(currentH != 0 && verticalMapSize != 0){
			direction = Random.Range(0, 1);

			if(direction == 0 && currentH > 0){
				currentH--;
				mapBase[currentH, currentV] = Random.Range(1, 3);
			} 
			else{
				currentV--;
				mapBase[currentH, currentV] = Random.Range(1, 3);
			}
		}
	}
}
