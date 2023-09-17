using UnityEngine;

public class KeyUI : MonoBehaviour {
    public GameObject keyIcon;

    void Awake() {
        Key.OnPlayerPickupKey += PlayerPickUpKey;
    }

    void PlayerPickUpKey() {
        Instantiate(keyIcon, gameObject.transform);
    }
}
