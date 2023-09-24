using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SFXPlayer : MonoBehaviour {
    [SerializeField] AudioClip keyPickup;
    [SerializeField] AudioClip healthPotion;
    [SerializeField] AudioClip playerDamage;
    [SerializeField] AudioClip roomCleared;
    [SerializeField] AudioClip newRoom;
    [SerializeField] AudioClip basicMenu;
    [SerializeField] AudioClip selectionMenu;
    [SerializeField] AudioClip movingObject;
    [SerializeField] AudioClip @switch;
    [SerializeField] AudioClip playerDeath;
    [SerializeField] AudioClip enemyDamage;
    [SerializeField] AudioClip enemyDeath;

    AudioSource sfxSource;

    [SerializeField] private List<AudioClip> clipList;

    private void OnEnable() {
        sfxSource = GetComponent<AudioSource>();
    }

    public void PlayClipByIndex(int i) {
        sfxSource.PlayOneShot(clipList[i]);
    }

    public int Clips() {
        return clipList.Count;
    }

    public string ClipName(int i) {
        return clipList[i].name;
    }

    void Awake() {
        Key.OnPlayerPickupKey += Handle_PlayerPickupKey;
        Player.OnPlayerHealthUpdate += Handle_PlayerHealthUpdated;
        Player.OnPlayerDeath += Handle_PlayerDeath;
        Room.OnRoomCleared += Handle_RoomCleared;
        Room.OnEnteredRoom += Handle_PlayerEnteredRoom;
        MissingShapeUI.OnPlayerUsedBasicMenu += Handle_BasicMenu;
        MissingShapeUI.OnPlayerUsedSelectionMenu += Handle_SelectionMenu;
        MoveableCrate.OnObjectMoved += Handle_ObjectMoved;
        Switch.OnPlayerUsedSwitch += Handle_SwitchUsed;
        Enemy.OnEnemyTookDamage += Handle_EnemyDamage;
        Enemy.OnEnemyDeath += Handle_EnemyDeath;
    }

    private void Handle_EnemyDeath() {
        sfxSource.PlayOneShot(enemyDeath);
    }

    private void Handle_EnemyDamage() {
        sfxSource.PlayOneShot(enemyDamage);
    }

    private void Handle_PlayerDeath() {
        sfxSource.PlayOneShot(playerDeath);
    }

    private void Handle_SwitchUsed() {
        sfxSource.PlayOneShot(@switch);
    }

    private void Handle_ObjectMoved() {
        sfxSource.PlayOneShot(movingObject);
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
