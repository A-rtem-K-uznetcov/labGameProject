using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckController : MonoBehaviour
{
    private bool isTake;
    public bool IsTake { get => isTake; set { if (!value) isTake = value; } }
    private AudioSource audioCheck;

    private Animator animatorFlag;
    private bool isPlay = true;
    public Animator AnimatorFlag { get => animatorFlag; }
    public bool IsPlay { get => isPlay; set => isPlay = value; }
    // Start is called before the first frame update
    void Start()
    {
        isTake = false;
        animatorFlag = GetComponent<Animator>();
        audioCheck = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
      if (isPlay) animatorFlag.speed = 1;

      else animatorFlag.speed = 0;
    }

   private void OnTriggerEnter2D(Collider2D collision)
   {
     if (collision.gameObject.name == "Player")
     {
     
       CheckController[] massChecks = GameObject.Find("CHECKPOINTS").GetComponentsInChildren<CheckController>();
       foreach(CheckController script in massChecks)
       {
        if (!script.Equals(this))
        {

         script.IsTake = false;
         script.animatorFlag.SetBool("isActive", false);
        }
       }
        if (!this.isTake)
      {
        audioCheck.Play();
       isTake = true;
      }
       animatorFlag.SetBool("isActive", true);
     }
   }

}
