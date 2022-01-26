using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class transparent : MonoBehaviour
{
    private Renderer me;
    private Material material1;
    public Material material2;
    public bool front = true;
    //public bool behind;
    // Start is called before the first frame update
    void Start()
    {
        me = GetComponent<Renderer>();
        material1 = me.material;
    }

    // Update is called once per frame
    void Update()
    {
        front = FindObjectOfType<PlayerGoOutCamera>().above;

        if (front)
            me.material = material1;
        else
            me.material = material2;
    }
    /*public void translucide()
    {
        me.material = material1;
    }
    public void opaque()
    {
        me.material = material2;
    }*/
}
