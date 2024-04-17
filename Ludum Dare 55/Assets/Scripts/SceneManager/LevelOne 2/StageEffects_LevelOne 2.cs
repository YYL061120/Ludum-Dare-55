using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageEffects_LevelOne2 : MonoBehaviour
{
    public GameObject T1_jump;
    public GameObject T2_stone;
    public GameObject MPSkill2Bar;
    public GameObject MPSkill1Bar;
    public GameObject MPSkill1Name;
    public GameObject MPSkill2Name;

    public CinemachineVirtualCamera virtualCamera;
    public float startScale = 2f; // ��ʼ��Scaleֵ
    public float targetScale = 4f; // Ŀ���Scaleֵ
    public float transitionDuration = 3f; // ���ɳ���ʱ��

    private float elapsedTime = 0f;
    private bool isTransitioning = false;

    public static bool canShowStoneTu=false;
    public bool hasShownStoneTu = false;
    // Start is called before the first frame update
    void Start()
    { 
        T1_jump.SetActive(false);
        T2_stone.SetActive(false);

        // ����Virtual Camera�ĳ�ʼScaleֵ
        virtualCamera.m_Lens.OrthographicSize = startScale;

        StartCoroutine(SetActive_jumpTutorial());
        MPSkill.canUseSkill1 = false;
        MPSkill.canUseSkill2 = false;
        
        MPSkill.PlayerSkill1Count = 0;
        MPSkill.PlayerSkill2Count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (canShowStoneTu) 
        {
            if (hasShownStoneTu == false)
            {
                PlayerController.canMove = false;
                T2_stone.SetActive(true);
                hasShownStoneTu=true;
            }
        }


        if(hasShownStoneTu)
        {
            MPSkill1Bar.SetActive(true);
            MPSkill2Bar.SetActive(true);
            MPSkill1Name.SetActive(true);
            MPSkill2Name.SetActive(true);
            MPSkill.canUseSkill2 = true;
            MPSkill.canUseSkill1 = true;
        }

        InitialCameraEffect();
    }

    public void StartTransition()
    {
        elapsedTime = 0f;
        isTransitioning = true;
    }

    public IEnumerator SetActive_jumpTutorial()
    {
        yield return new WaitForSeconds(1f);
        PlayerController.canMove = false;
        T1_jump.SetActive(true);   
    }

    public void FinishT1_jump()
    {
        T1_jump.SetActive(false);
        StartTransition();
        PlayerController.canMove = true;
    }

    public void FinishT2_stone()
    {
        canShowStoneTu = false;
        T2_stone.SetActive(false );
        PlayerController.canMove = true;
    }

    public void InitialCameraEffect()
    {
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
    }
}
