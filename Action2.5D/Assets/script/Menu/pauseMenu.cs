using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class pauseMenu : MonoBehaviour
{
    public bool pauseActivated = false;
    public GameObject menuPause;
    public GameObject menuOptions;
    public GameObject optionExit;

    public Image langageOption;
    public Image quitOption;
    public Material select;
    Material langage;
    Material option;

    public bool actuallyMenu;
    public bool goOption;

    public Animator resume;
    public Animator retryGame;
    public Animator options;
    public Animator exit;
    public Animator pauseOption;
    

    [SerializeField] List<GameObject> childMainMenu = new List<GameObject>();
    [SerializeField] List<GameObject> childOptions = new List<GameObject>();
    [SerializeField] float timeNext = 0.02f;
    float currTime = 0;
    public int index = 0;
    public float startDelay;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(index);
        menuPause.SetActive(false);
        menuOptions.SetActive(false);
        Transform[] ts = menuPause.GetComponentsInChildren<Transform>();
        foreach (Transform gm in ts)
        {
            if (gm.gameObject.tag == "button")
                childMainMenu.Add(gm.gameObject);
        }

        Transform[] ts2 = menuOptions.GetComponentsInChildren<Transform>();
        foreach (Transform gm2 in ts2)
        {
            if (gm2.gameObject.tag == "button")
                childOptions.Add(gm2.gameObject);
        }
        childMainMenu[index].GetComponent<Animator>().SetBool("normal", true);
        childMainMenu[index].GetComponent<Animator>().SetBool("highlighted", false);
        langage = langageOption.material;
        option = langageOption.material;
        
    }

    // Update is called once per frame
    void Update()
    {

        currTime += Time.deltaTime;
        if (Input.GetButtonDown("start"))
        {
            if (pauseActivated == false)
            {
                
                PauseGame();
            }
        }
        if(Input.GetKeyDown(KeyCode.D))
        {
            if (index + 1 == childMainMenu.Count)
                index = 0;
            else
                index++;
        }

        if (actuallyMenu)
        {
            if (Input.GetAxis("LeftJoystickVertical") < -0.9f )
            {

                childMainMenu[index].GetComponent<Animator>().SetBool("normal", true);
                childMainMenu[index].GetComponent<Animator>().SetBool("highlighted", false);

                if (index + 1 == childMainMenu.Count)
                    index = 0;
                else
                    index++;

                childMainMenu[index].GetComponent<Animator>().SetBool("normal", false);
                childMainMenu[index].GetComponent<Animator>().SetBool("highlighted", true);
                
                currTime = 0;
            }

            if (Input.GetAxis("LeftJoystickVertical") > 0.9f )
            {

                childMainMenu[index].GetComponent<Animator>().SetBool("normal", true);
                childMainMenu[index].GetComponent<Animator>().SetBool("highlighted", false);

                if (index - 1 != childMainMenu.Count)
                    index = 3;
                else
                    index--;

                childMainMenu[index].GetComponent<Animator>().SetBool("normal", false);
                childMainMenu[index].GetComponent<Animator>().SetBool("highlighted", true);

                currTime = 0;
            }
        }

        if (goOption)
        {
            if (Input.GetAxis("LeftJoystickVertical") < -0.9f )
            {

                if (index + 1 == childMainMenu.Count)
                    index = 0;
                else
                    index++;

                currTime = 0;
            }

            if (Input.GetAxis("LeftJoystickVertical") > 0.9f )
            {

                if (index - 1 != childMainMenu.Count)
                    index = 1;
                else
                    index--;

                currTime = 0;
            }
        }

        if (Input.GetButtonDown("A"))
        {
            if (index == 0 && goOption == false)
            {
                ResumeGame();
                index = 0;
            }

            else if (index == 1 && goOption == false)
            {
                retry();
            }

            else if (index == 2 && goOption == false)
            {
                openOption();
                goOption = true;
                actuallyMenu = false;
                index = 0;
            }

            else if (index == 3 && goOption == false)
            {
                exitInGame();
            }

            else if (index == 0 && goOption == true)
            {
                
            }

            else if (index == 1 && goOption == true)
            {
                exitOption();
                index = 0;
            }
        }

        if (index == 0 && goOption == true)
            langageOption.material = select;
        
        else
            langageOption.material = langage;


        if (index == 1 && goOption == true)
            quitOption.material = select;

        else
            quitOption.material = option;


        if (Input.GetKeyDown(KeyCode.F))
                {
                    Debug.Log(index);
                }
        
    }
    public void PauseGame()
    {
        menuPause.SetActive(true);
        actuallyMenu = true;
        pauseActivated = true;
        childMainMenu[index].GetComponent<Animator>().SetBool("normal", false);
        childMainMenu[index].GetComponent<Animator>().SetBool("highlighted", true);
        Time.timeScale = 0;

    }

    public void ResumeGame()
    {

        menuPause.SetActive(false);
        pauseActivated = false;
        actuallyMenu = false;
        resume.SetTrigger("Normal");
        Time.timeScale = 1;
    }

    public void retry()
    {
        ResumeGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        actuallyMenu = false;

    }

    public void exitInGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Main Menu");
    }

    public void openOption()
    {
        menuOptions.SetActive(true);
        pauseOption.SetBool("OptionsPause", true);
        actuallyMenu = false;
        goOption = true;
    }

    public void exitOption()
    {
        pauseOption.SetBool("OptionsPause", false);
        actuallyMenu = true;
        goOption = false;
    }

    public void goNormal()
    {
        resume.ResetTrigger("Highlighted");
    }
}
