using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ArduinoBluetoothAPI;
using System;

public class BluetoothManager : MonoBehaviour
{
    BluetoothHelper bluetoothHelper_;
    string deviceName_;

    public GameObject sphere_;
    public Text text;

    string received_message_;

    public bool isBluetoothClicked = false;

    // Start is called before the first frame update
    void Start()
    {
        deviceName_ = "HC-06"; //bluetooth should be turned ON;

        try
        {
            bluetoothHelper_ = BluetoothHelper.GetInstance(deviceName_);
            bluetoothHelper_.OnConnected += OnConnected;
            bluetoothHelper_.OnConnectionFailed += OnConnectionFailed;
            bluetoothHelper_.OnDataReceived += OnMessageReceived; //read the data

            bluetoothHelper_.setTerminatorBasedStream("\n"); //delimits received messages based on \n char

            LinkedList<BluetoothDevice> ds = bluetoothHelper_.getPairedDevicesList();

            foreach (BluetoothDevice d in ds)
            {
                Debug.Log($"{d.DeviceName} {d.DeviceAddress}");
            }

            bluetoothHelper_.Connect();
        }
        catch (Exception ex)
        {
            sphere_.GetComponent<Renderer>().material.color = Color.yellow;
            Debug.Log(ex.Message);
        }
    }

    // Update is called once per frame
    void Update()
    {
            
    }

    void OnConnected(BluetoothHelper helper)
    {
        sphere_.GetComponent<Renderer>().material.color = Color.green;
        text.text = "Connected";
        try
        {
            helper.StartListening();
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }

    }

    void OnConnectionFailed(BluetoothHelper helper)
    {
        sphere_.GetComponent<Renderer>().material.color = Color.red;
        text.text = "Connection Failed";
    }

    void OnMessageReceived(BluetoothHelper helper)
    {
        //StartCoroutine(blinkSphere());
        received_message_ = bluetoothHelper_.Read();
        Debug.Log(received_message_);
        text.text = received_message_;

        if (int.Parse(received_message_) == 1)
        {
            isBluetoothClicked = true;
            text.text = received_message_;
        }
        else if (int.Parse(received_message_) == 0)
        {
            isBluetoothClicked = false;
            text.text = received_message_;
        }
        else
        {
            isBluetoothClicked = false;
            text.text = received_message_;
        }

        //text.text = received_message_;
        // Debug.Log(received_message);
    }
}
