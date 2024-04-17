using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Image blackoutImage; // 纯黑色Panel的Image组件
    public float fadeDuration = 3f; // 渐变持续时间

    private float elapsedTime = 0f;

    void Start()
    {
        // 将纯黑色Panel的Alpha值设置为1，即完全不透明
        blackoutImage.color = new Color(0f, 0f, 0f, 1f);
    }

    void Update()
    {
        // 如果还没达到目标Alpha值，继续进行渐变
        if (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / fadeDuration);

            // 计算当前Alpha值
            float currentAlpha = Mathf.Lerp(1f, 0f, t);

            // 更新纯黑色Panel的Alpha值
            blackoutImage.color = new Color(0f, 0f, 0f, currentAlpha);
        }
    }
}
