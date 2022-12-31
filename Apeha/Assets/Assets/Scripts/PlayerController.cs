using UnityEngine;
using System.Collections;
using System.Data;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class PlayerController : MonoBehaviour
{
    Animator anim;

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
    int isMovingForwardHash, isMovingBackwardHash, isMovingRightHash, isMovingLeftHash, isDeadForwardHash, isDeadBackwardHash;
    int isAttackingHash;
    bool isMovingForward, isMovingBackward, isMovingRight, isMovingLeft, isAttacking;
    bool movingForward, movingBackward, movingLeft,movingRight;
    void Start()
    {
        anim = GetComponent<Animator>();
        isDeadForwardHash = Animator.StringToHash("isDeadForward");
        isDeadBackwardHash = Animator.StringToHash("isDeadBackward");
        isMovingForwardHash = Animator.StringToHash("isMovingForward");
        isMovingBackwardHash = Animator.StringToHash("isMovingBackward");
        isMovingRightHash = Animator.StringToHash("isMovingRight");
        isMovingLeftHash = Animator.StringToHash("isMovingLeft");
        isAttackingHash = Animator.StringToHash("isAttacking");

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
            isAttacking = anim.GetBool(isAttackingHash);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                anim.SetBool(isAttackingHash, true);
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
                    anim.SetBool(isAttackingHash, false);
                }

            }
            //Player rotation
            y = Input.GetAxis("Mouse X");
            //Player movment
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");

            Vector3 movementDirection = new Vector3(horizontal, 0, vertical);

            CheckMovmentAnim(horizontal, vertical);

            transform.Translate(movementDirection * speed * Time.deltaTime);
            rotate = new Vector3(0, y * sensitivity, 0);
            transform.eulerAngles = transform.eulerAngles - rotate;

        }
    }
    void CheckMovmentAnim(float horizontal, float vertical)
    {
        movingForward = horizontal == 0 && vertical == 1;
        movingBackward = horizontal == 0 && vertical == -1;
        movingLeft = horizontal == -1 && vertical == 0;
        movingRight = horizontal == 1 && vertical == 0;

        if (movingLeft)
        {
            anim.SetBool(isMovingLeftHash, true);
        }
        else
        {
            anim.SetBool(isMovingLeftHash, false);
        }

        if (movingForward)
        {
            anim.SetBool(isMovingForwardHash, true);
        }
        else
        {
            anim.SetBool(isMovingForwardHash, false);
        }

        if (movingBackward)
        {
            anim.SetBool(isMovingBackwardHash, true);
        }
        else
        {
            anim.SetBool(isMovingBackwardHash, false);
        }

        if (movingRight)
        {
            anim.SetBool(isMovingRightHash, true);
        }
        else
        {
            anim.SetBool(isMovingRightHash, false);
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
        //Reset all animations to false
        anim.SetBool(isAttackingHash, false);
        anim.SetBool(isMovingLeftHash, false);
        anim.SetBool(isMovingForwardHash, false);
        anim.SetBool(isMovingBackwardHash, false);
        anim.SetBool(isMovingBackwardHash, false);
        anim.SetBool(isMovingRightHash, false);
        // 50/50 of either of these animations
        if (Random.Range(0, 2) != 0)
        {
            anim.SetBool(isDeadForwardHash, true);
        }
        else 
        {
            anim.SetBool(isDeadBackwardHash, true);
        }
    }
}
