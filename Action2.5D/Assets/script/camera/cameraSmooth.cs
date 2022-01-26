using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraSmooth : MonoBehaviour
{
    public Transform target;
    public float smoothTime = 0.3F;
    public Vector3 offset = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 desiredPos = new Vector3(target.position.x, 0, 0) + new Vector3(offset.x, offset.y, offset.z);
        Vector3 smoothPos = Vector3.Lerp(transform.position, desiredPos, smoothTime);
        transform.position = smoothPos;
    }
}
