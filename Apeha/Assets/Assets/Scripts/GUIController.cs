using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GUIController : MonoBehaviour
{
    public TextMeshProUGUI healthText,coinsText;
    public int health, coins;

    private void Update()
    {
        health = PlayerController.health;
        coins = PlayerController.coins;
        if (health <= 0)
        {
            healthText.text = "Health: 0";
        }
        else
        {
            healthText.text = "Health: " + health.ToString();
        }
        coinsText.text = "Coins: " + coins.ToString();
    }
}
