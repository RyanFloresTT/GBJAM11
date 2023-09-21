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

    List<MissingRoom> missingRooms;
    GameObject chosenRoom;
    int spaceIndex;
    int chooseIndex;
    int totalMissing;

    void Awake() {
        MissingRoom.OnMissingRoomEnabled += Handle_MissingRoomEnabled;
        Room.OnEnteredRoom += Handle_EnteredRoom;
        Room.OnRoomCleared += Handle_RoomClear;
        missingRooms = new();
        Instance = this;
    }

    void Start() {
        totalMissing = 0;
        spaceIndex = 0;
    }

    private void Handle_MissingRoomEnabled(MissingRoom missingRoom) {
        missingRooms.Add(missingRoom);
        totalMissing++;
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

    public void CycleForwardMissingRooms() {
        spaceIndex = (spaceIndex + 1) % missingRooms.Count;
        Debug.Log(missingRooms[spaceIndex]);
    }
    public void CycleBackMissingRooms() {
        spaceIndex = (spaceIndex - 1 + missingRooms.Count) % missingRooms.Count;
        Debug.Log(missingRooms[spaceIndex]);
    }

    public void CycleThroughGORooms() {
        switch (missingRooms[spaceIndex].MissingRoomShape) {
            case RoomShapes.Square2x2:
                OnCycleRoom?.Invoke(squareRooms[(chooseIndex + 1) % missingRooms.Count]);
                break;
            case RoomShapes.L:
                OnCycleRoom?.Invoke(lRooms[(chooseIndex + 1) % missingRooms.Count]);
                break;
            case RoomShapes.Rectangle1x2:
                OnCycleRoom?.Invoke(rectRooms[(chooseIndex + 1) % missingRooms.Count]);
                break;
            default:
                Debug.LogError("Spawn room case defaulted!");
                break;
        }
    }
    public GameObject CycleBackGORooms() {
        switch (missingRooms[spaceIndex].MissingRoomShape) {
            case RoomShapes.Square2x2:
                return squareRooms[(chooseIndex - 1 + missingRooms.Count) % missingRooms.Count];
            case RoomShapes.L:
                return squareRooms[(chooseIndex - 1 + missingRooms.Count) % missingRooms.Count];
            case RoomShapes.Rectangle1x2:
                return squareRooms[(chooseIndex - 1 + missingRooms.Count) % missingRooms.Count];
            default:
                Debug.LogError("Spawn room case defaulted!");
                return null;
        }
    }

    public void SpawnRoom() {
        switch (missingRooms[spaceIndex].MissingRoomShape) {
            case RoomShapes.Square2x2:
                missingRooms[spaceIndex].SpawnInRoom(chosenRoom);
                break;
            case RoomShapes.L:
                missingRooms[spaceIndex].SpawnInRoom(chosenRoom);
                break;
            case RoomShapes.Rectangle1x2:
                missingRooms[spaceIndex].SpawnInRoom(chosenRoom);
                break;
            default:
                Debug.LogError("Spawn room case defaulted!");
                break;
        }

        spaceIndex++;
    }
}
