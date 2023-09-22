using System;
using TMPro;
using UnityEngine;

public class MissingShapeUI : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI remainingText;
    [SerializeField] private GameObject iconContainer;
    [SerializeField] private GameObject encounterIcon;
    [SerializeField] private GameObject puzzleIcon;
    [SerializeField] private GameObject potionIcon;

    public static Action OnPlayerFinishedChoosing;

    RoomManager roomManager;
    MissingShape missingRoom;
    int missingIndex;
    int shapeIndex;

    void Start() {
        roomManager = RoomManager.Instance;
        shapeIndex = 0;
        missingIndex = 0;

        if (roomManager.MissingRooms.Count > 0 ) {
            missingRoom = roomManager.MissingRooms[0];  
            DisplayCurrentShape();
        }
    }

    public void ConfirmCurrentShape() {
        missingRoom.SpawnInRoom(missingRoom.CompatibleShapes[shapeIndex]);

        if (roomManager.MissingRooms.Count - 1 <= 0) {
            Debug.Log("H");
            OnPlayerFinishedChoosing?.Invoke();
        } else {
            roomManager.MissingRooms.RemoveAt(0);
            missingRoom = roomManager.MissingRooms[0];
            DisplayCurrentShape();
        }
    }

    public void NextShape() {
        if (shapeIndex < missingRoom.CompatibleShapes.Count - 1) {
            shapeIndex++;
            DisplayCurrentShape();
        }
    }

    public void PreviousShape() {
        if (shapeIndex > 0) {
            shapeIndex--;
            DisplayCurrentShape(); 
        }
    }
    void DisplayCurrentShape() {
        ClearContainer();
        UpdateRemainingText();
        GameObject shapeGO = missingRoom.CompatibleShapes[shapeIndex];
        Shape currentShape = shapeGO.GetComponent<Shape>();
        if (currentShape != null) {
            foreach (Room room in currentShape.Rooms) {
                SpawnIconInContainer(room.Type);
            }
        }
    }

    void ClearContainer() {
        foreach (Transform child in iconContainer.transform) { Destroy(child.gameObject); }
    }

    void UpdateRemainingText() {
        remainingText.text = $"There are {roomManager.MissingRooms.Count} remaining rooms to choose.";
    }

    void SpawnIconInContainer(RoomType type) {
        switch (type) {
            case RoomType.Encounter:
                Instantiate(encounterIcon, iconContainer.transform);
                break;
            case RoomType.Puzzle:
                Instantiate(puzzleIcon, iconContainer.transform);
                break;
            case RoomType.Potion:
                Instantiate(potionIcon, iconContainer.transform);
                break;
        }
    }
}
