using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ammo : MonoBehaviour
{
    [SerializeField] playerMove player;
    [SerializeField] grenade aimGrenade;

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
        if(other.gameObject.tag == "ammo")
        {
            if(player.akIsDrag)
            {
                player.dragAk();
                aimGrenade.numbGrenade += 1;
                Destroy(other.gameObject);
            }

            if (player.shotGunIsDrag)
            {
                player.dragShotGun();
                aimGrenade.numbGrenade += 1;
                Destroy(other.gameObject);
            }

            if (player.sniperIsDrag)
            {
                player.dragSniper();
                aimGrenade.numbGrenade += 1;
                Destroy(other.gameObject);
            }
        }
    }
}
