using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingMine : MonoBehaviour
{
    public GameObject player;

    playerMove myPlayer;
    MeshRenderer myMeshRenderer;
    Collider myCollider;

    public float force;
    [SerializeField] float timeRespawn = 0.5f;
    float timer;

    bool explosion = false;
    bool beenDestroy = false;
    bool inTheZone = false;

    // Start is called before the first frame update
    void Start()
    {
        myMeshRenderer = transform.GetChild(0).gameObject.GetComponent<MeshRenderer>();
        myCollider = gameObject.GetComponent<Collider>();
        myPlayer = player.GetComponent<playerMove>();

        timer = timeRespawn;
    }

    // Update is called once per frame
    void Update()
    {
        if (inTheZone && explosion)
        {
            myPlayer.hurt();
        }

        if (beenDestroy)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                myMeshRenderer.enabled = true;
                myCollider.enabled = true;

                beenDestroy = false;

                timer = timeRespawn;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        explosion = true;

        myMeshRenderer.enabled = false;
        myCollider.enabled = false;

        beenDestroy = true;

    }

    private void OnTriggerStay(Collider other)
    {
        if (explosion)
        {
            if (other.GetComponent<Rigidbody>() != null)
            {
                Vector3 dir = (other.transform.position - transform.position).normalized;

                other.gameObject.GetComponent<Rigidbody>().AddForce(dir * force, ForceMode.Impulse);

                if (other.gameObject.tag == "Player")
                {
                    inTheZone = true;
                }

                explosion = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            inTheZone = false;
        }
    }
}
