﻿using System;
using UnityEngine;

[Serializable]
public class RoomData {
    public Vector3Int enterDoor;
    public Vector3Int exitDoor;
    public RoomType type;
    public Transform cameraLocation;
}
