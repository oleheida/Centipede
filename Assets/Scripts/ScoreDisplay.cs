using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour {

    TextMeshProUGUI scoreText;
    GameSession gameSession;
    

    private void Start()
    {
        scoreText = GameObject.Find("Score Text").GetComponent<TextMeshProUGUI>(); //gets an object of text
        gameSession = FindObjectOfType<GameSession>();
        
    }
    private void Update()
    {
        scoreText.text = gameSession.GetScore().ToString(); //displays score
    }
}
