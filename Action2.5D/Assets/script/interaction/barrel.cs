using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barrel : MonoBehaviour
{
    playerMove myPlayer;
    public GameObject player;

    public float force;

    bool explosion;
    bool inTheZone;

    // Start is called before the first frame update
    void Start()
    {
        explosion = false;

        myPlayer = player.GetComponent<playerMove>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Bullet")
            explosion = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            inTheZone = true;
        }

        if (explosion)
        {
            if (other.GetComponent<Rigidbody>() != null)
            {
                Vector3 dir = (other.transform.position - transform.position).normalized;

                other.gameObject.GetComponent<Rigidbody>().AddForce(dir * force, ForceMode.Impulse);

                if (inTheZone)
                {
                    myPlayer.hurt();
                }

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
