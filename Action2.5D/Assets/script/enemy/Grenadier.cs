using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenadier : MonoBehaviour
{
    public GameObject grenadePrefab;
    public GameObject enemyGre;
    public GameObject player;

    Rigidbody rb;

    public Transform bulletSpawn;

    [SerializeField] private float enemySpeed = 10f;

    float timer;
    [SerializeField] float timeRemaining = 2f;


    public bool flying = false;
    bool inTheZone = false;
    bool activate = false;
    bool firstTime = false;

    Vector3 initPos;
    public int stepNumber;
    int i = 0;

    public float grenadeScale;
    public float speedGrenade;

    bool test;

    moveGrenadeGrenadier myMoveGrenade;

    // Start is called before the first frame update
    void Start()
    {
        myMoveGrenade = grenadePrefab.GetComponent<moveGrenadeGrenadier>();

        timer = timeRemaining;

        firstTime = true;
        rb = enemyGre.GetComponent<Rigidbody>();

        initPos = transform.position;

        test = myMoveGrenade.grenadeDestroy = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.parent.position.x > player.transform.position.x)
            transform.parent.eulerAngles = new Vector3(transform.rotation.x, 0, transform.rotation.z);
        else
              transform.parent.eulerAngles = new Vector3(transform.rotation.x, -180, transform.rotation.z);

        test = myMoveGrenade.grenadeDestroy;

        if (!VisibleToCamera(transform))
        {
            activate = false;
        }

        if (flying && activate && inTheZone)
        {
            firstTime = false;

            rb.velocity = Vector3.zero;

            timer -= Time.deltaTime;
            if (timer <= 0 && test)
            {
                fire();
                timer = timeRemaining;
            }
        }

        if (firstTime && activate && !flying)
        {
            if (player.transform.position.x < transform.parent.position.x)
            {
                if (stepNumber <= initPos.x - transform.position.x || stepNumber <= transform.position.x - initPos.x)
                {
                    rb.velocity = Vector3.zero;
                    fire();
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
                    fire();
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
                    if (timer <= 0 && test)
                    {
                        fire();
                        timer = timeRemaining;
                    }
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            activate = true;
        }

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
                if (timer <= 0 && test)
                {
                    fire();
                    timer = timeRemaining;
                }
            }

            activate = true;
            inTheZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
            inTheZone = false;
    }

    private void fire()
    {
        test = false;
        initPos = transform.position;


        grenadePrefab.transform.localScale = new Vector3(grenadeScale, grenadeScale, grenadeScale);

        GameObject grenade = Instantiate(grenadePrefab, enemyGre.transform.position + (transform.position - enemyGre.transform.position).normalized * 1.5f, Quaternion.LookRotation(transform.position - enemyGre.transform.position));

        myMoveGrenade = grenade.GetComponent<moveGrenadeGrenadier>();
        myMoveGrenade.shootLong = speedGrenade;

        Vector3 greDir = (transform.position - enemyGre.transform.position).normalized;
    }

    private static bool VisibleToCamera(Transform transform)
    {
        Vector3 myVision = Camera.main.WorldToViewportPoint(transform.position);
        return (myVision.x >= 0 && myVision.y >= 0) && (myVision.x <= 1 && myVision.y <= 1) && myVision.z >= 0;
    }
}
