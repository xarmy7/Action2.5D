using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGoOutCamera : MonoBehaviour
{
    public GameObject wall;

    public Transform Foreground;
    public Transform BackGround;

    Rigidbody rb;

    public Vector3 player_Checkpoint;

    public bool above = true;
    public bool depth;
    bool myCollision = false;

    public float posZ = 33.5f;

    public float highDepth = 26f;
    public float high = 17f;

    // Start is called before the first frame update
    void Start()
    {
        player_Checkpoint = transform.position;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 truc = Camera.main.WorldToViewportPoint(transform.position);

        if (VisibleToCamera(transform) == false && depth == false && myCollision == false)
        {
            transform.position = new Vector3(transform.position.x, BackGround.position.y, posZ);
            depth = true;
            rb.velocity = rb.velocity = new Vector3(rb.velocity.x, -rb.velocity.y, rb.velocity.z);
            above = false;
        }
        else if (VisibleToCamera(transform) == false && depth == true && myCollision == false)
        {
            transform.position = new Vector3(transform.position.x, Foreground.position.y, 0f);
            depth = false;
            rb.velocity = rb.velocity = new Vector3(rb.velocity.x, -rb.velocity.y, rb.velocity.z);
            above = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "DeathZone")
        {
            myCollision = true;
            transform.position = new Vector3(player_Checkpoint.x, player_Checkpoint.y + 1f, player_Checkpoint.z);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        myCollision = false;
    }

    private bool VisibleToCamera(Transform transform)
    {
        Vector3 visionTest = Camera.main.WorldToViewportPoint(transform.position);
        return (visionTest.x >= 0) && (visionTest.x <= 1 && visionTest.y <= 1) && visionTest.z >= 0;
    }
}