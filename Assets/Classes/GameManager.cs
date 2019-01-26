﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Dependencies")]
    public GameObject TimeUI;
    public GameObject[] bugs;

    [Header("Settings")]
    public int BuildTime;
    public int HuntTime;
    public float[] SpawnTimeLimits;

    private UnityEngine.UI.Text timeText;
    private float currentTime;
    private float spawnTime = 0.0f;
    private float spawnTimeTarget = 1.0f;
    private string mode = "building"; //possible values: building, hunting, showingResults

    private void Start()
    {
        this.timeText = TimeUI.GetComponent<UnityEngine.UI.Text>();
        this.currentTime = this.BuildTime;
    }

    private void Update()
    {
        this.currentTime -= Time.deltaTime;
        
        if (this.mode == "building")
        {
            this.timeText.text = ((int) this.currentTime).ToString();
        }
        else if (this.mode == "hunting")
        {
            this.SpawnBugs();
        }

        if ((int) this.currentTime == 0)
        {
            if (this.mode == "building")
            {
                this.mode = "hunting";
                this.currentTime = this.HuntTime;
            }
            else if (this.mode == "hunting")
            {
                this.mode = "showingResults";
                this.currentTime = this.BuildTime;
            }
        }
    }

    private void SpawnBugs()
    {
        this.spawnTime += Time.deltaTime;
        if (this.spawnTime > this.spawnTimeTarget)
        {
            this.spawnTimeTarget = Random.Range(this.SpawnTimeLimits[0], this.SpawnTimeLimits[1]);
            this.spawnTime = 0.0f;

            Instantiate(this.bugs[0]);
        }
    }
}