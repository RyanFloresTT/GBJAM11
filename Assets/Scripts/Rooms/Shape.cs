using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape : MonoBehaviour
{
    [SerializeField] Room[] rooms;

    public Room[] Rooms { get; set; }
    public int TotalPoints { get; set; }

    private void Awake() {
        foreach (Room room in rooms) {
            TotalPoints += PointCalculator.GetPoints(room.Type);
        }
    }
}
