using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class SensorConnect : MonoBehaviour
{
    SerialPort sp;

    public bool isClicked;

    // Start is called before the first frame update
    void Start()
    {
        sp = new SerialPort("COM10", 9600);
        sp.Open();
        sp.ReadTimeout = 100;   // 읽기 작업을 마쳐야 하는 제한 시간(밀리초)
    }

    // Update is called once per frame
    void Update()
    {
        isClicked = isSensorClicked();
        if (sp.IsOpen)
        {
            Debug.Log("------------Arduino Opened------------");
        }
    }

    public bool isSensorClicked()
    {
        if (sp.IsOpen)
        {
            string arduinoData = sp.ReadLine();
            //print(arduinoData);
            
            if (int.Parse(arduinoData) == 1)
            {
                //print("Clicked!");
                return true;
            }
            else if (int.Parse(arduinoData) == 0)
            {
                //print("0");
                return false;
            }
            return false;
        }
        else
        {
            return false;
        }
    }
}
