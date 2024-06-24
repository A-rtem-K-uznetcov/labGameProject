
using UnityEngine;

public class PlayerAnimationContrroller : MonoBehaviour
{
    private Animator animator;
    public bool playAnimJump;
    private Rigidbody2D rb;
    private MoveController moveController;
    private bool isPlay = true;
    public bool IsPlay { get => isPlay; set => isPlay = value; }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        moveController = GetComponent<MoveController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlay)
        {
        animator.speed = 1;
        animator.SetFloat("yVelocity", rb.velocity.y);
        if (!moveController.CanJump)
            animator.SetTrigger("jumpTrigger");
        else if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) && moveController.CanJump)
            animator.SetTrigger("runTrigger");
        else if ((Mathf.Abs(rb.velocity.y) < 0.1f) && moveController.CanJump)
            animator.SetTrigger("idleTrigger");
    }
    else animator.speed = 0;
    
    }
}