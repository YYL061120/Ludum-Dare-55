using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource audioSource;

    void Start()
    {

    }

    // 在场景加载完成时调用的方法
    public void MusicStop()
    {
        // 停止音乐播放
        audioSource.Stop();
    }
}
