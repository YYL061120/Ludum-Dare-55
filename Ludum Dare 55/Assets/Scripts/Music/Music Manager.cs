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

    // �ڳ����������ʱ���õķ���
    public void MusicStop()
    {
        // ֹͣ���ֲ���
        audioSource.Stop();
    }
}
