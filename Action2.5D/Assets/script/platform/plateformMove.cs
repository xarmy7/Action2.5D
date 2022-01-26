using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plateformMove : MonoBehaviour
{
    public float endPos = 0;
    public float speed = 5;

    private float startPos;
    private float destPos;
    private float dir = 1;

    // Start is called before the first frame update
    void Start()
    {
        startPos = this.transform.position.x;
        destPos = endPos;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3((speed * Time.deltaTime) * dir, 0, 0);
        if (Mathf.Abs(transform.position.x - destPos) < 0.5f)
        {

            destPos = destPos.Equals(startPos) ? endPos : startPos;
            dir *= -1;
        }
       
    }
}
