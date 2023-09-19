using System;
using UnityEngine;

public class Room : MonoBehaviour {

    [SerializeField] private Vector3Int enterDoor;
    [SerializeField] private Vector3Int exitDoor;
    [SerializeField] private PlayerWalkThroughDoor entranceTrigger;

    public static Action<Vector3Int> OnEnteredRoom;
    public static Action<Vector3Int> OnRoomCleared;

    void Awake() {
        entranceTrigger.OnWalkThroughEntrance += Handle_WalkThroughEntrance;
    }
    void Handle_WalkThroughEntrance() {
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
    public void CompleteRoomObjective() {
        RoomCleared();
    }
}
