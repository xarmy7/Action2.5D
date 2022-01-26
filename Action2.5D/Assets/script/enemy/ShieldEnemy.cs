using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldEnemy : MonoBehaviour
{
    public GameObject shield;
    public GameObject player;

    Rigidbody rb;

    [SerializeField] private uint enemyShieldLife = 3;

    [SerializeField] private float enemySpeed = 10f;

    public bool flying = false;
    bool inTheZone = false;
    bool activate;
    bool firstTime = false;

    Vector3 initPos;


    public int stepNumber;
    int i = 0;

    EnemyManager enemyManager;


    // Start is called before the first frame update
    void Start()
    {
        initPos = transform.position;
        rb = GetComponent<Rigidbody>();
        firstTime = true;

        enemyManager = GetComponent<EnemyManager>();
        enemyManager.enemyLife = enemyShieldLife;
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
        }

        if (firstTime && activate && !flying)
        {
            if (stepNumber <= initPos.x - transform.position.x || stepNumber <= transform.position.x - initPos.x)
            {
                rb.velocity = Vector3.zero;
                firstTime = false;
            }
            else
            {
                rb.velocity = (player.transform.position - transform.position).normalized * enemySpeed;
            }
        }

        if (!inTheZone && activate && !firstTime && !flying)
        {
            if (stepNumber <= initPos.x - transform.position.x || stepNumber <= transform.position.x - initPos.x)
            {
                rb.velocity = Vector3.zero;
            }
            else
            {
                rb.velocity = (player.transform.position - transform.position).normalized * enemySpeed;
            }

            if (inTheZone && !firstTime && !flying)
            {
                rb.velocity = Vector3.zero;
            }
        }

        if (enemyShieldLife == 0)
            Destroy(gameObject);
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

            activate = true;
            inTheZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        initPos = transform.position;
        inTheZone = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            enemyManager.EnemyTouche();
        }
    }
}
