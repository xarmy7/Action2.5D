using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool gameHasEnded = false;
    public float restartDelay = 1f;
    public string nextLevel;
    // Start is called before the first frame update
    void Start()
    {
    }
    private void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("reset"))
        {
            start("Main Menu");
        }

    }
    public void endGame()
    {
        if (gameHasEnded == false)
        {

            gameHasEnded = true;
            Debug.Log("GAME OVER");
            Invoke("restart", restartDelay);
            //restart();
        }

    }
    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
    public void start(string nameScene)
    {
        SceneManager.LoadScene(nameScene);
    }

}
