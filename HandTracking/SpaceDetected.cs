using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �����̽��� �����ϴ� �ڵ�. ������ ������ ������ �´����� ����
// �� ��ũ��Ʈ�� ������ ������ �ٿ��ֱ�

public class SpaceDetected : MonoBehaviour
{
    InputText t;
    public GameObject spaceFinger; // ������ ����

    float time = 0f;

    //bool isTriggered = false;

    BluetoothManager bluetoothManager;

    public AudioSource audioSource;
    public AudioClip buttonSound;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<MeshRenderer>().material.color = Color.white;

        t = GameObject.Find("Input").GetComponent<InputText>();

        bluetoothManager = GameObject.Find("Manager").GetComponent<BluetoothManager>();
    }

    private void Update()
    {
        //GetComponent<MeshRenderer>().material.color = Color.white;
        time += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == spaceFinger)
        {
            
            
            
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == spaceFinger)
        {
            GetComponent<MeshRenderer>().material.color = Color.red;
            if (time >= 1 && bluetoothManager.isBluetoothClicked)
            {
                Debug.Log("Space!");    // ���⿡ �����̽� ��� �ֱ�!!-----------
                t.textSpace();
                time = 0f;
                audioSource.PlayOneShot(buttonSound, 1.0f);
            }
        }
    }
    private void OnTriggerExit(Collider target)
    {
        if (target.gameObject == spaceFinger)
        {
            GetComponent<MeshRenderer>().material.color = Color.white;
        }
    }
}
