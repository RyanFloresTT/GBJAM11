using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreHandler : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;

    public int Score { get; private set; }

    private void Start() {
        Room.OnRoomCleared += Handle_RoomCleared;
    }

    private void Handle_RoomCleared(RoomData data) {
        Score += CalculateScore(data.Type);
        scoreText.text = Score.ToString();
    }

     public static int CalculateScore(RoomType type) {
        switch (type) {
            case RoomType.Puzzle:
                return 30;
            case RoomType.Encounter:
                return 75;
            case RoomType.Potion:
                return 15;
            default
                : return 0;
        }
    }
}
