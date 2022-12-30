using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackArea : MonoBehaviour
{
    public SphereCollider attackArea;
    private int attackDamage;
    public void SetDamage(int damage)
    {
        attackDamage = damage;
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.GetComponent<EnemyController>() != null)
        {
            EnemyController enemy = collider.GetComponent<EnemyController>();
            enemy.TakeDamage(attackDamage);
        }
    }
}
