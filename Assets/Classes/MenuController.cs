using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string[] names = Input.GetJoystickNames();

        print("Joy length " + names.Length);


        foreach(string name in names){
            print(name);
        }
    }

    public void startRound(GameManager gameManager)
    {
        gameManager.roundStarted = true;
        GameObject obj =  GameObject.Find("MenuCanvas");

        if (obj != null){
           obj.SetActive(false);
         }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
