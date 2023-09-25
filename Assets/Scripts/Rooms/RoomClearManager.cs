using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(Room))]
public class RoomClearManager : MonoBehaviour {
    [SerializeField] private List<GameObject> requirements;
    [SerializeField] private bool solved = false;

    Room currentRoom;

    void Awake() {
        currentRoom = GetComponent<Room>();
    }

    void Update() {
        if (!solved && CheckSolved()) {
            solved = true;
            currentRoom.CompleteRoomObjective();
        }
    }

    private bool CheckSolved() {
        if (requirements.Count == 0) return true;

        return requirements.All(requirement => {
            IPuzzleObject puzzle = requirement.GetComponent<IPuzzleObject>();
            return puzzle != null && puzzle.PuzzlePieceSolved;
        });
    }
}
