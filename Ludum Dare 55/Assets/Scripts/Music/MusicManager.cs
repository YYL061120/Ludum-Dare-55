using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager: MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource backgroundMusic;
    public AudioSource levelPassMusic;

    public static bool canPlayBackgroundMusic = false;
    public static bool canPlayLevelPassMusic;

    public static bool havePlayLevelPassMusic = false;
    public static bool havePlayBackgroundMusic = false;

    private string currentScene;
    void Start()
    {
        canPlayLevelPassMusic = false;
    }

    private void Update()
    {
        PlayBackgroundMusic();

        PlayLevelPassMusic();
    }

    //get the name of currentScene
    public static string GetCurrentScene()
    {
        Scene currentScene=SceneManager.GetActiveScene();
        string name = currentScene.name;
        return name;
    }

    public void PlayBackgroundMusic()
    {
        currentScene = GetCurrentScene();

        switch (currentScene)
        {
            case "LevelOne 1":
                if (!havePlayBackgroundMusic)
                {
                    if (canPlayBackgroundMusic)
                    {
                        backgroundMusic.Play();
                        havePlayBackgroundMusic = true;
                    }
                }
                break;

            case "LevelOne 2":
                if (!havePlayBackgroundMusic)
                {
                    if (canPlayBackgroundMusic)
                    {
                        backgroundMusic.Play();
                        havePlayBackgroundMusic = true;
                    }
                }
                break;

            case "LevelOne 3":
                break;
        }
    }

    public void PlayLevelPassMusic()
    {
        if (!havePlayLevelPassMusic)
        {
            if (canPlayLevelPassMusic)
            {
                levelPassMusic.Play();
                havePlayLevelPassMusic = true;
            }
        }
    }
}
