using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.Experimental.UIElements.StyleSheets;


public class CheckController : MonoBehaviour
{
    public GameObject keyboardSprite;
    
    public GameObject controllerSprite;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {   


        int count = 0;
        string[] joystickNames = Input.GetJoystickNames(); 

        foreach(string name in joystickNames)
        {
            if(name != ""){
                count++;
                break;
            }
        }

        if(count > 0)
        {
            keyboardSprite.SetActive(false);
            controllerSprite.SetActive(true);
        }
        else
        {
            controllerSprite.SetActive(false);
            keyboardSprite.SetActive(true);
        }


    }
}
