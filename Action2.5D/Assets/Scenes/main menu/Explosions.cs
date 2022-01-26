using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class Explosions : MonoBehaviour
{

    myMenu mn;
    public int RandomNumber;
    public Animation PlayA;
    public Animation PlayL;
    public Animation PlayE;

    public AnimationClip ExIdle;
    public AnimationClip Ex1;
    public AnimationClip Ex2;
    public AnimationClip Ex3;
    public AnimationClip Ex4;
    public AnimationClip Ex5;
    public AnimationClip Ex6;
    public AnimationClip Ex7;

    private bool localAM = true;
    // Start is called before the first frame update
    void Start()
    {
        mn = gameObject.GetComponent<myMenu>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(mn.index);
        RandomNumber = Random.Range(1,7);
        
        if (localAM)
        {
            Debug.Log("Pass");
            if (Input.GetButtonDown("A"))
            {
                if (mn.index == 0)
                {
                    if (RandomNumber == 1)
                    {
                        PlayA.clip = Ex2;
                        PlayA.Play();
                    }
                    if (RandomNumber == 2)
                    {
                        PlayA.clip = Ex2;
                        PlayA.Play();
                    }
                    if (RandomNumber == 3)
                    {
                        PlayA.clip = Ex3;
                        PlayA.Play();
                    }
                    if (RandomNumber == 4)
                    {
                        PlayA.clip = Ex4;
                        PlayA.Play();
                    }
                    if (RandomNumber == 5)
                    {
                        PlayA.clip = Ex5;
                        PlayA.Play();
                    }
                    if (RandomNumber == 6)
                    {
                        PlayA.clip = Ex6;
                        PlayA.Play();
                    }
                    if (RandomNumber == 7)
                    {
                        PlayA.clip = Ex7;
                        PlayA.Play();
                    }
                }
                if (mn.index == 1)
                {
                    if (RandomNumber == 1)
                    {
                        PlayL.clip = Ex2;
                        PlayL.Play();
                    }
                    if (RandomNumber == 2)
                    {
                        PlayL.clip = Ex2;
                        PlayL.Play();
                    }
                    if (RandomNumber == 3)
                    {
                        PlayL.clip = Ex3;
                        PlayL.Play();
                    }
                    if (RandomNumber == 4)
                    {
                        PlayL.clip = Ex4;
                        PlayL.Play();
                    }
                    if (RandomNumber == 5)
                    {
                        PlayL.clip = Ex5;
                        PlayL.Play();
                    }
                    if (RandomNumber == 6)
                    {
                        PlayL.clip = Ex6;
                        PlayL.Play();
                    }
                    if (RandomNumber == 7)
                    {
                        PlayL.clip = Ex7;
                        PlayL.Play();
                    }
                    StartCoroutine(Vibrator());
                }   
                if (mn.index == 2)
                {
                    if (RandomNumber == 1)
                    {
                        PlayE.clip = Ex2;
                        PlayE.Play();
                    }
                    if (RandomNumber == 2)
                    {
                        PlayE.clip = Ex2;
                        PlayE.Play();
                    }
                    if (RandomNumber == 3)
                    {
                        PlayE.clip = Ex3;
                        PlayE.Play();
                    }
                    if (RandomNumber == 4)
                    {
                        PlayE.clip = Ex4;
                        PlayE.Play();
                    }
                    if (RandomNumber == 5)
                    {
                        PlayE.clip = Ex5;
                        PlayE.Play();
                    }
                    if (RandomNumber == 6)
                    {
                        PlayE.clip = Ex6;
                        PlayE.Play();
                    }
                    if (RandomNumber == 7)
                    {
                        PlayE.clip = Ex7;
                        PlayE.Play();
                    }
                    StartCoroutine(Vibrator());
                }
            }
        }
        else
        {
            localAM = mn.actuallyMenu;
        }

        if (Input.GetButtonDown("B"))
        {
            PlayE.clip = ExIdle;
            PlayL.clip = ExIdle;
            PlayA.clip = ExIdle;
            PlayA.Play();
            PlayL.Play();
            PlayE.Play();
        }
        
        IEnumerator Vibrator()
        {
            localAM = false;
            GamePad.SetVibration(0, 0.1f, 0.1f);
            yield return new WaitForSeconds(0.2f);
            GamePad.SetVibration(0, 0f, 0f);
        }

    }
}
