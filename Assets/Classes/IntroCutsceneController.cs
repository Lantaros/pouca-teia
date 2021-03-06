﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro.Examples;

public class IntroCutsceneController : MonoBehaviour
{
    [SerializeField]
    private GameObject Text1;
    [SerializeField]
    private GameObject Text2;
    [SerializeField]
    private GameObject Text3;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(BeginIntro());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene(1);
        }
    }
    
    IEnumerator BeginIntro()
    {
        this.Text1.SetActive(false);
        this.Text2.SetActive(false);
        this.Text3.SetActive(false);

        yield return new WaitForSeconds(0.5f);

        this.Text1.SetActive(true);
        this.Text1.GetComponent<RollingTextFade>().Begin();

        yield return new WaitForSeconds(6.0f);
        this.Text2.SetActive(true);
        this.Text2.GetComponent<RollingTextFade>().Begin();

        yield return new WaitForSeconds(6.0f);
        this.Text3.SetActive(true);
        this.Text3.GetComponent<RollingTextFade>().Begin();

        yield return new WaitForSeconds(8.0f);
        SceneManager.LoadScene(1);
        yield return null;
        StopCoroutine(BeginIntro());
    }
}
