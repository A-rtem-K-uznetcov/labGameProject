using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    private GameObject[] hearts;
    [SerializeField] private int health;
    [SerializeField] private int howManyFruictsAddHealth;
    [SerializeField] private GameObject prefabHeart;
    private Animator animator;
    private int currentHealth = 0;
    private bool flagAdd = true;
    // Start is called before the first frame update
    void Start()
    {
        hearts = new GameObject[health];
        currentHealth = health;
        resetHealth(currentHealth);
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        checkAddHealth();
        addHealth();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Trap" || collision.gameObject.tag == "Enemy")
        {
            if (currentHealth > 0)
            {
                currentHealth -= 1;
                GetComponents<AudioSource>()[3].Play();
                Destroy(hearts[currentHealth]);
            }
            if (currentHealth <= 0)
            {
                GetComponents<AudioSource>()[4].Play();
                currentHealth = health - 1;
                StartCoroutine(DieDelay());
            }
        }
    }
    IEnumerator DieDelay()
    {
        animator.SetTrigger("isDie");
        yield return new WaitForSeconds(0.3f);
        animator.SetTrigger("idleTrigger");
        gameObject.transform.position = GetComponent<SpawnController>().SpawnPos;
        resetHealth(currentHealth);
    }

    private void resetHealth(int health)
    {
        float sizeHeartWithOffset = 0.12f;
        float size = health * sizeHeartWithOffset;
        for (int i = 0; i < health; i++)
        {
            hearts[i] = Instantiate(prefabHeart, Vector3.zero, Quaternion.identity);
            hearts[i].transform.SetParent(transform, false);
            hearts[i].transform.position = new Vector3((transform.position.x - size / 2 + 0.04f) + i * sizeHeartWithOffset, transform.position.y + 0.2f, transform.position.z);
        }
    }

    private void addHealth()
    {
        if (currentHealth < health)
        {
            int tmp = gameObject.GetComponent<PickUpController>().Score;
            if (tmp % howManyFruictsAddHealth == 0 && tmp != 0)
            {
                float sizeHeartWithOffset = 0.12f;
        float size = health * sizeHeartWithOffset;
            hearts[currentHealth] = Instantiate(prefabHeart, Vector3.zero, Quaternion.identity);
            hearts[currentHealth].transform.SetParent(transform, false);
            hearts[currentHealth].transform.position = new Vector3((transform.position.x - size / 2 + 0.04f) + currentHealth * sizeHeartWithOffset, transform.position.y + 0.2f, transform.position.z);
                currentHealth++;
                flagAdd = false;
            }
        }
    }

    private void checkAddHealth()
    {
        if (gameObject.GetComponent<PickUpController>().Score % 10 != 0 && !flagAdd)
            flagAdd = true;
    }

}
