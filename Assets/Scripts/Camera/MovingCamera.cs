using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Room))]
public class MovingCamera : MonoBehaviour {
    [SerializeField] AnimationCurve animationCurve;
    [SerializeField] float moveDuration;
    [SerializeField] PlayerWalkThroughDoor trigger;

    float elapsedTime;
    Transform currentRoom;

    void Start() {
        elapsedTime = 0;
        currentRoom = GetComponent<Room>().Data.CameraLocation.transform;
        trigger.OnWalkThroughEntrance += Handle_PlayerWalkThroughTrigger;
    }

    private void Handle_PlayerWalkThroughTrigger() {
        MoveCameraTo(currentRoom.position);
    }

    private void OnTriggerExit2D(Collider2D collision) {
        MoveCameraTo(currentRoom.position);
    }

    void MoveCameraTo(Vector3 roomPos) {
        StartCoroutine(MoveCamera(roomPos));
    }

    private IEnumerator MoveCamera(Vector3 roomPos) {
        elapsedTime = 0f;

        while (elapsedTime < moveDuration) {
            float t = elapsedTime / moveDuration;
            Camera.main.transform.position = Vector3.Lerp(GetCameraPosition(), roomPos, animationCurve.Evaluate(t));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    Vector3 GetCameraPosition() => Camera.main.transform.position;
}
