using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRotation : MonoBehaviour
{
    public Transform Aim;

    Vector3 m_Input = Vector3.zero;
    Vector3 dir = Vector3.zero;
    Vector3 posAim = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(new Vector3(Aim.position.x, Aim.position.y, transform.position.z));
    }
}
