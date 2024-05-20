using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Destination_NextScene : MonoBehaviour
{
    BoxCollider2D boxCol;
    public Animator animator;
    public string currentSceneName;

    // Start is called before the first frame update
    void Start()
    {
        boxCol = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        animator.SetBool("canLoad", false);
    }

    // Update is called once per frame
    void Update()
    {
        currentSceneName = MusicManager.GetCurrentScene();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            animator.SetBool("canLoad",true); 
            StartCoroutine(Loading());
        }
    }

    IEnumerator Loading()
    {
        PlayerController.canMove = false;

        MusicManager.canPlayLevelPassMusic = true;

        yield return new WaitForSeconds(4f);

        screenFading.canFade = true;
        screenFading.fadeIn = true;

        StartCoroutine(SwitchScene());
    }
    
    IEnumerator SwitchScene()
    {
        yield return new WaitForSeconds(2.1f);

        switch (currentSceneName)
        {
            case "LevelOne 1":
                SceneManager.LoadSceneAsync("LevelOne 2");
                animator.SetBool("canLoad", false);
                break;
            case "LevelOne 2":
                SceneManager.LoadSceneAsync("LevelOne 3");
                animator.SetBool("canLoad", false);
                break;
            case "LevelOne 3":
                SceneManager.LoadSceneAsync("Victory");
                animator.SetBool("canLoad", false);
                break;
        }
    }
}
