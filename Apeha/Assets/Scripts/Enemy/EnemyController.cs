using UnityEngine;
using UnityEngine.AI;
using System.Linq;
using Unity.VisualScripting;

public class EnemyController : MonoBehaviour
{
    public GameObject coinPrefab;
    public GameObject healthPrefab;
    public Transform target;
    [SerializeField] private float EnemySpeed = 3f;
    [SerializeField] private int health = 100;
    [SerializeField] private float attackRange = 3f;
    [SerializeField] private int attackDamage = 1;
    [SerializeField] private float timeBetweenAttacks = 2f;

    float timeOfLastAttack = 0f;

    // Use this for initialization
    void Start()
    {
        Rest();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MoveToPlayer()
    {
        if (health <= 0)
        {
            Die();
        }
        else
        {
            if (Vector3.Distance(transform.position, target.position) <= attackRange)
            {
                Debug.Log("Player in range of attack");

                if (Time.time - timeOfLastAttack > timeOfLastAttack)
                {
                    timeOfLastAttack = Time.time;
                    Attack();
                }

            }
            //rotate to look at player
            transform.LookAt(target.position);
            transform.Rotate(new Vector3(0, -90, 0), Space.Self);

            //move towards player
            transform.Translate(new Vector3(EnemySpeed * Time.deltaTime, 0, 0));
        }
    }

    public void Attack()
    {
       PlayerController.health -= attackDamage;
    }
    public void TakeDamage(int damage)
    {
        Debug.Log("Damage Taken");
        health -= damage;
    }
    public void Die()
    {
        Vector3 deathPos = this.gameObject.transform.position;
        if (Random.Range(0, 100) > 70) //%30 chance a health pot drops 
        {
            Instantiate(healthPrefab, deathPos, Quaternion.identity);
        }
        else
        {
            Instantiate(coinPrefab, deathPos, Quaternion.Euler(-90, -25, 30));
        }
        Destroy(this.gameObject);
    }
    public void Rest()
    {

    }
}