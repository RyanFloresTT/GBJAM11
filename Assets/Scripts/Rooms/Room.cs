using System;
using UnityEngine;

public class Room : MonoBehaviour {
    [SerializeField] RoomData data;
    [SerializeField] RoomType type;
    [SerializeField] PlayerWalkThroughDoor entranceTrigger;

    public RoomType Type { get { return type; } }
    public int Points { get; set; }

    public static Action<RoomData> OnEnteredRoom;
    public static Action<RoomData> OnRoomCleared;

    void Awake() {
        ListenForEntranceTrigger();
        Points += PointCalculator.GetPoints(Type);
    }
    void Handle_WalkThroughEntrance() {
        EnterRoom();
    }

    [ContextMenu("Enter Room")]
    void EnterRoom() {
        OnEnteredRoom?.Invoke(data);
    }

    [ContextMenu("Clear Room")]
    void RoomCleared() {
        OnRoomCleared?.Invoke(data);
    }
    public void CompleteRoomObjective() {
        RoomCleared();
    }

    void ListenForEntranceTrigger() {
        if (entranceTrigger == null) { return; }
        entranceTrigger.OnWalkThroughEntrance += Handle_WalkThroughEntrance;
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.yellow;
        Vector3 halfDimensions = new(4, 3, 0f);
        Vector3 offsetPosition = transform.position + new Vector3(0f, 1f, 0f);
        Gizmos.DrawWireCube(offsetPosition, halfDimensions * 2);
    }
}
