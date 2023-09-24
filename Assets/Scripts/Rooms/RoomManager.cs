using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RoomManager : MonoBehaviour {
    [SerializeField] Tilemap wallTiles;
    [SerializeField] Tilemap doorTiles;

    public static RoomManager Instance;
    public static Action<GameObject> OnCycleRoom;

    public List<MissingShape> MissingRooms { get; set; }

    const float CLOSING_DELAY = 0.6f;
    const float OPENING_DELAY = 0.5f;

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
        SetAnimatedTile(data.EnterDoorLocation, data.ClosingEntranceDoor, data.LastClosingTile, CLOSING_DELAY, true);
        MoveCameraToNewRoom(data.CameraLocation);
        
    }
    void Handle_RoomClear(RoomData data) {
        SetAnimatedTile(data.ExitDoorLocation, data.OpeningExitDoor, data.LastOpeningTile, OPENING_DELAY, false);
    }
    void MoveCameraToNewRoom(Transform transform) {
        Camera.main.transform.position = transform.position;
    }
    public void UpdateTiles(Tilemap wallTiles, Tilemap doorTiles) {
        this.wallTiles = wallTiles;
        this.doorTiles = doorTiles;
    }

    void SetAnimatedTile(Vector3Int doorLocation, AnimatedTile doorTile, Tile lastFrame, float delay, bool isClosing) {
        wallTiles.SetTile(doorLocation, doorTile);
        StartCoroutine(PlaceLastFrameTileAfterDelay(doorLocation, lastFrame, delay, isClosing));
    }

    IEnumerator PlaceLastFrameTileAfterDelay(Vector3Int tileLocation, Tile lastFrame, float delay, bool isClosing) {
        yield return new WaitForSeconds(delay);
        if (isClosing) {
            wallTiles.SetTile(tileLocation, lastFrame);
        } else {
            Tile blankTile = new();
            wallTiles.SetTile(tileLocation, blankTile);
            doorTiles.SetTile(tileLocation, lastFrame);
        }
    }
}
