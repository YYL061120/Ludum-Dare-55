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
        // ��ȡ AudioSource ���
        audioSource = GetComponent<AudioSource>();

        // ���ű�������
        audioSource.Play();

        // ���ĳ�����������¼�
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // �ڳ����������ʱ���õķ���
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // ֹͣ���ֲ���
        audioSource.Stop();
    }
}
