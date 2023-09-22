using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SFXPlayer : MonoBehaviour
{
    [SerializeField] AudioClip keyPickup;
    [SerializeField] AudioClip healthPotion;
    [SerializeField] AudioClip playerDamage;
    [SerializeField] AudioClip roomCleared;
    [SerializeField] AudioClip newRoom;
    [SerializeField] AudioClip basicMenu;
    [SerializeField] AudioClip selectionMenu;
    [SerializeField] AudioClip movingObject;
    [SerializeField] AudioClip @switch;

    AudioSource sfxSource;

    private void OnEnable() {
        sfxSource = GetComponent<AudioSource>();
    }

    void Awake() {
        Key.OnPlayerPickupKey += Handle_PlayerPickupKey;
        Player.OnPlayerHealthUpdate += Handle_PlayerHealthUpdated;
        Room.OnRoomCleared += Handle_RoomCleared;
        Room.OnEnteredRoom += Handle_PlayerEnteredRoom;
        MissingShapeUI.OnPlayerUsedBasicMenu += Handle_BasicMenu;
        MissingShapeUI.OnPlayerUsedSelectionMenu += Handle_SelectionMenu;
        MoveableCrate.OnObjectMoved += Handle_ObjectMoved;
        Switch.OnPlayerUsedSwitch += Handle_SwitchUsed;
    }

    private void Handle_SwitchUsed() {
        sfxSource.PlayOneShot(@switch);
    }

    private void Handle_ObjectMoved() {
        sfxSource.PlayOneShot(selectionMenu);
    }

    private void Handle_SelectionMenu() {
        sfxSource.PlayOneShot(selectionMenu);
    }

    private void Handle_BasicMenu() {
        sfxSource.PlayOneShot(basicMenu);
    } 
    private void Handle_PlayerPickupKey() {
        sfxSource.PlayOneShot(keyPickup);
    }

    private void Handle_PlayerHealthUpdated(int inc) {
        if (inc > 0) {
            // heal
            sfxSource.PlayOneShot(healthPotion);
        } else {
            //dmg
            sfxSource.PlayOneShot(playerDamage);
        }
    }

    private void Handle_RoomCleared(RoomData data) {
        sfxSource.PlayOneShot(roomCleared);
    }

    private void Handle_PlayerEnteredRoom(RoomData data) {
        sfxSource.PlayOneShot(newRoom);
    }
}
