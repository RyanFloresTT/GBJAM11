using UnityEngine;
using UnityEngine.Tilemaps;

public class UpdateRoomManagerTiles : MonoBehaviour
{
    [SerializeField] PlayerWalkThroughDoor walkThroughTrigger;
    [SerializeField] Tilemap newWallTiles;
    [SerializeField] Tilemap newDoorTiles;
    RoomManager roomManager;

    void Start() {
        walkThroughTrigger.OnWalkThroughEntrance += Handle_WalkedThroughShape;
        roomManager = RoomManager.Instance;
    }

    private void Handle_WalkedThroughShape() {
        roomManager.UpdateTiles(newWallTiles, newDoorTiles);
    }
}
