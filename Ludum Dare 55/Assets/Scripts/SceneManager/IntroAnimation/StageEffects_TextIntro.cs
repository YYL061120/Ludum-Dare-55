using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Image blackoutImage; // ����ɫPanel��Image���
    public float fadeDuration = 3f; // �������ʱ��

    private float elapsedTime = 0f;

    void Start()
    {
        // ������ɫPanel��Alphaֵ����Ϊ1������ȫ��͸��
        blackoutImage.color = new Color(0f, 0f, 0f, 1f);
    }

    void Update()
    {
        // �����û�ﵽĿ��Alphaֵ���������н���
        if (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / fadeDuration);

            // ���㵱ǰAlphaֵ
            float currentAlpha = Mathf.Lerp(1f, 0f, t);

            // ���´���ɫPanel��Alphaֵ
            blackoutImage.color = new Color(0f, 0f, 0f, currentAlpha);
        }
    }
}
