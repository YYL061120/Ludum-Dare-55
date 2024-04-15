using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class System_Logic : MonoBehaviour
{
    public GameObject Panel_Die;

    // Start is called before the first frame update
    void Start()
    {
        Panel_Die.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
/*        Debug.Log("canMove: " + PlayerController.canMove);
        Debug.Log("canUseSkill2: " + MPSkill.canUseSkill2);*/
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
        MPSkill.canUseSkill2 = true;
    }
}
        