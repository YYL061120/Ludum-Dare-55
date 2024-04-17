using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class System_Logic : MonoBehaviour
{
    public GameObject Panel_Die;
    Scene currentScene;

    // Start is called before the first frame update
    void Start()
    {
        Panel_Die.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        currentScene = SceneManager.GetActiveScene();

        SceneLimitation();
    }

    public static void GameStopping()
    {
        PlayerController.canMove = false;
        MPSkill.canUseSkill2 = false;
    }

    public void SceneRestartLevel1()      
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        PlayerController.canMove = true;
    }

    public void SceneRestratLevel2()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        PlayerController.canMove = true;
    }

    public void SceneLimitation()
    {
        switch (currentScene.name) 
        {
            case "LevelOne 1":
                MPSkill.canUseSkill2 = false;
                MPSkill.canUseSkill1 = false;
                break;
            case "LevelOne 2":
                break;
        }
    }
}
        