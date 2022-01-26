using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endLevel : MonoBehaviour
{
    public GameObject porte;
    public bool finish = false;
 

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       if(finish)
        {
            Destroy(porte.gameObject);
        }
       if (Input.GetKeyDown(KeyCode.T))
        {
            finish = true;
        }
    }
}
