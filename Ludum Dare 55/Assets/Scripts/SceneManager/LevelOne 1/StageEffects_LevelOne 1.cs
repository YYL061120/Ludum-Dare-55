using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class StageEffects_LevelOne1 : MonoBehaviour
{
    public GameObject T1Panel;
    public GameObject T2Panel;
    
    public GameObject T1Panel_Page1;
    public GameObject T2Panel_Page1;
    public GameObject T2Panel_Page2;

    public GameObject goalPanel;

    public GameObject goalPanel_Page1;
    public GameObject goalPanel_Page2;
    public GameObject goalPanel_Page3;

    public AudioSource clickSound;

    public CinemachineVirtualCamera virtualCamera;
    public float startScale = 2f; // 初始的Scale值
    public float targetScale = 4f; // 目标的Scale值
    public float transitionDuration = 3f; // 过渡持续时间

    private float elapsedTime = 0f;
    private bool isTransitioning = false;

    //two tutorials haven't been played
    private bool Tutorial_1_end = false;
    private bool Tutorial_2_end = false;    

    public static bool canShowHealthTutorial=false;
    public bool hasShownT2Page1_start = false;
    public bool hasShownT2Page1_end = false;
    public bool canFinishAll_2 = false;

    public bool canShowGoalPanel = false;

    public int goal_PageNumber = 1;

    public int clickTime = 0;
    private bool haveFinishGoal;

    // Start is called before the first frame update
    void Start()
    {

        // 设置Virtual Camera的初始Scale值
        virtualCamera.m_Lens.OrthographicSize = startScale;

        StartTransition();

        //Player can not move until they have been told the goal of the game
        PlayerController.canMove = false;


        //StartCoroutine(waitT1());
    }

    // Update is called once per frame
    void Update()
    {
        // 如果正在过渡中，则更新Scale值
        if (isTransitioning)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / transitionDuration);
            float newScale = Mathf.Lerp(startScale, targetScale, t);

            // 更新Virtual Camera的Scale值
            virtualCamera.m_Lens.OrthographicSize = newScale;

            // 如果过渡完成，则停止过渡
            if (t >= 1f)
            {
                isTransitioning = false;
                canShowGoalPanel = true;
            }
        }

        if (!isTransitioning && !haveFinishGoal)
        {
            ShowGoalPanel();
        }

        if (canShowHealthTutorial)
        {
            if (hasShownT2Page1_start==false)
            {
                hasShownT2Page1_start = true;
                StartCoroutine(waitT2());
            }
        }

    }

    // 开始过渡到目标Scale值
    public void StartTransition()
    {
        elapsedTime = 0f;
        isTransitioning = true;
    }

    //Health Tutorial
    public IEnumerator waitT2()
    {
        yield return new WaitForSeconds(0.5f);
        T2Panel.SetActive(true);
        T2Panel_Page1.SetActive(true);
        T2Panel_Page2.SetActive(false);
        PlayerDie.damageable = false;
        PlayerController.canMove = false;
    }

    public void FinishT2_Page1()
    {
        if (hasShownT2Page1_end == false)
        {
            Debug.Log("Enter 1");
            T2Panel_Page1.SetActive(false);
            T2Panel_Page2.SetActive(true);
            StartCoroutine(SetFinishAll_2());
        }
    }

    public void FinishAll_2()
    {
        if (canFinishAll_2)
        {
            PlayerController.canMove = true;
            T2Panel.SetActive(false);
            PlayerDie.damageable = true;
        }
    }

    public IEnumerator SetFinishAll_2()
    {
        yield return new WaitForSeconds(0.2f);
        canFinishAll_2 = true;
    }


    //Show Goal Panel
    public void ShowGoalPanel()
    {
        goalPanel.SetActive(true);
        canShowGoalPanel = false;
    }


    public void GoalPageCount()
    {
        goal_PageNumber += 1;
    }

    public void FinishGoal_Page1()
    {
        clickSound.Play();
        if(goal_PageNumber == 0) 
        {
            goalPanel_Page1.SetActive(false);
            goalPanel_Page2.SetActive(true);
        }
    }

    public void FinishGoal_Page2()
    {
        clickSound.Play();

        if (goal_PageNumber == 1)
        {
            goalPanel_Page2.SetActive(false);
            goalPanel_Page3.SetActive(true);
        }
    }

    public void FinishGoal_Page3()
    {
        clickSound.Play();

        if (goal_PageNumber == 2)
        {
            haveFinishGoal = true;
            MusicManager.canPlayBackgroundMusic = true;
            goalPanel.SetActive(false);
            PlayerController.canMove = true;
        }
    }







    //Turorial_Move

    //public IEnumerator waitT1()
    //{
    //    canShowHealthTutorial = false;
    //    yield return new WaitForSeconds(1f);
    //    T1Panel.SetActive(true);
    //    T1Panel_Page1.SetActive(true);
    //    PlayerController.canMove = false;
    //}

    //public void FinishT1_Page1()
    //{
    //    T1Panel_Page1.SetActive(false);
    //}

    //public void FinishAll_1()
    //{
    //    PlayerController.canMove = true;
    //    T1Panel.SetActive(false);
    //    StartTransition();
    //}
}
