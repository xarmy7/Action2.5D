using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class displayDmg : MonoBehaviour
{
    public Image myLife;
    [SerializeField] playerMove player;
    public Sprite firstDmg;
    public Sprite secondDmg;
    public Sprite empty;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.playerLife == 2)
            myLife.sprite = firstDmg;


        else if (player.playerLife == 1)
            myLife.sprite = secondDmg;

        else
            myLife.sprite = empty;

    }
}
