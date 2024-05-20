using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class screenFading : MonoBehaviour
{
    public Image fadeImage; // ��ק��ɫ Image ������
    public float fadeDuration = 2f; // �������ʱ��
    public static bool fadeIn; // �����ǽ��ڻ��ǽ���

    public static bool canFade=false;

    private void Start()
    {

    }

    private void Update()
    {
        if (canFade)
        {
            if (fadeIn)
            {
                canFade = false;
                StartCoroutine(FadeToBlack());
            }
            else
            {
                canFade = false;
                StartCoroutine(FadeToClear());
            }
        }
    }

    private IEnumerator FadeToBlack()
    {
        Debug.Log("Fade to Black");
        float elapsedTime = 0f;
        Color color = fadeImage.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsedTime / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }

        // ȷ�������ȫ��ɫ
        color.a = 1f;
        fadeImage.color = color;
    }

    private IEnumerator FadeToClear()
    {
        Debug.Log("Enter Clear");
        float elapsedTime = 0f;
        Color color = fadeImage.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(1f - (elapsedTime / fadeDuration));
            fadeImage.color = color;
            yield return null;
        }

        // ȷ�������ȫ͸��
        color.a = 0f;
        fadeImage.color = color;

    }
}
