using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MP_chargingLogic : MonoBehaviour
{
    Animator animator;
    public GameObject player;
    public GameObject Text;
    public AudioSource chargingSFX;

    public GameObject MP2Tu;

    private static int chargingIntervals = 0;
    private static bool haveShownMP2Tu = false;

    public float chargingRange=1f;
    public float chargingTime = 3f;

    private bool _isCharged=false;
    private bool havePlayChargingSFX = false;

    public bool isCharged
    {
        get { return _isCharged; }
        set { _isCharged = value;
            animator.SetBool(AnimationStrings.isCharged, value);
        }
    }

    private bool _isCharging = false;
    public bool isCharging
    {
        get { return _isCharging; }
        set { _isCharging = value;
            animator.SetBool(AnimationStrings.isCharging, value);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector2.Distance(player.transform.position, transform.position);

        if (distanceToPlayer <= chargingRange)
        {
            Text.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E) && isCharged == false)
            {
                PlayerController.canMove = false;
                isCharging = true;
                isCharged = true;
                PlayerDie.damageable = false;

                if(!havePlayChargingSFX)
                {
                    chargingSFX.Play();
                    havePlayChargingSFX = true;
                }

                StartCoroutine(ChargingSkillPoints());
            }

            //MP2 tutorial is shown after player has finished the first charge
            if (chargingIntervals == 1)
            {
                if(haveShownMP2Tu == false)
                {
                    MP2TuShown();
                    haveShownMP2Tu = true;
                }
            }
        }
        
        else
        {
            Text.SetActive(false);
        }

        //Debug.Log("PlayerSkill1Count: " + MPSkill.PlayerSkill1Count);
        //Debug.Log("PlayerSkill1Count: " + MPSkill.PlayerSkill2Count);
    }

    public IEnumerator ChargingSkillPoints()
    {
        yield return new WaitForSeconds(chargingTime);
        PlayerDie.damageable = true;
        PlayerController.canMove = true;
        MPSkill.PlayerSkill1Count += 1;
        MPSkill.PlayerSkill2Count += 1;
        chargingIntervals += 1;
    }

    //MP2 tutorial shows and closes
    public void MP2TuShown()
    {
        PlayerController.canMove = false;
        PlayerDie.damageable = false;
        MPSkill.canUseSkill1 = false;
        MPSkill.canUseSkill2 = false;
        MP2Tu.SetActive(true);
    }
    public void FinishMP2Tu()
    {
        PlayerController.canMove = true;
        PlayerDie.damageable = true;
        StartCoroutine(MPSkill.DelayEnableMPUsing());
        MP2Tu.SetActive(false);
    }
}
