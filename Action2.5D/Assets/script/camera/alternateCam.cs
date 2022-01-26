using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class alternateCam : MonoBehaviour
{
    public float newPos;
    [HideInInspector] public float newPosition;

    // Start is called before the first frame update
    void Start()
    {
    }

// Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            FindObjectOfType<cameraSmooth>().offset.y = newPos;
        }
    }
}
