using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameManager gameManager;

    public GameObject MainMenuPanel;
    public GameObject HelpMenuPanel;

    private enum MenuState {MAIN, HELP, NONE};
    private MenuState currentState;
    
    // Start is called before the first frame update
    void Start()
    {
        currentState = MenuState.MAIN;
    }

    public void startRound()
    {
        gameManager.startRound();
        currentState = MenuState.NONE;
        GameObject obj =  GameObject.Find("MenuCanvas");

        if (obj != null){
           obj.SetActive(false);
         }
    }

    // Update is called once per frame
    void Update()
    {
        switch(currentState)
        {
            case MenuState.MAIN:
                if(Input.GetButton("Play"))
                {
                    startRound();
                }
                else if(Input.GetButton("Help"))
                {
                    currentState = MenuState.HELP;
                    MainMenuPanel.SetActive(false);
                    HelpMenuPanel.SetActive(true);
                }
                break;
            case MenuState.HELP:
                if(Input.GetButton("Back"))
                {
                    currentState = MenuState.MAIN;
                    MainMenuPanel.SetActive(true);
                    HelpMenuPanel.SetActive(false);
                }
                break;
            default:
                break;
        }


    }
}
