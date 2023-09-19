using UnityEngine;

public class Switch : MonoBehaviour, IInteractable {
    public bool On = false;
    [SerializeField] SpriteRenderer onSprite;
    [SerializeField] SpriteRenderer offSprite;
    [SerializeField] LayerMask playerLayer;

    private void Toggle() {
        Debug.Log(onSprite.enabled);

        onSprite.enabled = !onSprite.enabled;
        On = !On;
    }

    public void OnInteract() {
        Toggle();
    }
}
