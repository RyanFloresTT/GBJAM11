using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class MissingShapeUI : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI remainingText;
    [SerializeField] private GameObject iconContainer;
    [SerializeField] private GameObject encounterIcon;
    [SerializeField] private GameObject puzzleIcon;
    [SerializeField] private GameObject potionIcon;

    public static Action OnPlayerFinishedChoosing;
    public static Action OnPlayerUsedBasicMenu;
    public static Action OnPlayerUsedSelectionMenu;

    RoomManager roomManager;
    MissingShape missingRoom;
    int shapeIndex;
    PlayerInputActions playerInput;

    void OnEnable() {
        playerInput = new();

        playerInput.Player.LeftMenu.Enable();
        playerInput.Player.RightMenu.Enable();
        playerInput.Player.A.Enable();

        playerInput.Player.LeftMenu.performed += Handle_LeftMenu_Performed;
        playerInput.Player.RightMenu.performed += Handle_RightMenu_Performed;
        playerInput.Player.A.performed += Handle_A_Performed;
    }

    void OnDisable() {
        playerInput.Player.LeftMenu.Disable();
        playerInput.Player.RightMenu.Disable();
        playerInput.Player.A.Disable();
    }
    private void Handle_LeftMenu_Performed(InputAction.CallbackContext context) {
        PreviousShape();
    }

    private void Handle_RightMenu_Performed(InputAction.CallbackContext context) {
        NextShape();
    }


    private void Handle_A_Performed(InputAction.CallbackContext context) {
        ConfirmCurrentShape();
        OnPlayerUsedSelectionMenu?.Invoke();
    }

    void Start() {
        roomManager = RoomManager.Instance;
        shapeIndex = 0;

        if (roomManager.MissingRooms.Count > 0 ) {
            missingRoom = roomManager.MissingRooms[0];  
            DisplayCurrentShape();
        }
    }

    public void ConfirmCurrentShape() {
        missingRoom.SpawnInRoom(missingRoom.CompatibleShapes[shapeIndex]);

        if (roomManager.MissingRooms.Count - 1 <= 0) {
            OnPlayerFinishedChoosing?.Invoke();
        } else {
            roomManager.MissingRooms.RemoveAt(0);
            missingRoom = roomManager.MissingRooms[0];
            DisplayCurrentShape();
        }
    }

    void NextShape() {
        if (shapeIndex < missingRoom.CompatibleShapes.Count - 1) {
            shapeIndex++;
            DisplayCurrentShape();
            OnPlayerUsedBasicMenu?.Invoke();
        }
    }

     void PreviousShape() {
        if (shapeIndex > 0) {
            shapeIndex--;
            DisplayCurrentShape();
            OnPlayerUsedBasicMenu?.Invoke();
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
        remainingText.text = $"{roomManager.MissingRooms.Count} left...";
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
