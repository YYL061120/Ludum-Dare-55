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
            if (isOnGround)
            {
                _playerMoveSpeed = walkSpeed;
            }
            else
            {
                _playerMoveSpeed = airSpeed;
            }
        }
    }


    public bool canMove=true;

    public bool isOnGround;

    private bool _isMoving;
    public bool isMoving
    {
        get { return _isMoving; }
        set 
        {
            _isMoving = value;
            animator.SetBool("isMoving", value);
        }
    }



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move(canMove);

        IsOnGround();

        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            OnJump();
        }

        Debug.Log(isMoving);
        Debug.Log("isOnGround is " + isOnGround);
    }

    public void Move(bool _canMove)
    {
        float playerInputDirection = Input.GetAxis("Horizontal");

        //return whether player is moving
        if(playerInputDirection != 0) 
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
        if(playerInputDirection != 0f)
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

    public bool IsOnGround()
    {
        isOnGround = Physics2D.Raycast(groundCheck.position, Vector2.down, 0.25f, groundLayer);
        Debug.DrawRay(groundCheck.position, Vector2.down * 0.25f, Color.green);
        return isOnGround;
    }


/*    public Vector2 PlayerBottomPosition()
    {
        Vector2 playerSize = GetComponent<SpriteRenderer>().bounds.size;
        Vector2 playerBottom=new Vector2(transform.position.x, transform.position.y-playerSize.y/2);
        return playerBottom;
    }*/
}
