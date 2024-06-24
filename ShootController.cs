using System.Collections;
using UnityEngine;

public class ShootController : MonoBehaviour
{
        [SerializeField]
    private GameObject prefabBullet;
    private bool canShoot = true; 
    private bool flipFlag;
    private AudioSource shootSound;
    private bool isPlay = true;
    public bool IsPlay { get => isPlay; set => isPlay = value; }
    // Start is called before the first frame update
    void Start()
    {
     shootSound = GetComponents<AudioSource>()[1];
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPlay)
        {
        flipFlag = GameObject.Find("Player").GetComponent<MoveController>().FlipFlag;
         if (Input.GetKeyDown(KeyCode.F) && canShoot)
         {
            if (prefabBullet != null)
            {
            StartCoroutine(DelayBetweenShoot());
            shootSound.Play();
             Instantiate(prefabBullet, new Vector3(transform.position.x + (flipFlag ? 1 : -1), transform.position.y, transform.position.z), Quaternion.identity);
            }
         }
        }
    }
        IEnumerator DelayBetweenShoot()
    {
        canShoot = false;
        yield return new WaitForSeconds(0.4f);
        canShoot = true;
    }
}
