using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomIconUI : MonoBehaviour
{
    [SerializeField] Image puzzleRoom;
    [SerializeField] Image encounterRoom;
    [SerializeField] Image potionRoom;

    void Start() {
        RoomManager.OnCycleRoom += Handle_CycleRoom;
    }

    void Handle_CycleRoom(GameObject roomObject) {
        Shape shape = roomObject.GetComponent<Shape>();
        if (shape != null) {
            Debug.Log(roomObject);
            var rooms = shape.Rooms;
            foreach (Room room in rooms) {
                switch(room.Type) {
                    case RoomType.Encounter:
                        Instantiate(encounterRoom, transform);
                        break;
                    case RoomType.Puzzle:
                        Instantiate(puzzleRoom, transform);
                        break;
                    case RoomType.Potion:
                        Instantiate(potionRoom, transform);
                        break;
                }
            }
        }
    }
}
