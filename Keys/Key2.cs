using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key2 : MonoBehaviour
{
    InputText t;
    GameObject pointFinger;

    SensorConnect sensorConnect;
    bool isTriggered = false;

    BluetoothManager bluetoothManager;

    SwitchMaterial switchMaterial;

    public AudioSource audioSource;
    public AudioClip buttonSound;

    // Start is called before the first frame update
    void Start()
    {
        t = GameObject.Find("Input").GetComponent<InputText>();
        pointFinger = GameObject.Find("PointFinger");
        GetComponent<MeshRenderer>().material.color = Color.white;

        sensorConnect = GameObject.Find("Manager").GetComponent<SensorConnect>();

        bluetoothManager = GameObject.Find("Manager").GetComponent<BluetoothManager>();

        switchMaterial = GetComponent<SwitchMaterial>();
        switchMaterial.mat[0].color = Color.white;
        switchMaterial.mat[1].color = Color.white;
    }
    

    //void OnMouseDown()
    //{
    //    t.getInput(2);
    //}
    void OnTriggerStay(Collider target)
    {
        if (target.gameObject == pointFinger)
        {
            //GetComponent<MeshRenderer>().material.color = Color.red;
            switchMaterial.mat[0].color = new Color(0.6f, 0.6f, 0.6f, 1);
            switchMaterial.mat[1].color = new Color(0.6f, 0.6f, 0.6f, 1);

            if (bluetoothManager.isBluetoothClicked)
            {
                if (isTriggered == false)
                {
                    t.getInput(2);
                    print("clicked----------------------");
                    isTriggered = true;
                    audioSource.PlayOneShot(buttonSound, 1.0f);
                }
                else
                {
                    isTriggered = true;
                }
            }
            else
            {
                isTriggered = false;
            }
        }
    }
    void OnTriggerExit(Collider target)
    {
        if (target.gameObject == pointFinger)
        {
            //GetComponent<MeshRenderer>().material.color = Color.white;
            switchMaterial.mat[0].color = Color.white;
            switchMaterial.mat[1].color = Color.white;
        }
    }
    private void OnTriggerEnter(Collider target)
    {
        if (target.gameObject == pointFinger)
        {
            
        }
    }
}
