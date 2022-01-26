using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public Sprite ak47;
    public Sprite shotGun;
    public Sprite sniper;
    public Sprite empty;
    public Image secondWeapon;
    playerMove player;
    public Text ammo;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<playerMove>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            player.akIsDrag = false;
            player.shotGunIsDrag = true;
        }

        if(player.akIsDrag)
        {
            secondWeapon.sprite = ak47;
        }

        else if (player.shotGunIsDrag)
        {
            secondWeapon.sprite = shotGun;
        }

        else if (player.sniperIsDrag)
        {
            secondWeapon.sprite = sniper;
        }
        
        else
        {
            secondWeapon.sprite = empty;
        }
    }
}
