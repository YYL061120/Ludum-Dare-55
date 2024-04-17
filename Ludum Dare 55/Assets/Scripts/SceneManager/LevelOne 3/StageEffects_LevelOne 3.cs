using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public float startScale = 2f; // ��ʼ��Scaleֵ
    public float targetScale = 4f; // Ŀ���Scaleֵ
    public float transitionDuration = 3f; // ���ɳ���ʱ��

    private float elapsedTime = 0f;
    private bool isTransitioning = false;

    // Start is called before the first frame update
    void Start()
    { 
        // ����Virtual Camera�ĳ�ʼScaleֵ
        virtualCamera.m_Lens.OrthographicSize = startScale;

        MPSkill.canUseSkill1 = true;
        MPSkill.canUseSkill2 = true;

        MPSkill.PlayerSkill1Count = 0;
        MPSkill.PlayerSkill2Count = 0;

        StartTransition();
    }

    // Update is called once per frame
    void Update()
    {
        InitialCameraEffect();
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
    public void StartTransition()
    {
        elapsedTime = 0f;
        isTransitioning = true;
    }
}


