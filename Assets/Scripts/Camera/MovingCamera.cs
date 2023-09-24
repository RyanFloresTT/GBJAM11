using System.Collections;
using UnityEngine;

public class MovingCamera : MonoBehaviour {

    [SerializeField] Transform previousRoom;
    [SerializeField] Transform nextRoom;
    [SerializeField] Directions entranceDirection;
    [SerializeField] Directions exitDirection;
    [SerializeField] AnimationCurve animationCurve;
    [SerializeField] float moveDuration;

    float elapsedTime;

    void Start() {
        elapsedTime = 0;
    }

    private void OnTriggerExit2D(Collider2D collision) {

        Vector2 direction = collision.transform.position - transform.position;

        switch (entranceDirection) {
            case Directions.North:
                if (direction.y <= 0) { MoveCameraTo(nextRoom.position); }
                break;
            case Directions.South:
                if (direction.y >= 0) { MoveCameraTo(nextRoom.position); }
                break;
            case Directions.East:
                if (direction.x <= 0) { MoveCameraTo(nextRoom.position); }
                break;
            case Directions.West:
                if (direction.x >= 0) { MoveCameraTo(nextRoom.position); }
                break;
            default:
                Debug.LogWarning("Direction Enum Defaulted!");
                break;
        }
        switch (exitDirection) {
            case Directions.North:
                if (direction.y <= 0) { MoveCameraTo(previousRoom.position); }
                break;
            case Directions.South:
                if (direction.y >= 0) { MoveCameraTo(previousRoom.position); }
                break;
            case Directions.East:
                if (direction.x <= 0) { MoveCameraTo(previousRoom.position); }
                break;
            case Directions.West:
                if (direction.x >= 0) { MoveCameraTo(previousRoom.position); }
                break;
            default:
                Debug.LogWarning("Direction Enum Defaulted!");
                break;
        }
    }

    void MoveCameraTo(Vector3 roomPos) {
        StartCoroutine(MoveCamera(roomPos));
    }

    private IEnumerator MoveCamera(Vector3 roomPos) {
        Debug.Log("moving");
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
