using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageEffects_GeneralIntro : MonoBehaviour
{
    public GameObject Panel1;
    public GameObject Panel2;
    public GameObject Panel3;
    public GameObject Panel4;
    public GameObject Panel5;

    public Animator animator3;
    public Animator animator4;

    public AudioSource backgroundMusic;
    // Start is called before the first frame update
    void Start()
    {
        InitialPanelSetter(); 
    }

    // Update is called once per frame
    void Update()
    {
        // Frame information for Panel3
        AnimatorStateInfo stateInfo3 = animator3.GetCurrentAnimatorStateInfo(0);

        //Animation of Panel3 is done
        if (stateInfo3.normalizedTime >= 1f)
        {
            Panel3.SetActive(false);
            Panel4.SetActive(true);
        }

        // Frame information for Panel4
        AnimatorStateInfo stateInfo4 = animator4.GetCurrentAnimatorStateInfo(0);

        //Animation of Panel3 is done
        if (stateInfo4.normalizedTime >= 1f)
        {
            Panel4.SetActive(false);
            Panel5.SetActive(true);
            StopBackgroundMusic();
        }
    }

    public void InitialPanelSetter()
    {
        Panel1.SetActive(true);
        Panel2.SetActive(true);
        Panel3.SetActive(false);
        Panel4.SetActive(false);
        Panel5.SetActive(false);
    }

    public void Panel3Start()
    {
        Debug.Log("Enter Successfully");
        Panel1.SetActive(false); 
        Panel2.SetActive(false);
        Panel3.SetActive(true);
    }

    public void YES()
    {
        SceneManager.LoadSceneAsync("LevelOne 1");
    }

    public void NO()
    {
        Application.Quit();
    }

    public void StopBackgroundMusic()
    {
        backgroundMusic.Stop();
    }
}
