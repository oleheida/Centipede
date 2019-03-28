using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthDisplay : MonoBehaviour {


    PlayerScript player;
    TextMeshProUGUI healthText;

    void Start ()
    {
        healthText = GameObject.Find("Health Text").GetComponent<TextMeshProUGUI>(); //gets an object of text
        player = FindObjectOfType<PlayerScript>();
    }

	void Update () {
        healthText.text = player.GetHealth().ToString(); //displays health
    }
}
