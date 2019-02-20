using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
    hand's base position according to origin (-0,029; 4,521, -1)
 */
public class Timer : MonoBehaviour
{

    public float totalTime;

    private bool started = false;

    private Vector3 handBasePos = new Vector3(-0.029f, 4.521f, -1f);

    /* Euler constant, rotation stop condition. It reaches this value near 360º */
    private float EULER_Z_LIMIT = 5f;
    private Transform clockHand;

    void Start()
    {   
        clockHand = this.transform.GetChild(0);
    }

    public void init(int totalTime){
        this.totalTime = totalTime;
        this.started = true;
    }

    void Update(){

        if(this.started)
            clockHand.RotateAround(handBasePos, Vector3.back, 360*Time.deltaTime/totalTime);
            
            if(clockHand.rotation.eulerAngles.z < EULER_Z_LIMIT)
                this.started = false;
    }

    bool hasFinished(){
        return !this.started;
    }

}
