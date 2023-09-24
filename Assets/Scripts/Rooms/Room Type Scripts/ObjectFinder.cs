using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This will be used for PuzzleManager to find enemies and poitions. It will return true when ALL GO's are null in 'objects' array.
public class ObjectFinder : MonoBehaviour, IPuzzleObject {

    [SerializeField] GameObject[] objects;

    bool allDestroyed;
    public bool PuzzlePieceSolved => allDestroyed;

    public void Update() {
        foreach(var obj in objects) {
            if (obj != null) { return; }
        }
        allDestroyed = true;
    }
}
