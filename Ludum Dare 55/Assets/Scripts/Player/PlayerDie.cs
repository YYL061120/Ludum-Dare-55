using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerDie : MonoBehaviour
{
    public GameObject Panel_FallingDie;
    public GameObject Panel_NoHealthDie;
    Animator animator;

    private static bool _damageable = true;
    public static bool damageable
    {
        get
        {
            return _damageable;
        }

        set
        {
            _damageable = value;
        }
    }

    private bool _isDead = false;
    public bool isDead
    {
        get { return _isDead; }
        set
        {
            _isDead = value;
            animator.SetBool(AnimationStrings.isDying,value);
        }
    }

    private bool _isAlive = true;
    public bool isAlive
    {
        get { return _isAlive; }
        set
        {
            _isDead = value;
            animator.SetBool(AnimationStrings.isAlive, value);
        }
    }

    public float waitingTime_NoHealthDie = 2.5f;
    public float deathHeight = -11f;
    // Start is called before the first frame update
    void Start()
    {
        animator=GetComponent<Animator>();

        //Set Player Alive state everytime when the game starts
        isAlive = true;
        isDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        //falling die
        if (transform.position.y<deathHeight)
        {
            FallingDie();
        }

        //noHealth die
        if (PlayerHealth.currenthealth <= 0)
        {
            isDead = true;
            isAlive = false;
            PlayerController.canMove = false;
            MPSkill.canUseSkill1 = false;
            MPSkill.canUseSkill2 = false;
            StartCoroutine(NoHealthDie());
        }
    }

    public void FallingDie()
    {
        Panel_FallingDie.SetActive(true);
        MPSkill.canUseSkill1 = false;
        MPSkill.canUseSkill2 = false;
    }

    public IEnumerator NoHealthDie()
    {
        yield return new WaitForSeconds(waitingTime_NoHealthDie);
        Panel_NoHealthDie.SetActive(true);
    }
}
