using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class moveBullet : MonoBehaviour
{
    public float speedBullet;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.velocity = transform.forward * speedBullet;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position += transform.forward * speedBullet * Time.deltaTime;
    }
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);

        if (collision.gameObject.tag == "Player")
        {
            collision.collider.GetComponent<playerMove>().hurt();
        }
    }
}