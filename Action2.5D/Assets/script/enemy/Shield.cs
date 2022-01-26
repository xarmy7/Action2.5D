using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public Vector3 m_Input;

    public GameObject enemyShield;
    public GameObject player;

    public Rigidbody playerRb;

    Material mat;

    public float force;

    [SerializeField]
    float forceExplosion = 10f;

    public uint shieldLife = 25;

    Vector3 posShield;

    Color color;
    float temp = 0f;
    float shieldOfSet = 1.25f;

    bool variable = false;
    bool explosion;
    bool inTheZone;

    Color myColor;

    playerMove myPlayerMove;
    EnemyManager enemyManager;

    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<Renderer>().material;
        myColor = new Color(1, 1, 1);

        myPlayerMove = player.GetComponent<playerMove>();
        enemyManager = GetComponent<EnemyManager>();

        enemyManager.enemyLife = shieldLife;
    }

    private void Update()
    {

        if (enemyShield == null)
        {
            Destroy(gameObject);
        }

        if (shieldLife == 0f)
        {
            explosion = true;
        }

        if (variable)
        {
            mat.color = Color.Lerp(mat.color, new Color(1, 1 - temp, 1 - temp), 1);
        }

        if (inTheZone && explosion)
        {
            myPlayerMove.hurt();
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        m_Input = (transform.position - player.transform.position).normalized;

        if (m_Input.x != 0 || m_Input.y != 0)
        {
            if(enemyShield != null)
            { 
                Vector3 dir = m_Input * shieldOfSet;
                posShield = enemyShield.transform.position - dir;

                transform.position = posShield;
                transform.LookAt(player.transform.position);

            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (player.transform.position.x > transform.position.x)
                playerRb.AddForce(Vector3.right * force, ForceMode.Impulse);
            else if (player.transform.position.x < transform.position.x)
                playerRb.AddForce(Vector3.left * force, ForceMode.Impulse);
        }

        if (collision.gameObject.tag == "Bullet")
        {
            variable = true;
            temp += 0.035f;
            enemyManager.EnemyTouche();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
            inTheZone = true;

        if (explosion)
        {
            if (other.GetComponent<Rigidbody>() != null && other.gameObject.tag != "Enemy")
            {
                Vector3 dir = (other.transform.position - transform.position).normalized;

                other.gameObject.GetComponent<Rigidbody>().AddForce(dir * forceExplosion, ForceMode.Impulse);
                Destroy(gameObject);

            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
            inTheZone = false;
    }
}
