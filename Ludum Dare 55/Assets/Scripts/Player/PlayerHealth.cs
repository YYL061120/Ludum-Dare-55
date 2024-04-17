using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Color flashColor = Color.white; // ��˸��ɫ
    private Material material; // ��ǰ��Ļ�Ĳ���
    private Color originalColor; // ԭʼ��Ļ��ɫ

    public CinemachineVirtualCamera virtualCamera; // Cinemachine Virtual Camera
    public float shakeAmplitude = 0.5f; // �𶯷���

    private CinemachineBasicMultiChannelPerlin noise; // Cinemachine ��Ч��

    public float maxHealth=100f;
    public static float currenthealth;
    public float damage_noLight = 5f;

    public Scrollbar healthBar;

    private static bool _isLighted = true;
    public static bool isLighted
    {
        get
        {
            return _isLighted;
        }
        set
        {
            _isLighted = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        currenthealth = maxHealth;
        noise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        // ��ȡ��Ļ�Ĳ���
        material = GetComponent<Renderer>().material;
        originalColor = material.GetColor("_Color");
    }

    // Update is called once per frame
    void Update()
    {
        isLighted_HealthChecker();

        healthBar.size = currenthealth / maxHealth;
        //Debug.Log("CURRENT HEALTH IS " + currenthealth);
    }

    //player won't get hurt when not lighted
    public void isLighted_HealthChecker()
    {
        if(!isLighted && PlayerDie.damageable && !MPSkill.isSheltered)
        {
            ShakeCamera();
            FlashScreen();
            currenthealth -= damage_noLight * Time.deltaTime;
        }

        else
        {
            StopShaking();
            StopFlashing();
        }
    }

    // ���������Ч��
    public void ShakeCamera()
    {
        noise.m_AmplitudeGain = shakeAmplitude;
    }

    // ֹͣ�����Ч��
    private void StopShaking()
    {
        noise.m_AmplitudeGain = 0f;
    }

    public void FlashScreen()
    {
        material.SetColor("_Color", flashColor);
    }

    private void StopFlashing()
    {
        material.SetColor("_Color", originalColor);
    }
}
