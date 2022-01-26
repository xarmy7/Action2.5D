using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class secondShooter : MonoBehaviour
{
    public GameObject player;
    public GameObject bulletPrefab;

    ParticleSystem myParticleSystem;

    public Rigidbody me;

    public Transform bulletSpawn;

    public Vector3 posAim;
    public Vector3 m_Input;
    Vector3 move = Vector3.zero;

    public float BulletlifeTime;
    public float speedBullet;
    public float timeRemaining = 2f;
    public float forceShoot;
    public float damage;
    public int numBullet;
    public float bulletScale;
    float timer;

    public int dispersionHalfAngle;

    public bool TriggerIsInUse;
    public bool shotGun;
    AimShoot myWeapon;
    [HideInInspector] public bool secondShoot = false;
    [HideInInspector] public bool shooting = false;

    moveBullet myMoveBullet;
    WeaponsOnPlayer weapOnPlayer;

    public ammoBar ammoBar = null;

    // Start is called before the first frame update
    void Start()
    {
        myWeapon = GetComponent<AimShoot>();
        
    }

    // Update is called once per frame
    void Update()
    {
        ammoBar.setAmmo(numBullet);
        m_Input = new Vector3(Input.GetAxis("LeftJoystickHorizontal"), Input.GetAxis("LeftJoystickVertical"), 0).normalized;

        //maybe add↓
        // if (m_Input.x != 0 || m_Input.y != 0)

        Vector3 dir = m_Input * 2;
        posAim = player.transform.position + dir;

        transform.position = posAim;

        float angle = Vector3.Angle(Vector3.right, m_Input);

        Quaternion quat = Quaternion.AngleAxis(angle, Vector3.forward);

        transform.eulerAngles = new Vector3(0, 0, angle);

        player.transform.up = move;

        if(numBullet <= 0)
        {
            numBullet = 0;
        }

        if (Input.GetAxisRaw("RightTrigger") < 0)
        {
            numBullet--;
            TriggerIsInUse = true;

            if (TriggerIsInUse)
            {
                myWeapon.uzi = false;
                secondShoot = true;

                timer -= Time.deltaTime;
                if (timer <= 0 && numBullet > 0)
                {
                    fire();

                    numBullet--;

                    timer = timeRemaining;

                    
                }
            }
        }
        
        else
        {
            shooting = false;
            TriggerIsInUse = false;

            if (timer > 0)
                timer -= Time.deltaTime;
        }
        
    }
    private IEnumerator DestroyBulletAfterTime(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(bullet);
    }
    public void fire()
    {
        GameObject bullet;

        bulletPrefab.transform.localScale = new Vector3(bulletScale, bulletScale, bulletScale);

        int randomNumberX = Random.Range(-dispersionHalfAngle, dispersionHalfAngle);

        bullet = Instantiate(bulletPrefab, player.transform.position + (transform.position - player.transform.position).normalized * 2, Quaternion.LookRotation(transform.position - player.transform.position));
        shooting = true;
        myMoveBullet = bullet.GetComponent<moveBullet>();
        myMoveBullet.speedBullet = speedBullet;

        bullet.transform.Rotate(randomNumberX, 0, 0);

        me.AddForce(-m_Input * forceShoot, ForceMode.Impulse);
        StartCoroutine(DestroyBulletAfterTime(bullet, BulletlifeTime));
        Physics.IgnoreCollision(bullet.GetComponent<Collider>(), bulletSpawn.GetComponent<Collider>());
    }

}
