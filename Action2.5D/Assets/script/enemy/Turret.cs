using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public GameObject player;
    public GameObject bulletPrefab;

    Rigidbody rb;

    public Transform bulletSpawn;

    [SerializeField] private uint turretLife = 3;
    [SerializeField] private uint bulletLifeTime = 3;

    float timer;
    float lifeMax = 4194967295;
    [SerializeField] float timeRemaining = 2f;

    bool inTheZone = false;
    bool activate;
    public bool left;
    public bool up;

    Vector3 initPos;

    EnemyManager enemyManager;


    // Start is called before the first frame update
    void Start()
    {
        enemyManager = GetComponent<EnemyManager>();
        enemyManager.enemyLife = turretLife;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0 && inTheZone)
        {
            fireTurret();
            timer = timeRemaining;
        }

        if (turretLife == 0 || turretLife >= lifeMax)
        {
            Destroy(gameObject);
        }

        if (gameObject == null)
        {
            turretLife = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            inTheZone = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
            inTheZone = false;
    }

    private IEnumerator DestroyBulletAfterTime(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(bullet);
    }

    private void fireTurret()
    {
        GameObject bullet;
        if (transform.rotation.eulerAngles == new Vector3(0,0,0) && !up)
        {
            bullet = Instantiate(bulletPrefab, transform.position + (Vector3.up).normalized, Quaternion.LookRotation(-Vector3.up));

            StartCoroutine(DestroyBulletAfterTime(bullet, bulletLifeTime));
            Physics.IgnoreCollision(bullet.GetComponent<Collider>(), bulletSpawn.GetComponent<Collider>());
        }
        else if (transform.rotation.eulerAngles == new Vector3(0, 0, 0) && up)
        {
            bullet = Instantiate(bulletPrefab, transform.position + (Vector3.up).normalized, Quaternion.LookRotation(Vector3.up));

            StartCoroutine(DestroyBulletAfterTime(bullet, bulletLifeTime));
            Physics.IgnoreCollision(bullet.GetComponent<Collider>(), bulletSpawn.GetComponent<Collider>());
        }
        else if (transform.rotation.eulerAngles == new Vector3(0, 180, 90) && left)
        {
            bullet = Instantiate(bulletPrefab, transform.position + (Vector3.right).normalized, Quaternion.LookRotation(-Vector3.right));

            StartCoroutine(DestroyBulletAfterTime(bullet, bulletLifeTime));
            Physics.IgnoreCollision(bullet.GetComponent<Collider>(), bulletSpawn.GetComponent<Collider>());
        }
        else if (transform.rotation.eulerAngles == new Vector3(0, 180, 90) && !left)
        {
            bullet = Instantiate(bulletPrefab, transform.position + (Vector3.right).normalized, Quaternion.LookRotation(Vector3.right));

            StartCoroutine(DestroyBulletAfterTime(bullet, bulletLifeTime));
            Physics.IgnoreCollision(bullet.GetComponent<Collider>(), bulletSpawn.GetComponent<Collider>());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            enemyManager.EnemyTouche();
        }
    }
}
