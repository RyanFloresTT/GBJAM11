using System;
using UnityEngine;

public class Room : MonoBehaviour {
    [SerializeField] RoomData data;
    [SerializeField] PlayerWalkThroughDoor entranceTrigger;

    public RoomType Type { get { return data.Type; } }
    public int Points { get { return ScoreHandler.CalculateScore(Type); } private set { } }
    public RoomData Data { get { return data; } private set { } }

    public static Action<RoomData> OnEnteredRoom;
    public static Action<RoomData> OnRoomCleared;

    void Awake() {
        ListenForEntranceTrigger();
        Points += ScoreHandler.CalculateScore(Type);
    }
    void Handle_WalkThroughEntrance() {
        EnterRoom();
    }

    void EnterRoom() {
        OnEnteredRoom?.Invoke(data);
    }

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
