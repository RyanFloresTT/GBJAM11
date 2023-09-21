using System;
using UnityEngine;

public class MissingRoom : MonoBehaviour
{
    [SerializeField] RoomShapes missingRoomShape;

    public RoomShapes MissingRoomShape { get { return missingRoomShape; } }

    public static Action<MissingRoom> OnMissingRoomEnabled;

    void Start() {
        OnMissingRoomEnabled?.Invoke(this);
    }

    public void SpawnInRoom(GameObject room) {
        Instantiate(room, transform);
    }
}
