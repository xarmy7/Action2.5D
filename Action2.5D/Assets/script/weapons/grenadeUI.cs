using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class grenadeUI : MonoBehaviour
{
    public GameObject textPrefab;
    Text textPrefabText;
    public string ammoGrenade;
    public grenade numbGrenade;

    // Start is called before the first frame update
    void Start()
    {
        textPrefabText = textPrefab.GetComponent<Text>();

        
    }

    // Update is called once per frame
    void Update()
    {
        textPrefabText.text = ammoGrenade;

        if(numbGrenade.numbGrenade == 3)
        {
            ammoGrenade = "3/3";
        }
        if (numbGrenade.numbGrenade == 2)
        {
            ammoGrenade = "2/3";
        }
        if (numbGrenade.numbGrenade == 1)
        {
            ammoGrenade = "1/3";
        }
        if (numbGrenade.numbGrenade == 0)
        {
            ammoGrenade = "0/3";
        }

    }
}
