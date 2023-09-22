using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Shape : MonoBehaviour
{
    [SerializeField] Room[] rooms;

    public Room[] Rooms { get { return rooms; } set { } }
    public int TotalPoints { get; set; }

    void Awake() {
        foreach (Room room in rooms) {
            TotalPoints += PointCalculator.GetPoints(room.Type);
        }
    }
}
