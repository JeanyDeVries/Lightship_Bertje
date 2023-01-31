using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDownCoin : MonoBehaviour
{
    public float timeRemaining = 10;

    private float timerAmount;

    private void Start()
    {
        timerAmount = timeRemaining; 
    }

    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        }
    }

    public void Reset()
    {
        timeRemaining = timerAmount;
    }
}
