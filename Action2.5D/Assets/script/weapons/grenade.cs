using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grenade : MonoBehaviour
{
    public GameObject grenadePrefab;
    public GameObject player;
    public Rigidbody grenades;
    public float grenadeScale;
    public float speedGrenade;
    public Transform bulletSpawn;
    moveGrenade myMoveGrenade;
    public int numbGrenade = 3;




    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("RightBumper") && numbGrenade > 0 || Input.GetKeyDown(KeyCode.C) || Input.GetButtonDown("LeftBumper") && numbGrenade > 0)
        {
            fire();
            numbGrenade -= 1;
        }
    }

    private void fire()
    {
        grenadePrefab.transform.localScale = new Vector3(grenadeScale, grenadeScale, grenadeScale);

        GameObject grenade = Instantiate(grenadePrefab, player.transform.position + (transform.position - player.transform.position).normalized * 1.5f, Quaternion.LookRotation(transform.position - player.transform.position));

        myMoveGrenade = grenade.GetComponent<moveGrenade>();
        myMoveGrenade.shootLong = speedGrenade;

        Vector3 greDir = (transform.position - player.transform.position).normalized;

        //StartCoroutine(DestroyBulletAfterTime(grenade, lifeTime));
        //grenades.AddForce(greDir * forceShoot, ForceMode.Impulse); 
        //Physics.IgnoreCollision(grenade.GetComponent<Collider>(), bulletSpawn.GetComponent<Collider>());

    }
}
