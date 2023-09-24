using System;
using System.Collections.Generic;
using UnityEngine;

public class MissingShape : MonoBehaviour
{
    [SerializeField] RoomShapes shape;
    [SerializeField] GameObject[] compatibleShapes;

    public List<GameObject> CompatibleShapes { get; set; }
    public RoomShapes Shape { get { return shape; } }

    public static Action<MissingShape> OnMissingRoomEnabled;

    void Awake() {
        CompatibleShapes = new();
        for (int i = 0; i < compatibleShapes.Length; i++) {
            CompatibleShapes.Add(compatibleShapes[i]);
        }
    }

    void Start() {
        OnMissingRoomEnabled?.Invoke(this);
    }

    public void SpawnInRoom(GameObject room) {
        Instantiate(room, transform);
    }
}
