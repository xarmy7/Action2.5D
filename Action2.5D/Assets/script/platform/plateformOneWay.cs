using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plateformOneWay : MonoBehaviour
{
    public bool below;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player" || other.gameObject.tag == "Bullet")
        {
            if (below)
                Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), other.gameObject.GetComponent<Collider>());
            below = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player" || other.gameObject.tag == "Bullet")
        {
            below = false;
        }
    }

}
