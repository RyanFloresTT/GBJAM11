using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RoomManager : MonoBehaviour
{
    [SerializeField] Tilemap tiles;
    [SerializeField] Tile closedDoorTile;
    [SerializeField] GameObject[] squareRooms;
    [SerializeField] GameObject[] lRooms;
    [SerializeField] GameObject[] rectRooms;

    public static RoomManager Instance;
    public static Action<GameObject> OnCycleRoom;

    public List<MissingShape> MissingRooms { get; set; }

    void Awake() {
        Instance = this;
        MissingRooms = new();
        MissingShape.OnMissingRoomEnabled += Handle_MissingRoomEnabled;
        Room.OnEnteredRoom += Handle_EnteredRoom;
        Room.OnRoomCleared += Handle_RoomClear;
    }

    void Handle_MissingRoomEnabled(MissingShape missingRoom) {
        MissingRooms.Add(missingRoom);
    }

    void Handle_EnteredRoom(RoomData data) {
        CloseEntranceDoor(data.enterDoor);
        MoveCameraToNewRoom(data.cameraLocation);
        
    }
    void CloseEntranceDoor(Vector3Int entranceDoor) {
        tiles.SetTile(entranceDoor, closedDoorTile);
    }
    void Handle_RoomClear(RoomData data) {
        OpenExitDoor(data.exitDoor);
    }
    void OpenExitDoor(Vector3Int exitDoor) {
        Tile emptyTile = new();
        tiles.SetTile(exitDoor, emptyTile);
    }
    void MoveCameraToNewRoom(Transform transform) {
        Camera.main.transform.position = transform.position;
    }
    public void UpdateTiles(Tilemap tiles) {
        this.tiles = tiles;
    }
}
