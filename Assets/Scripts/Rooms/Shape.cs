using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Shape : MonoBehaviour
{
    [SerializeField] Room[] rooms;

    public Room[] Rooms { get { return rooms; } set { } }
    public int TotalPoints { get { return CountPoints(); } private set { } }

    int CountPoints() {
        int total = 0;
        foreach (Room room in rooms) {
            total += room.Points;
        }
        return total;
    }
}
