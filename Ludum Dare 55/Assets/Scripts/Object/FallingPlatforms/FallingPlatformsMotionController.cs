using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class FallingPlatformsMotionController : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer spriteRenderer_FallingPlatforms;

    //private Color OriginalColor;

    public float fadedSpeed=0.25f;

    public bool isOnGround_fallingPlatforms;

    public bool isFalling=false;

    public bool isBreaking = false;

    public float waitingTime = 0.35f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer_FallingPlatforms = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isBreaking)
        {
            animator.SetBool("isBreaking", true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            StartCoroutine(Onfalling());
        }

        if (collision.collider.CompareTag("Ground"))
        {
            isFalling = false;
            rb.bodyType = RigidbodyType2D.Kinematic;
            isBreaking = true; 
            StartCoroutine(Disappear());
        }
    }

    public IEnumerator Onfalling()
    {
        yield return new WaitForSeconds(waitingTime);
        rb.bodyType = RigidbodyType2D.Dynamic;
        isFalling = true;
    }

    public IEnumerator Disappear()
    {
        yield return new WaitForSeconds(0.75f);
        Destroy(gameObject);
    }

}
