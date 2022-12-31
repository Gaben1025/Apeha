using UnityEngine;
using System.Collections;
using System.Data;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5.0f;
    [SerializeField] public float rotationSpeed;
    [SerializeField] private int attackDamage = 1;
    [SerializeField] public static int health = 100;
    [SerializeField] private float timeBetweenAttacks = .5f;

    public static int coins = 0;

    private GameObject attackArea = default;
    private bool attacking = false;
    private float timer = 0f;
    private float y, sensitivity = -2.5f;

    private Vector3 rotate;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        attackArea = transform.GetChild(0).gameObject;
        attackArea.SetActive(attacking);
    }

    private void Update()
    {
        if (health <= 0) //Die
        {
            PlayerDie();
        }
        else //Play Game
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Attack();
            }

            if (attacking)
            {
                timer += Time.deltaTime;
                //Sends message to set the current damage
                BroadcastMessage("SetDamage", attackDamage);
                //Turns off attacks while time less than attack speed
                if (timer >= timeBetweenAttacks)
                {
                    timer = 0;
                    attacking = false;
                    attackArea.SetActive(attacking);
                }

            }
            //Player rotation
            y = Input.GetAxis("Mouse X");
            //Player movment
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");

   
            Vector3 movementDirection = new Vector3(horizontal, 0, vertical);

            transform.Translate(movementDirection * speed * Time.deltaTime);
            rotate = new Vector3(0, y * sensitivity, 0);
            transform.eulerAngles = transform.eulerAngles - rotate;

        }
    }
    void Attack ()
    {
        attacking = true;
        attackArea.SetActive(attacking);
    }
    public void PickupCoin()
    {
        coins++;
    }
    public void PickupHealth()
    {
        health+=20;
    }
    private void PlayerDie()
    {
        
    }
}
