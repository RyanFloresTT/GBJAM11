using UnityEngine;
using UnityEngine.Tilemaps;

public class RoomManager : MonoBehaviour
{
    [SerializeField] Tilemap tiles;
    [SerializeField] Tile closedDoorTile;

    void Awake() {
        Room.OnEnteredRoom += Handle_EnteredRoom;
        Room.OnRoomCleared += Handle_RoomClear;
    }
    void Handle_EnteredRoom(RoomData data) {
        CloseEntranceDoor(data.enterDoor);
        MoveCameraToNewRoom(data.cameraLocation);
        
    }
    void CloseEntranceDoor(Vector3Int entranceDoor) {
        tiles.SetTile(entranceDoor, closedDoorTile);
        Debug.Log("Entrance Door Closed.");
    }
    void Handle_RoomClear(RoomData data) {
        OpenExitDoor(data.exitDoor);
    }
    void OpenExitDoor(Vector3Int exitDoor) {
        Tile emptyTile = new();
        tiles.SetTile(exitDoor, emptyTile);
        Debug.Log("Exit Door Opened.");
    }
    void MoveCameraToNewRoom(Transform transform) {
        Camera.main.transform.position = transform.position;
    }
}
