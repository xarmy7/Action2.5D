using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public GameObject dynamo;
    generator myGenerator;

    // Start is called before the first frame update
    void Start()
    {
        myGenerator = dynamo.GetComponent<generator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (myGenerator.beenDestroy && myGenerator != null)
        {
            Destroy(gameObject);
            Destroy(dynamo);
        }
    }
}
