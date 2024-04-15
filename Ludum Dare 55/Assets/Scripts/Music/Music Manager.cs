using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    // Start is called before the first frame update
    private AudioSource audioSource;

    void Start()
    {
        // 获取 AudioSource 组件
        audioSource = GetComponent<AudioSource>();

        // 播放背景音乐
        audioSource.Play();

        // 订阅场景加载完成事件
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // 在场景加载完成时调用的方法
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 停止音乐播放
        audioSource.Stop();
    }
}
