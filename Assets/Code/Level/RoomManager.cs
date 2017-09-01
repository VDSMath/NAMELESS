using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour {

    [SerializeField]
    private List<GameObject> rooms;
    private GameObject currentRoom;

	// Use this for initialization
	void Start () {
        currentRoom = GameObject.Find("Room1");
        currentRoom.GetComponent<Room>().Enter();

        Room[] _rooms = GetComponentsInChildren<Room>();

        foreach(Room r in _rooms)
        {
            rooms.Add(r.gameObject);
        }
    }
	
    public void SwitchCurrentRoom(GameObject newRoom)
    {
        StopAllDoors();

        currentRoom.GetComponent<Room>().Exit();
       
        currentRoom = newRoom;

        currentRoom.GetComponent<Room>().Enter();
    }

    private void StopAllDoors()
    {
        Room[] rooms = GetComponentsInChildren<Room>();

        foreach(Room r in rooms)
        {
            r.StopDoors();
        }
    }
}
