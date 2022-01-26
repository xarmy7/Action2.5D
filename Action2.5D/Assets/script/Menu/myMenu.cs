using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class myMenu : MonoBehaviour
{
    public Animator cam;
    [SerializeField]bool level = false;
    [SerializeField]bool option = false;
    [SerializeField] Material select;
    [SerializeField] Material noSelect;
    [SerializeField] GameObject MainMenu;
    [SerializeField] GameObject levels;
    [SerializeField] GameObject options;
    [SerializeField] List<GameObject> childMainMenu = new List<GameObject>();
    [SerializeField] List<GameObject> childLevels = new List<GameObject>();
    [SerializeField] List<GameObject> childOptions = new List<GameObject>();
    [SerializeField] float timeNext = 0.2f;
    float currTime = 0;
    public int index = 0;
    bool goLevel = false;
    public bool actuallyMenu = true;
    bool goOption = false;
    public float startDelay;



    // Start is called before the first frame update
    void Start()
    {
        cam = gameObject.GetComponent<Animator>();

        Transform[] ts = MainMenu.GetComponentsInChildren<Transform>();
        foreach (Transform gm in ts)
        {
            if(gm.gameObject.tag == "button")
                childMainMenu.Add(gm.gameObject);
        }

        Transform[] ts2 = levels.GetComponentsInChildren<Transform>();
        foreach (Transform gm2 in ts2)
        {
            if (gm2.gameObject.tag == "button")
                childLevels.Add(gm2.gameObject);
        }

        /*Transform[] ts3 = options.GetComponentsInChildren<Transform>();
        foreach (Transform gm3 in ts3)
        {
            if (gm3.gameObject.tag == "button")
                childOptions.Add(gm3.gameObject);
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        if(actuallyMenu)
        {
            if (Input.GetAxis("LeftJoystickVertical") < -0.9f && currTime >= timeNext)
            {

                childMainMenu[index].GetComponent<Renderer>().material = noSelect;

                if (index + 1 == childMainMenu.Count)
                    index = 0;
                else
                    index++;

                childMainMenu[index].GetComponent<Renderer>().material = select;
                currTime = 0;
            }

            if (Input.GetAxis("LeftJoystickVertical") > 0.9f && currTime >= timeNext)
            {

                childMainMenu[index].GetComponent<Renderer>().material = noSelect;

                if (index - 1 == -1)
                    index = 2;
                else
                    index--;

                childMainMenu[index].GetComponent<Renderer>().material = select;
                currTime = 0;
            }
        }

        if (goLevel)
        {
            if (Input.GetAxis("LeftJoystickVertical") < -0.9f && currTime >= timeNext)
            {

                childLevels[index].GetComponent<Renderer>().material = noSelect;

                if (index + 1 == childLevels.Count)
                    index = 0;
                else
                    index++;

                childLevels[index].GetComponent<Renderer>().material = select;
                currTime = 0;
            }

            if (Input.GetAxis("LeftJoystickVertical") > 0.9f && currTime >= timeNext)
            {

                childLevels[index].GetComponent<Renderer>().material = noSelect;

                if (index - 1 == -1)
                    index = 4;
                else
                    index--;

                childLevels[index].GetComponent<Renderer>().material = select;
                currTime = 0;
            }
        }

        /*if (goOption)
        {
            if (Input.GetAxis("LeftJoystickVertical") < -0.9f && currTime >= timeNext)
            {

                childOptions[index].GetComponent<Renderer>().material = noSelect;

                if (index + 1 == childOptions.Count)
                    index = 0;
                else
                    index++;

                childOptions[index].GetComponent<Renderer>().material = select;
                currTime = 0;
            }

            if (Input.GetAxis("LeftJoystickVertical") > 0.9f && currTime >= timeNext)
            {

                childOptions[index].GetComponent<Renderer>().material = noSelect;

                if (index - 1 == -1)
                    index = 3;
                else
                    index--;

                childOptions[index].GetComponent<Renderer>().material = select;
                currTime = 0;
            }
        }*/


        if (Input.GetButtonDown("A"))
        {
            if(index == 1 && goLevel == false)
            {
                cam.SetBool("GoLevelSelection", true);
                cam.SetBool("GoMenuFromLvLSelection", false);
                cam.SetBool("GoOptionsMenu", false);
                cam.SetBool("GoMenuFromOptions", false);

                level = true;
                actuallyMenu = false;
                goLevel = true;

                SwitchColor(ref childMainMenu, 0, ref childLevels, index);
                StartCoroutine(DelayIndex());   

            }



            else if (index == 0 && goLevel == false)
            {
                start("Eric_Tuto");
            }

            else if (index == 2 && goLevel == false)
            {
                Application.Quit();
            }

            else if (index == 0 && goLevel == true)
            {
                start("Eric_Tuto");
            }

            else if (index == 1 && goLevel == true)
            {
                start("Kieran_Level");
            }

            else if (index == 2 && goLevel == true)
            {
                start("Karim_Level");
            }

            else if (index == 3 && goLevel == true)
            {
                start("Eric_Level");
            }

            else if (index == 4 && goLevel == true)
            {
                start("Arnaud_Level");
            }
        }



        if (Input.GetButtonDown("B"))
        {
            if(level)
            {
                cam.SetBool("GoLevelSelection", false);
                cam.SetBool("GoMenuFromLvLSelection", true);
                cam.SetBool("GoOptionsMenu", false);
                cam.SetBool("GoMenuFromOptions", false);
                level = false;
                actuallyMenu = true;
                goLevel = false;
                StartCoroutine(DelayIndex());
                //SwitchColor(ref childLevels, 0, ref childMainMenu, index);
            }
            /*if(option)
            {
                cam.SetBool("GoLevelSelection", false);
                cam.SetBool("GoMenuFromLvLSelection", false);
                cam.SetBool("GoOptionsMenu", false);
                cam.SetBool("GoMenuFromOptions", true);
                option = false;
                actuallyMenu = true;
                goOption = false;
                StartCoroutine(DelayIndex());
                //SwitchColor(ref childOptions, 0, ref childMainMenu, index);
            }*/
        }
        currTime += Time.deltaTime;
    }
    public void start(string nameScene)
    {
        SceneManager.LoadScene(nameScene);
    }

    void SwitchColor(ref List<GameObject> Select,int indexSelect, ref List<GameObject> NoSelect, int indexNoSelect)
    {
        childMainMenu[indexNoSelect].GetComponent<Renderer>().material = noSelect;
        childLevels[indexSelect].GetComponent<Renderer>().material = select;
    }


    IEnumerator DelayIndex()
    {
        yield return new WaitForSeconds(0.1f);
        index = 0;
    }
}
