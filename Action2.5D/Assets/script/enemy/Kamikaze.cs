using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kamikaze : MonoBehaviour
{
    public GameObject player;

    public float radiusDetection;
    public float speed;

    float timer;
    [SerializeField] float timeRemaining = 2f;

    float timeRemBlinked = 0.2f;
    float timerBlinked;

    [SerializeField]
    float force = 15f;

    bool activate;
    bool blink;
    bool explosion;
    public bool inTheZone;

    PlayerGoOutCamera goOut;
    playerMove myPlayerMove;

    Color lerpedColor;
    Material mat;
    Rigidbody playerRb;

    // Start is called before the first frame update
    void Start()
    {
        blink = false;
        activate = false;
        explosion = false;
        inTheZone = false;

        timer = timeRemaining;
        timerBlinked = timeRemBlinked;

        playerRb = player.GetComponent<Rigidbody>();
        goOut = player.GetComponent<PlayerGoOutCamera>();
        myPlayerMove = player.GetComponent<playerMove>();
        mat = transform.GetChild(0).GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        //Movement
        if (transform.position.x - radiusDetection <= player.transform.position.x && transform.position.z == player.transform.position.z)
        {
            if (player.transform.position.x > transform.position.x)
                transform.Translate(transform.right * Time.deltaTime * speed);
            else
                transform.Translate(-transform.right * speed * Time.deltaTime);
        }

        //Blink
        if (activate)
        {
            timer -= Time.deltaTime;

            if (!blink)
            {
                lerpedColor = Color.Lerp(Color.white, Color.red, Time.deltaTime * 1);
                mat.color = lerpedColor;

                timerBlinked -= Time.deltaTime;
            }

            if (timerBlinked <= 0)
            {
                blink = true;
                lerpedColor = Color.Lerp(Color.red, Color.white, Time.deltaTime * 1);
                mat.color = lerpedColor;

                timerBlinked = timeRemBlinked;

                timerBlinked += Time.deltaTime;

                if (timerBlinked > timeRemBlinked)
                {
                    timerBlinked = timeRemBlinked;
                    blink = false;
                }
            }

            if (timer <= 0)
            {
                explosion = true;
                timer = timeRemaining;
            }
        }

        if (inTheZone && explosion)
        {
            myPlayerMove.hurt();
            Debug.Log("caca");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Bullet")
        {
            activate = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
            inTheZone = true;

        if (explosion)
        {
            if (other.GetComponent<Rigidbody>() != null)
            {
                Vector3 dir = (other.transform.position - transform.position).normalized;

                other.gameObject.GetComponent<Rigidbody>().AddForce(dir * force, ForceMode.Impulse);
                Destroy(gameObject);

            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
            inTheZone = false;
    }
}
