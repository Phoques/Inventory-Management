using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    //Time time;

    public TimerSO timerSo;

    public Text timerText;

    private void Start()
    {
        
    }

    private void Update()
    {
        TimerTest();
        timerText.text = timerSo.time.ToString();
    }


    public void TimerTest()
    {
        timerSo.time += Time.deltaTime;

        Debug.Log(timerSo.time);
    }

}
