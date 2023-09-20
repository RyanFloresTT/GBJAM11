using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RoomClearManager : MonoBehaviour {
    [SerializeField] private List<GameObject> requirements;
    [SerializeField] private bool solved = false;
    [SerializeField] Room currentRoom;

    void Update() {
        if (!solved && CheckSolved()) {
            solved = true;
            currentRoom.CompleteRoomObjective();
            Debug.Log("The room has been solved!! unlock them doors!");
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
