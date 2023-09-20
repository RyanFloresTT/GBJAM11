using System;
using UnityEngine;

public class Room : MonoBehaviour {
    [SerializeField] private RoomData roomData;
    [SerializeField] private PlayerWalkThroughDoor entranceTrigger;

    public static Action<RoomData> OnEnteredRoom;
    public static Action<RoomData> OnRoomCleared;

    void Awake() {
        entranceTrigger.OnWalkThroughEntrance += Handle_WalkThroughEntrance;
    }
    void Handle_WalkThroughEntrance() {
        EnterRoom();
    }

    [ContextMenu("Enter Room")]
    void EnterRoom() {
        OnEnteredRoom?.Invoke(roomData);
    }

    [ContextMenu("Clear Room")]
    void RoomCleared() {
        OnRoomCleared?.Invoke(roomData);
    }
    public void CompleteRoomObjective() {
        RoomCleared();
    }
}
