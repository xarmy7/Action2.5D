using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMove : MonoBehaviour
{
    public GameObject cam;
    
    Rigidbody rb;

    public ammoBar ammoBar = null;

    public float moveSpeed;
    public int numShoot;
    public float dash;
    [SerializeField] float gravityDown = 2f;
    [SerializeField] float gravityUp = 0f;

    public uint playerLife = 3;
    public uint lifeMax = 3;

    public Vector3 Player_Checkpoint;
    Vector3 initPos;

    private bool moveP;
    public bool activate;
    public bool akIsDrag = false;
    public bool uziIsDrag = true;
    public bool shotGunIsDrag = false;
    public bool sniperIsDrag = false;

    [SerializeField] secondShooter secondShoot;
    PlayerGoOutCamera myPlayerCamera;
    ammo ammo;

    void OnCollisionExit(Collision collisionInfo)
    {
        if (collisionInfo.gameObject.tag == "Moving_plateform")
            transform.SetParent(null);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "ak47")
        {
            dragAk();
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "shotGun")
        {
            dragShotGun();
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "sniper")
        {
            dragSniper();
            Destroy(other.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        initPos = transform.position;
        Player_Checkpoint = initPos;
        secondShoot = FindObjectOfType<secondShooter>();
        rb = gameObject.GetComponent<Rigidbody>();

        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerLife == 0 || playerLife > lifeMax)
        {
            myPlayerCamera = GetComponent<PlayerGoOutCamera>();

            if (Player_Checkpoint.z == 0)
            {
                myPlayerCamera.depth = false;
                myPlayerCamera.above = true;

                cam.transform.position = Player_Checkpoint;
                transform.position = Player_Checkpoint;
            }
            else if (Player_Checkpoint.z >= 10)
            {
                myPlayerCamera.depth = true;
                myPlayerCamera.above = false;

                cam.transform.position = Player_Checkpoint;
                transform.position = Player_Checkpoint;
            }

            playerLife = lifeMax;
        }

        if (rb.velocity.y > 1)
            rb.AddForce(Vector3.down * gravityUp, ForceMode.Acceleration);
        else
            rb.AddForce(Vector3.down * gravityDown, ForceMode.Acceleration);

        if (activate)
        {
            moveP = isMove2();

            Vector3 m_Input2 = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
            rb.MovePosition(transform.position + m_Input2 * Time.deltaTime * moveSpeed);

            if (moveP)
            {
                if (Input.GetAxis("Horizontal") > 0)
                    transform.eulerAngles = new Vector3(transform.rotation.x, -90, transform.rotation.z);
                else
                    transform.eulerAngles = new Vector3(transform.rotation.x, 90, transform.rotation.z);
            }

        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            FindObjectOfType<GameManager>().endGame();
        }

        if (Input.GetButtonDown("A"))
        {
            rb.AddForce(Vector3.up * dash, ForceMode.Impulse);
        }
    }
    public bool isMove2()
    {
        return Input.GetButton("Horizontal");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Moving_plateform")
            transform.SetParent(collision.transform);
    }
    public void hurt(uint damage = 1)
    {
        playerLife -= damage;

        //hearts[(int)playerLife].enabled = false;

        if (gameObject == null)
        {
            playerLife = 0;
        }
    }
    public void dragAk()
    {
        secondShoot.BulletlifeTime = 2;
        secondShoot.speedBullet = 45;
        secondShoot.timeRemaining = 0.125f;
        secondShoot.forceShoot = 4;
        secondShoot.dispersionHalfAngle = 6;
        secondShoot.bulletScale = 0.5f;
        secondShoot.damage = 1;
        secondShoot.shotGun = false;
        secondShoot.numBullet = numShoot;
        akIsDrag = true;
        shotGunIsDrag = false;
        sniperIsDrag = false;
        ammoBar.setMaxAmmo(numShoot);
    }
    public void dragShotGun()
    {
        secondShoot.BulletlifeTime = 3;
        secondShoot.speedBullet = 50;
        secondShoot.timeRemaining = 0.75f;
        secondShoot.forceShoot = 17;
        secondShoot.dispersionHalfAngle = 0;
        secondShoot.bulletScale = 0.5f;
        secondShoot.damage = 5;
        secondShoot.shotGun = true;
        secondShoot.numBullet = numShoot;
        akIsDrag = false;
        shotGunIsDrag = true;
        sniperIsDrag = false;
        ammoBar.setMaxAmmo(numShoot);
    }
    public void dragSniper()
    {
        secondShoot.BulletlifeTime = 3;
        secondShoot.speedBullet = 66;
        secondShoot.timeRemaining = 1;
        secondShoot.forceShoot = 20;
        secondShoot.dispersionHalfAngle = 0;
        secondShoot.bulletScale = 0.7f;
        secondShoot.damage = 7;
        secondShoot.shotGun = false;
        secondShoot.numBullet = numShoot;
        akIsDrag = false;
        shotGunIsDrag = false;
        sniperIsDrag = true;
        ammoBar.setMaxAmmo(numShoot);   
    }

}
