using System.Collections;
using UnityEngine;

public class iceTransforming : MonoBehaviour
{
    Animator animator;

    private bool _isMelting = false;

    public bool isMelting
    {
        get
        {
            return _isMelting;
        }
        set
        {
            _isMelting = value;
            animator.SetBool(AnimationStrings.isMelting, value);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        animator  = GetComponent<Animator>();   
    }

    // Update is called once per frame
    void Update()
    {
        if (isMelting)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
            {
                StartCoroutine(DestroySelf());
            }
        }
    }

    public IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
