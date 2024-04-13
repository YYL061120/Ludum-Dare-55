using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatformsMotionController : MonoBehaviour
{
    Rigidbody2D rb;

    public float waitingTime = 0.35f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Debug.Log("Enter succeeded");
            StartCoroutine(Onfalling());
        }
    }

    public IEnumerator Onfalling()
    {
        yield return new WaitForSeconds(waitingTime);
        rb.bodyType = RigidbodyType2D.Dynamic;
    }
}
