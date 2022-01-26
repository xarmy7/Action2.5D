using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveGrenade : MonoBehaviour
{
    public float shootLong;
    public float timeRemaining;
    float timer;
    public float boumReflexion;
    public bool timeToExplode;

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
        Debug.Log("enter");
        Debug.Log(timer);

        if (timer < 0)
        {
            timeToExplode = true;
            Debug.Log("true");
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (timeToExplode == true)
        {
            if (other.gameObject.tag == "enemy")
            {
                    timer = timeRemaining;
                    Vector3 dir = (other.transform.position - transform.position).normalized;
                    other.gameObject.GetComponent<Rigidbody>().AddForce(dir * boumReflexion, ForceMode.Impulse);
            }
            Destroy(gameObject);
        }
    }
    
}

