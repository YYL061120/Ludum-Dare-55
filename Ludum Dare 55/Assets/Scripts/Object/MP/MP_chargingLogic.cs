using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MP_chargingLogic : MonoBehaviour
{
    Animator animator;
    public GameObject player;

    public float chargingRange=1f;
    public float chargingTime = 3f;

    private bool _isCharged=false;

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
            if (Input.GetKeyDown(KeyCode.E) && isCharged == false)
            {
                PlayerController.canMove = false;
                isCharging = true;
                isCharged = true;
                PlayerDie.damageable = false;
                StartCoroutine(ChargingSkillPoints());
            }
        }

        Debug.Log("PlayerSkill1Count: " + MPSkill.PlayerSkill1Count);
        Debug.Log("PlayerSkill1Count: " + MPSkill.PlayerSkill2Count);
    }

    public IEnumerator ChargingSkillPoints()
    {
        yield return new WaitForSeconds(chargingTime);
        PlayerDie.damageable = true;
        PlayerController.canMove = true;
        MPSkill.PlayerSkill1Count += 1;
        MPSkill.PlayerSkill2Count += 1;
    }
}
