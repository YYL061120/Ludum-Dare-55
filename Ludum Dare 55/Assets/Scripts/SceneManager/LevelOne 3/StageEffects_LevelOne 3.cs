using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public float startScale = 2f; // 初始的Scale值
    public float targetScale = 4f; // 目标的Scale值
    public float transitionDuration = 3f; // 过渡持续时间

    private float elapsedTime = 0f;
    private bool isTransitioning = false;

    // Start is called before the first frame update
    void Start()
    { 
        // 设置Virtual Camera的初始Scale值
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
            }
        }
    }
    public void StartTransition()
    {
        elapsedTime = 0f;
        isTransitioning = true;
    }
}


