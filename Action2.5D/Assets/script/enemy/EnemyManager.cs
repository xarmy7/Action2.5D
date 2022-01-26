using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject aim;

    [HideInInspector] public float enemyLife;

    float maxLife = 1000f;

    bool touch;
    public bool boss;

    secondShooter mySecondShooter;
    AimShoot myAim;
    playerMove myWeapOnPlayer;

    // Start is called before the first frame update
    void Start()
    {
        mySecondShooter = aim.GetComponent<secondShooter>();
        myAim = aim.GetComponent<AimShoot>();
        myWeapOnPlayer = aim.transform.parent.GetComponent<playerMove>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void EnemyTouche()
    {

        if (myAim.uzi)
        {
            enemyLife -= myAim.damage;
        }
        else
        {
            enemyLife -= mySecondShooter.damage;
        }

        if (enemyLife <= 0 || enemyLife >= maxLife)
        {
            if (boss == true)
            {
                FindObjectOfType<endLevel>().finish = true;
                gameObject.SetActive(false);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
