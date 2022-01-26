using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animPauseMenu : MonoBehaviour
{
    public Animator animResume;
    public Animator animRetry;
    public Animator animOption;
    public Animator animOpenOption;
    public bool isPause = false;
    // Start is called before the first frame update
    void Start()
    {
        isPause = FindObjectOfType<pauseMenu>().pauseActivated;
        animRetry = gameObject.GetComponent<Animator>();
        animResume = gameObject.GetComponent<Animator>();
        animOption = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            animOpenOption.SetBool("OptionsPause", true);
        }
    }
}
