using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            PlayerController player = other.GetComponent<PlayerController>();

            Debug.Log("Coin Pickup");
            player.PickupCoin();
           
            Destroy(this.gameObject);
        }
    }
}
