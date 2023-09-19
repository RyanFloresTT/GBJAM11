using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {

    [SerializeField] private Vector3Int enterDoor;
    [SerializeField] private Vector3Int exitDoor;
    [SerializeField] private PlayerWalkThroughDoor entranceTrigger;

    public static Action<Vector3Int> OnEnteredRoom;
    public static Action<Vector3Int> OnRoomCleared;

    private void Awake() {
        entranceTrigger.OnWalkThroughEntrance += Handle_WalkThroughEntrance;
    }

    private void Handle_WalkThroughEntrance() {
        EnterRoom();
    }

    [ContextMenu("Enter Room")]
    void EnterRoom() {
        OnEnteredRoom?.Invoke(enterDoor);
    }

    [ContextMenu("Clear Room")]
    void RoomCleared() {
        OnRoomCleared?.Invoke(exitDoor);
    }
}
