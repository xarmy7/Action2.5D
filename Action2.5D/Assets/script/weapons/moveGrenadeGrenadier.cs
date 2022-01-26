using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveGrenadeGrenadier : MonoBehaviour
{
    public float shootLong;
    public float timeRemaining;
    float timer;
    public float boumReflexion;
    public bool timeToExplode;

    public bool InsideTheZone = false;

    public bool grenadeDestroy = false;

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        timer = timeRemaining;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * shootLong * Time.deltaTime;

        timer -= Time.deltaTime;

        if (timer < 0)
        {
            timeToExplode = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            InsideTheZone = true;
        }

        if (timeToExplode == true)
        {
            if (other.gameObject.tag == "Player")
            {
                other.GetComponent<playerMove>().hurt();
            }

            if (other.GetComponent<Rigidbody>() != null)
            {
                timer = timeRemaining;
                Vector3 dir = (other.transform.position - transform.position).normalized;
                other.gameObject.GetComponent<Rigidbody>().AddForce(dir * boumReflexion, ForceMode.Impulse);

            }
            Destroy(gameObject);
                grenadeDestroy = true;
        }
    }

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.tag == "Player")
    //        InsideTheZone = false;
    //}

}

