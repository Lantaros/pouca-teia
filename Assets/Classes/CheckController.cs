using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.Experimental.UIElements.StyleSheets;


public class CheckController : MonoBehaviour
{
    public GameObject keyboardSprite;
    
    public GameObject xboxConSprite;

    public GameObject psConSprite;

    enum InputDevice {KEYBOARD, PS_CONTROLLER, XBOX_CONTROLLER};

    private InputDevice device;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {   
        device = InputDevice.KEYBOARD;

        string[] joystickNames = Input.GetJoystickNames(); 

        foreach(string name in joystickNames)
        {
            if(name.Equals("XBOX 360 For Windows")){
                device = InputDevice.XBOX_CONTROLLER;
                break;
            }
            else if(name != ""){
                device = InputDevice.XBOX_CONTROLLER;
                break;
            }
        }

        keyboardSprite.SetActive(false);
        xboxConSprite.SetActive(false);
        psConSprite.SetActive(false);

        switch(device){
            case InputDevice.KEYBOARD:
                keyboardSprite.SetActive(true);
            break;

            case InputDevice.XBOX_CONTROLLER:
                xboxConSprite.SetActive(true);
            break;

            case InputDevice.PS_CONTROLLER:
                psConSprite.SetActive(true);
            break;

            default:
                Debug.Log("ERROR: Input Device Error!!!!!");
            break;
        }
    }
}
