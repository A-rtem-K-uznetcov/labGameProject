using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolController : MonoBehaviour
{
    private bool flipFlag = false;
    private bool stay = false;
    [SerializeField]
    private float speed;
    private Animator animator;
    private SpriteRenderer render;
    private bool isPlay = true;
    public bool IsPlay { get => isPlay; set => isPlay = value; }
    // Start is called before the first frame update
    void Start()
    {
        render = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlay)
        {
            animator.speed = 1;
            if (!stay)
                transform.Translate(new Vector3(flipFlag ? speed : -speed, 0, 0) * Time.deltaTime);
        }
        else animator.speed = 0;
    }
    private void Flip()
    {
        render.flipX = flipFlag;
        flipFlag = !flipFlag;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!stay)
            StartCoroutine(StayDelay());
    }

    IEnumerator StayDelay()
    {
        stay = true;
        animator.SetTrigger("isIdle");
        yield return new WaitForSeconds(2);
        animator.SetTrigger("isRun");
        stay = false;
        Flip();
    }

}
