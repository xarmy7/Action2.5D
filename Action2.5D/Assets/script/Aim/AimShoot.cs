using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimShoot : MonoBehaviour
{
    AudioSource m_AudioSourceUzi = null;

    AudioClip initClip = null;

    public GameObject player = null;
    public GameObject bulletPrefab = null;

    public Rigidbody me = null;

    public Transform bulletSpawn = null;

    public Vector3 posAim = Vector3.zero;
    public Vector3 m_Input = Vector3.zero;
    [HideInInspector] public Vector3 dir = Vector3.zero;
    Vector3 move = Vector3.zero;

    public float BulletlifeTime = 0f;
    public float speedBullet = 0f;
    public float timeRemaining = 2f;
    public float damage = 1f;
    [SerializeField] float forceShoot = 1.75f;
    [SerializeField] float bulletScale = 0.3f;
    float timer = 0f;

    [SerializeField] int dispersionHalfAngle = 30;

    public bool TriggerIsInUse;
    [HideInInspector] public bool uzi = true;
    [HideInInspector] public bool shooting = false;

    moveBullet myMoveBullet = null;
    secondShooter mySecond = null;
    WeaponsOnPlayer weapOnPlayer = null;

    // Start is called before the first frame update
    void Start()
    {
        uzi = true;
        mySecond = GetComponent<secondShooter>();
        m_AudioSourceUzi = GetComponent<AudioSource>();
    }
   
    // Update is called once per frame
    void Update()
    {
        m_Input = new Vector3(Input.GetAxis("LeftJoystickHorizontal"), Input.GetAxis("LeftJoystickVertical"), 0).normalized;
  
        //maybe add↓
        // if (m_Input.x != 0 || m_Input.y != 0)

        dir = m_Input * 2;
        posAim = player.transform.position + dir;

        transform.position = posAim;

        float angle = Vector3.Angle(Vector3.right, m_Input);

        Quaternion quat = Quaternion.AngleAxis(angle, Vector3.forward);

        transform.eulerAngles = new Vector3(0, 0, angle);

        player.transform.up = move;

        if (Input.GetAxisRaw("RightTrigger") > 0)
        {
            TriggerIsInUse = true;

            if (!m_AudioSourceUzi.isPlaying)
            {
                m_AudioSourceUzi.Play();
            }

            if (TriggerIsInUse)
            {
                mySecond.secondShoot = false;
                uzi = true;

                timer -= Time.deltaTime;

                if (timer <= 0)
                {
                    fire();

                    timer = timeRemaining;
                }
            }
        }
        else
        {
            shooting = false;
            TriggerIsInUse = false;
            m_AudioSourceUzi.Stop();
        }

        if (Input.GetButtonDown("X"))
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                GameObject bullet;
                for (int i = 0; i <= 0; i++)
                {
                    
                    bullet = Instantiate(bulletPrefab, player.transform.position + (transform.position - player.transform.position).normalized * 2, Quaternion.LookRotation(transform.position - player.transform.position));
                StartCoroutine(DestroyBulletAfterTime(bullet, 1f));
                   
                }
                timer = timeRemaining;
            }
            
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
