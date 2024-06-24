
using System.Collections;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using UnityEngine;

public class PickUpController : MonoBehaviour
{
  private int score = 0;
  [SerializeField]
  private TMP_Text textMeshPro;
  public int Score { get => score; set => score = value; }
  private void Start()
  {

    textMeshPro.text += score;
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.gameObject.tag == "Fruits")
      StartCoroutine(DelayDestroy(collision));
  }
  IEnumerator DelayDestroy(Collider2D collision)
  {
    score++;
    textMeshPro.text = textMeshPro.text.Substring(0, textMeshPro.text.Length - 1) + score;
    AudioSource music = collision.gameObject.GetComponent<AudioSource>();
    if (music != null) music.Play();

    collision.gameObject.GetComponent<SpriteRenderer>().enabled = false;
    yield return new WaitForSeconds(music.clip.length);
    Destroy(collision.gameObject);
  }
}