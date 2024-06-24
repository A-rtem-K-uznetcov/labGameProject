
using System.Collections;
using UnityEngine;


public class MoveController : MonoBehaviour
{

    [SerializeField]
    private float speed;
    [SerializeField]
    private float powerJump;
    private bool flipFlag = true;
    private bool canJump = true;
    private bool isPlay = true;
    private bool playMusicWalk = false;
    private Rigidbody2D rb;
    private SpriteRenderer render;
    private AudioSource jumpSound;
    private AudioSource walkSound;
    public bool CanJump { get => canJump; }
    public bool FlipFlag { get => flipFlag; }
    public bool IsPlay { get => isPlay; set => isPlay = value; }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        render = GetComponent<SpriteRenderer>();
        jumpSound = GetComponents<AudioSource>()[0];
        walkSound = GetComponents<AudioSource>()[2];
    }

    // Update is called once per frame
    void Update()
    {
        /*
        //if (Input.GetKeyDown(KeyCode.D)) // однократное нажатие
        if (Input.GetKey(KeyCode.D))
            transform.Translate(new Vector2(speed*Time.deltaTime, 0));
        else if (Input.GetKey(KeyCode.A))
            transform.Translate(new Vector2(-speed*Time.deltaTime, 0));
            */
        if (isPlay)
        {
            transform.Translate(new Vector2(Input.GetAxis("Horizontal") * Time.deltaTime * speed, 0));  //перемещение по предопред. осям
            if (Input.GetAxis("Horizontal") != 0 && canJump)
            {
                if (!playMusicWalk)
                    StartCoroutine(LoopPlayWalk());
            }
            else
                walkSound.Stop();
            if (canJump && Input.GetAxis("Jump") > 0)
            {
                canJump = false;
                rb.AddForce(powerJump * Vector2.up, ForceMode2D.Impulse);
                jumpSound.Play();
            }
            if ((flipFlag && Input.GetAxis("Horizontal") < 0) || (!flipFlag && Input.GetAxis("Horizontal") > 0))
                Flip();

        }
    }

    private void Flip()
    {
        render.flipX = flipFlag;
        flipFlag = !flipFlag;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        canJump = true;
    }

    IEnumerator LoopPlayWalk()
    {
        walkSound.Play();
        playMusicWalk = true;
        yield return new WaitForSeconds(walkSound.clip.length + 0.15f);
        playMusicWalk = false;
    }

}
