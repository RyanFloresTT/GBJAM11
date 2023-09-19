using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RoomManager : MonoBehaviour
{
    [SerializeField] Tilemap tiles;
    [SerializeField] Tile closedDoorTile;
    Room currentRoom;

    void Awake() {
        Room.OnEnteredRoom += Handle_EnteredRoom;
        Room.OnRoomCleared += Handle_RoomClear;
    }
    void Handle_EnteredRoom(Vector3Int vector) {
        CloseEntranceDoor(vector);
    }
    void CloseEntranceDoor(Vector3Int entranceDoor) {
        tiles.SetTile(entranceDoor, closedDoorTile);
        Debug.Log("Entrance Door Closed.");
    }
    void Handle_RoomClear(Vector3Int vector) {
        OpenExitDoor(vector);
    }
    void OpenExitDoor(Vector3Int exitDoor) {
        Tile emptyTile = new();
        tiles.SetTile(exitDoor, emptyTile);
        Debug.Log("Exit Door Opened.");
    }
}
