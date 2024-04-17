using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPointLogic : MonoBehaviour
{
    public GameObject Player;
    public GameObject DetectPoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DetectPointChecker();
    }

    public void DetectPointChecker()
    {
        bool canDetect1 = true;
        switch (DetectPoint.name)
        {
            case "DetectPoint1":
                if (Vector2.Distance(Player.transform.position, DetectPoint.transform.position) < 5f)
                {
                    StageEffects_LevelOne1.canShowHealthTutorial = true;
                }
                break;

            case "DetectPoint_LevelOne 2_stone":
                if (Vector2.Distance(Player.transform.position, DetectPoint.transform.position) < 2f)
                {
                    StageEffects_LevelOne2.canShowStoneTu = true;
                }
                break;
        }
    }

/*    public IEnumerator DetectPoint1()
    {
        yield return new WaitForSeconds(0.001f);
        StageEffects_LevelOne1.canShowHealthTutorial = false;
    }*/
}
