using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeFireV2 : MonoBehaviour
{
    public ParticleSystem myParticle;

    Material mat;

    float timer;

    [SerializeField]
    float timeRemaining = 7f;

    [SerializeField]
    float timeForReset = 3f;
    float timeDanger;

    public Vector3 initScale;
    public Vector3 warnScale;
    public Vector3 danger;

    bool firstStep = true;
    bool secondStep = false;
    bool thirdStep = false;

    public float sizeFire;

    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<Renderer>().material;

        timer = timeRemaining;

        myParticle.Stop();

    }

    // Update is called once per frame
    void Update()
    {
        var particleMain = myParticle.main;

        timer -= Time.deltaTime;

        if (timer <= 4f && firstStep)
        {
            thirdStep = false;
            mat.color = Color.yellow;
            timeDanger = timeForReset;
            secondStep = true;
            myParticle.Play();

        }

        if (timer <= 2 && secondStep)
        {
            firstStep = false;
            mat.color = new Color(1f, 0.4707f, 0f);

            particleMain.maxParticles = 8;

            thirdStep = true;
        }

        if (timer <= 0 && thirdStep)
        {
            secondStep = false;

            transform.parent.localScale = Vector3.Lerp(initScale, danger, sizeFire * Time.deltaTime);

            mat.color = Color.red;

            timeDanger -= Time.deltaTime;

            if (timeDanger <= 0)
            {
                myParticle.Stop();

                mat.color = Color.yellow;
                transform.parent.localScale = initScale;
                timer = timeRemaining;
                firstStep = true;
            }
        }
    }
}
