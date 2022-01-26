using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsOnPlayer : MonoBehaviour
{
    public GameObject aim;
    public GameObject ak47Weapon;
    public GameObject sniperWeapon;
    public GameObject shotGunWeapon;
    public GameObject uziWeapon;

    public ParticleSystem ak1;
    public ParticleSystem ak2;
    public ParticleSystem uzi1;
    public ParticleSystem uzi2;
    public ParticleSystem sniper1;
    public ParticleSystem sniper2;
    public ParticleSystem shotGun1;
    public ParticleSystem shotGun2;

    float timer;

    playerMove myPlayer;
    AimShoot uzi;
    secondShooter secondShooter;

    // Start is called before the first frame update
    void Start()
    {
        myPlayer = GetComponent<playerMove>();
        uzi = aim.GetComponent<AimShoot>();
        secondShooter = aim.GetComponent<secondShooter>();

        timer = secondShooter.timeRemaining;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (myPlayer.akIsDrag && secondShooter.secondShoot)
        {
            sniperWeapon.SetActive(false);
            shotGunWeapon.SetActive(false);
            uziWeapon.SetActive(false);
            ak47Weapon.SetActive(true);

            if (secondShooter.shooting && !uzi.shooting)
            {
                ak1.Play();
                ak2.Play();
            }
            else
            {
                ak1.Stop();
                ak2.Stop();
            }
        }
        else if (myPlayer.shotGunIsDrag && secondShooter.secondShoot)
        {

            sniperWeapon.SetActive(false);
            ak47Weapon.SetActive(false);
            uziWeapon.SetActive(false);
            shotGunWeapon.SetActive(true);

            timer -= Time.deltaTime;
            if (secondShooter.shooting && !uzi.shooting && timer <= 0)
            {
                shotGun1.Play();
                shotGun2.Play();

                timer = secondShooter.timeRemaining;
            }
            else
            {
                shotGun1.Stop();
                shotGun2.Stop();
            }
        }
        else if (myPlayer.sniperIsDrag && secondShooter.secondShoot)
        {
            ak47Weapon.SetActive(false);
            shotGunWeapon.SetActive(false);
            uziWeapon.SetActive(false);
            sniperWeapon.SetActive(true);

            timer -= Time.deltaTime;
            if (secondShooter.shooting && !uzi.shooting && timer <= 0)
            {
                sniper1.Play();
                sniper2.Play();

                timer = secondShooter.timeRemaining;
            }
            else
            {
                sniper1.Stop();
                sniper2.Stop();
            }
        }
        else if (uzi.uzi && !secondShooter.secondShoot)
        {
            ak47Weapon.SetActive(false);
            sniperWeapon.SetActive(false);
            shotGunWeapon.SetActive(false);
            uziWeapon.SetActive(true);

            if (uzi.shooting)
            {
                uzi1.Play();
                uzi2.Play();
            }
            else
            {
                uzi1.Stop();
                uzi2.Stop();
            }

        }
    }
}
