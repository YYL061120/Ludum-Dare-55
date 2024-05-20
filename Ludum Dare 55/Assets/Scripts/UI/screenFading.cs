using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class screenFading : MonoBehaviour
{
    public Image fadeImage; // 拖拽黑色 Image 到这里
    public float fadeDuration = 2f; // 渐变持续时间
    public static bool fadeIn; // 控制是渐黑还是渐亮

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

        // 确保最后完全黑色
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

        // 确保最后完全透明
        color.a = 0f;
        fadeImage.color = color;

    }
}
