using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightOrigin : MonoBehaviour
{

    public Transform playerTransform;

    public GameObject PlayerUpperLight;
    public GameObject hightLight;

    public float heightOffset = 23f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(playerTransform.position.x, playerTransform.position.y+heightOffset);

        playerUpperLightController();
    }

    public void playerUpperLightController()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 100f);

        Debug.DrawRay(transform.position, Vector2.down * hit.distance, Color.red);

        if (hit.collider != null)
        {
            GameObject hitObject= hit.collider.gameObject;

            if (hitObject.CompareTag("Player"))
            {
                PlayerHealth.isLighted = true;
                PlayerUpperLight.SetActive(true);
                hightLight.SetActive(false);
            }

            if (hitObject.CompareTag("Ground") || hitObject.CompareTag("ice"))
            {
                if (!MPSkill.MPSkill2_using)
                {
                    PlayerHealth.isLighted = false;
                }
                PlayerUpperLight.SetActive(false);
                hightLight.SetActive(true);
            }
        }
    }
}       
