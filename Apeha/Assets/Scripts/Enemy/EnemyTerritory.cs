using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTerritory : MonoBehaviour
{
    public SphereCollider territory;
    GameObject player;
    bool playerInTerritory;

    public GameObject enemy;
    EnemyController EnemyController;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        EnemyController = enemy.GetComponent<EnemyController>();
        playerInTerritory = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInTerritory == true)
        {
            EnemyController.MoveToPlayer();
        }

        if (playerInTerritory == false)
        {
            EnemyController.Rest();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            territory.radius += 4;
            playerInTerritory = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            territory.radius -= 4;
            playerInTerritory = false;
        }
    }
}
