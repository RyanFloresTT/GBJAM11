using System;
using UnityEngine;
using UnityEngine.Tilemaps;

[Serializable]
public class RoomData {
    public Vector3Int EnterDoorLocation;
    public Vector3Int ExitDoorLocation;
    public RoomType Type;
    public Transform CameraLocation;
    public AnimatedTile ClosingEntranceDoor;
    public Tile LastClosingTile;
    public AnimatedTile OpeningExitDoor;
    public Tile LastOpeningTile;
}
