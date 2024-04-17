using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;

    public LayerMask groundLayer;
    public Transform groundCheck;


    [SerializeField]
    public float walkSpeed = 3f;
    public float airSpeed = 2f;
    public float jumpInpulse = 4f;


    private float _playerMoveSpeed = 3f;
    public float playerMoveSpeed
    {
        get { return _playerMoveSpeed; }
        set
        {
            playerMoveSpeed = value;
        }
    }


    public static bool canMove=true;




    private bool _isOnGround;
    public bool isOnGround
    {
        get
        {
            return _isOnGround; 
        }
        set
        {
            _isOnGround = value;
            animator.SetBool(AnimationStrings.isOnGround, value);
        }
    }

    private bool _isMoving;

    public bool isMoving
    {
        get { return _isMoving; }
        set 
        {
            _isMoving = value;
            animator.SetBool(AnimationStrings.isMoving, value);
        }
    }



    // Start is called before the first frame update
    void Start()
    {
        canMove = true;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat(AnimationStrings.yVelocity, rb.velocity.y);

        Move(canMove);
        PlayerMoveSpeedChanger();

        IsOnGround_Player();

        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && canMove)
        {
            OnJump();
        }
    }

    public void Move(bool _canMove)
    {
        float playerInputDirection = Input.GetAxis("Horizontal");

        //return whether player is moving
        if(playerInputDirection != 0 && canMove) 
        {
            isMoving = true;
        }

        else
        {
            isMoving = false;
        }

        //Initially, check whether player can move
        if (_canMove)
        {
            Vector2 PlayerMovingVector = new Vector3(playerInputDirection, 0f, 0f);
            transform.Translate(PlayerMovingVector * playerMoveSpeed * Time.deltaTime);
        }

        //flip direction according to the movement input of player
        if(playerInputDirection != 0f && canMove)
        {
            if (playerInputDirection > 0)
            {
                transform.localScale = new Vector3(-1f, 1f, transform.localScale.z*1f);
            }

            else
            {
                transform.localScale = new Vector3(1f, 1f, transform.localScale.z*1f);
            }
        }
    }


    public void OnJump()
    {
        if (isOnGround)
        {
            rb.velocity = new Vector2(0f, jumpInpulse);
        }
    }

    public bool IsOnGround_Player()
    {
        isOnGround = Physics2D.Raycast(groundCheck.position, Vector2.down, 0.5f, groundLayer);
        Debug.DrawRay(groundCheck.position, Vector2.down * 0.25f, Color.green);
        return isOnGround;
    }

    public void PlayerMoveSpeedChanger()
    {
        if (isOnGround)
        {
            _playerMoveSpeed = walkSpeed;
        }
        else
        {
            _playerMoveSpeed = airSpeed;
        }
    }

/*    public Vector2 PlayerBottomPosition()
    {
        Vector2 playerSize = GetComponent<SpriteRenderer>().bounds.size;
        Vector2 playerBottom=new Vector2(transform.position.x, transform.position.y-playerSize.y/2);
        return playerBottom;
    }*/
}
