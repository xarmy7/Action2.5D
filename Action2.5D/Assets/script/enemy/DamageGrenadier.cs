using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageGrenadier : MonoBehaviour
{
    public uint grenadierLife = 20;

    playerMove myPlayer;

    EnemyManager enemyManager;

    // Start is called before the first frame update
    void Start()
    {
        myPlayer = FindObjectOfType<playerMove>().GetComponent<playerMove>();
        enemyManager = GetComponent<EnemyManager>();
        enemyManager.enemyLife = grenadierLife;
    }

    // Update is called once per frame
    void Update()
    {
        if (grenadierLife == 0 || grenadierLife >= 4194967295)
        {
            Destroy(gameObject);
            grenadierLife = 0;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            enemyManager.EnemyTouche();
        }

        if (collision.gameObject.tag == "Player")
        {
            myPlayer.hurt();
        }
    }
}
