using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Destination_NextScene : MonoBehaviour
{
    BoxCollider2D boxCol;
    Scene currentScene;

    // Start is called before the first frame update
    void Start()
    {
        boxCol = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        currentScene = SceneManager.GetActiveScene();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            switch(currentScene.name)
            {
                case "LevelOne 1":
                    SceneManager.LoadSceneAsync("LevelOne 2");
                    break;
                case "LevelOne 2":
                    SceneManager.LoadSceneAsync("LevelOne 3");
                    break;
            }
        }
    }
}
