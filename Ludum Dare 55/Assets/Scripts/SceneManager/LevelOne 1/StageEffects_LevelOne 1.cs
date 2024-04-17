using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageEffects_LevelOne1 : MonoBehaviour
{
    public GameObject T1Panel;
    public GameObject T2Panel;
    
    public GameObject T1Panel_Page1;
    public GameObject T2Panel_Page1;
    public GameObject T2Panel_Page2;

    public CinemachineVirtualCamera virtualCamera;
    public float startScale = 2f; // ��ʼ��Scaleֵ
    public float targetScale = 4f; // Ŀ���Scaleֵ
    public float transitionDuration = 3f; // ���ɳ���ʱ��

    private float elapsedTime = 0f;
    private bool isTransitioning = false;

    //two tutorials haven't been played
    private bool Tutorial_1_end = false;
    private bool Tutorial_2_end = false;    

    public static bool canShowHealthTutorial=false;
    public bool hasShownT2Page1_start = false;
    public bool hasShownT2Page1_end = false;
    public bool canFinishAll_2 = false;

    public int clickTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        // ����Virtual Camera�ĳ�ʼScaleֵ
        virtualCamera.m_Lens.OrthographicSize = startScale;

        StartCoroutine(waitT1());
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(canShowHealthTutorial);
        // ������ڹ����У������Scaleֵ
        if (isTransitioning)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / transitionDuration);
            float newScale = Mathf.Lerp(startScale, targetScale, t);

            // ����Virtual Camera��Scaleֵ
            virtualCamera.m_Lens.OrthographicSize = newScale;

            // ���������ɣ���ֹͣ����
            if (t >= 1f)
            {
                isTransitioning = false;
            }

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

    // ��ʼ���ɵ�Ŀ��Scaleֵ
    public void StartTransition()
    {
        elapsedTime = 0f;
        isTransitioning = true;
    }


    public IEnumerator waitT1()
    {
        canShowHealthTutorial = false;
        yield return new WaitForSeconds(1f);
        T1Panel.SetActive(true);
        T1Panel_Page1.SetActive(true);
        PlayerController.canMove = false;
    }

    public void FinishT1_Page1()
    {
        T1Panel_Page1.SetActive(false);
    }

    public IEnumerator waitT2()
    {
        yield return new WaitForSeconds(0.5f);
        T2Panel.SetActive(true);
        T2Panel_Page1.SetActive(true);
        T2Panel_Page2.SetActive(false);
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

    public void FinishAll_1()
    {
        PlayerController.canMove = true;
        T1Panel.SetActive(false);
        StartTransition();
    }

    public void FinishAll_2()
    {
        if (canFinishAll_2)
        {
            PlayerController.canMove = true;
            T2Panel.SetActive(false);
        }
    }

    public IEnumerator SetFinishAll_2()
    {
        yield return new WaitForSeconds(0.2f);
        canFinishAll_2 = true;
    }
}
