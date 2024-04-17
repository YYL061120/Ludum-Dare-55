using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plants : MonoBehaviour
{
    Animator animator;
    public GameObject climbCollider;

    private bool _canGrow = false;
    public bool canGrow
    {
        get
        {
            return _canGrow;
        }

        set
        {
            _canGrow = value;
            animator.SetBool(AnimationStrings.canGrow, value);
        }
    }

    public bool isWatered;
    public bool isLighting;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        climbCollider.SetActive(false); 
        GameObject plant = gameObject;
        if (plant.CompareTag("watered"))
        {
            isWatered=true;
        }

        if (plant.CompareTag("unwatered"))
        {
            isWatered = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        CanGrowSetter();
        Debug.Log("canGrow: " + canGrow);
    }

    public void CanGrowSetter()
    {
       if(isWatered && isLighting)
        {
            canGrow = true;
        }

       if(canGrow)
        {
            climbCollider.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Drops"))
        {
            isWatered = true;
        }
    }
}
