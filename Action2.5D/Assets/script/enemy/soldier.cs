using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soldier : MonoBehaviour
{
    AudioSource audio;

    public GameObject player = null;
    public GameObject bulletPrefab = null;

    playerMove myPlayer = null;

    Rigidbody rb = null;

    Transform bulletSpawn = null;

    [SerializeField] private uint soldierLife = 3;
    [SerializeField] private uint bulletLifeTime = 3;

    [SerializeField] private float enemySpeed = 10f;

    float timer;
    [SerializeField] float timeRemaining = 2f;

    public int stepNumber = 0;
    int i = 0;

    public bool flying = false;
    bool inTheZone = false;
    bool activate = false;
    bool firstTime = false;

    Vector3 initPos;

    EnemyManager enemyManager;

    // Start is called before the first frame update
    void Start()
    {
        firstTime = true;
        initPos = transform.position;

        rb = GetComponent<Rigidbody>();
        myPlayer = player.GetComponent<playerMove>();
        enemyManager = GetComponent<EnemyManager>();

        enemyManager.enemyLife = soldierLife;

        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!VisibleToCamera(transform))
        {
            activate = false;
        }
        
        if (flying && activate && inTheZone)
        {
            firstTime = false;

            rb.velocity = Vector3.zero;

            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                fireEnemy();
                timer = timeRemaining;
            }
        }

        if (firstTime && activate && !flying)
        {
            if (player.transform.position.x < transform.position.x)
            {
                if (stepNumber <= initPos.x - transform.position.x || stepNumber <= transform.position.x - initPos.x)
                {
                    rb.velocity = Vector3.zero;
                    fireEnemy();
                    firstTime = false;
                }
                else
                {
                    Vector3 moving = (player.transform.position - transform.position).normalized * enemySpeed;
                    rb.velocity = new Vector3(moving.x, 0, moving.z);
                }
            }
            else
            {
                if (stepNumber >= initPos.x - transform.position.x || stepNumber >= transform.position.x - initPos.x)
                {
                    rb.velocity = Vector3.zero;
                    fireEnemy();
                    firstTime = false;
                }
                else
                {
                    Vector3 moving = (player.transform.position - transform.position).normalized * enemySpeed;
                    rb.velocity = new Vector3(moving.x, 0, moving.z);
                }
            }
        }

        if (!inTheZone && activate && !firstTime && !flying)
        {
            if (stepNumber <= initPos.x - transform.position.x || stepNumber <= transform.position.x - initPos.x)
            {
                rb.velocity = Vector3.zero;

                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    fireEnemy();
                    timer = timeRemaining;
                }
            }
            else
            {
                Vector3 moving = (player.transform.position - transform.position).normalized * enemySpeed;
                rb.velocity = new Vector3(moving.x, 0, moving.z);
            }

            if (inTheZone && !firstTime && !flying)
            {
                rb.velocity = Vector3.zero;

                if (stepNumber <= initPos.x - transform.position.x || stepNumber <= transform.position.x - initPos.x)
                {
                    timer -= Time.deltaTime;
                    if (timer <= 0)
                    {
                        fireEnemy();
                        timer = timeRemaining;
                    }
                }
            }
        }

        if (soldierLife == 0 || soldierLife >= 4194967295)
        {
            Debug.Log("why ??");
        }
    }

    private static bool VisibleToCamera(Transform transform)
    {
        Vector3 myVision = Camera.main.WorldToViewportPoint(transform.position);
        return (myVision.x >= 0 && myVision.y >= 0) && (myVision.x <= 1 && myVision.y <= 1) && myVision.z >= 0;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (i == 0)
            {
                firstTime = true;

                i++;
            }
            else
            {
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    fireEnemy();
                    timer = timeRemaining;
                }
            }

            activate = true;                                            
            inTheZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        inTheZone = false;
    }

    private IEnumerator DestroyBulletAfterTime(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(bullet);
    }

    private void fireEnemy()
    {
        initPos = transform.position;

        GameObject bullet = Instantiate(bulletPrefab, transform.position + (player.transform.position - transform.position).normalized, Quaternion.LookRotation(player.transform.position - transform.position));
        
        if (!audio.isPlaying && audio.clip != null)
            audio.Play();

        StartCoroutine(DestroyBulletAfterTime(bullet, bulletLifeTime));
        Physics.IgnoreCollision(bullet.GetComponent<Collider>(), GetComponent<Collider>());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            enemyManager.EnemyTouche();
        }

        if (collision.gameObject.tag == "Player")
            myPlayer.hurt();
    }
}
