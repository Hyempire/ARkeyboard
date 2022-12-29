using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandTracking : MonoBehaviour
{
    public UDPReceive udpReceive;
    public GameObject[] handPoints1;
    public GameObject[] handPoints2;

    public Text isSocekt2;

    public GameObject _hands;
    public GameObject _hands2;
    public GameObject _keypads;

    // Start is called before the first frame update
    void Start()
    {
        isSocekt2 = GameObject.Find("isSocket").GetComponent<Text>();
        isSocekt2.text = "Waiting";
    }

    // Update is called once per frame
    void Update()
    {
        string data = udpReceive.data;
        if (data.Length > 0)
        {
            isSocekt2.text = "Receiving";
            _hands.SetActive(true);
            _hands2.SetActive(true);
            _keypads.SetActive(true);
        }

        // ���� ��ȣ ���ֱ�
        data = data.Remove(0, 1);
        data = data.Remove(data.Length - 1, 1);
        //print(data.Length);
        //print(data);
        string[] points = data.Split(',');
        //print(string.Join(",", points));
        //print(points.Length);

        // print(points[0]);

        // �� �¿�
        string handType = "";
        // string right_ = "R";
        // string left_ = "L";

        // ���� �ϳ��� ���� ���� �� : 64�� : 0~20 21�� ���帶ũ ���ϱ� 3 _ x y z ��ǥ�ϱ� + R/L
        if (points.Length < 65)
        {
            for (int i = 0; i < 22; i++)
            {
                if (i == 0)
                {
                    handType = points[i];
                    //print(handType);
                }
                if (i > 0)
                {
                    // index 3 ��°���� �ްڴ�
                    float x = 5 - float.Parse(points[i * 3 - 2]) / 100;
                    float y = float.Parse(points[i * 3 - 1]) / 100;
                    float z = float.Parse(points[i * 3]) / 100;

                    handPoints1[i - 1].transform.localPosition = new Vector3(x, y, z);
                }
            }
        }
        // �� �ϳ��� ���� + clicked!
        else if (points.Length == 65)
        {
            for (int i = 0; i < 22; i++)
            {
                if (i == 0)
                {
                    handType = points[i];
                    //print(handType);
                }
                if (i > 0)
                {
                    // index 3 ��°���� �ްڴ�
                    float x = 5 - float.Parse(points[i * 3 - 2]) / 100;
                    float y = float.Parse(points[i * 3 - 1]) / 100;
                    float z = float.Parse(points[i * 3]) / 100;

                    handPoints1[i - 1].transform.localPosition = new Vector3(x, y, z);
                }
                //                if (i == 22)
                //                {
                //                    // click���� ���� ��!
                //                    Debug.Log(points[i]);
                //                }
            }
            Debug.Log(points[points.Length - 1]);
            print(data);
        }
        // �� �� �� ����
        else if ((points.Length >= 66) && (points.Length < 129))
        {
            //print("-----Both Hands-----");
            for (int i = 0; i < 22; i++)
            {
                if (i == 0)
                {
                    handType = points[i * 3];
                    //print(handType);
                }
                if (i > 0)
                {
                    // index 3 ��°���� �ްڴ�
                    float x1 = 5 - float.Parse(points[i * 3 - 2]) / 100; // 1
                    float y1 = float.Parse(points[i * 3 - 1]) / 100;     // 2
                    float z1 = float.Parse(points[i * 3]) / 100;         // 3

                    handPoints2[i - 1].transform.localPosition = new Vector3(x1, y1, z1);
                }
            }
            for (int i = 22; i < 44; i++)
            {
                if (i == 22)
                {
                    handType = points[i * 3 - 2];   // 65 ��°�ϱ� 64
                    //print(handType);
                }
                if (i > 22)
                {
                    // index 3 ��°���� �ްڴ�
                    float x2 = 5 - float.Parse(points[(i-1) * 3-1]) / 100; // 66
                    float y2 = float.Parse(points[(i - 1) * 3]) / 100;         // 67
                    float z2 = float.Parse(points[(i - 1) * 3+1]) / 100;     // 68 --- 128��°���� �����ϱ� 127�� ����

                    handPoints1[i - 23].transform.localPosition = new Vector3(x2, y2, z2);
                }
            }
        }
        // �� �� �� ���� + clicked!
        else if (points.Length == 129)
        {
            //print("-----Both Hands-----");
            for (int i = 0; i < 22; i++)
            {
                if (i == 0)
                {
                    handType = points[i * 3];
                    //print(handType);
                }
                if (i > 0)
                {
                    // index 3 ��°���� �ްڴ�
                    float x1 = 5 - float.Parse(points[i * 3 - 2]) / 100; // 1
                    float y1 = float.Parse(points[i * 3 - 1]) / 100;     // 2
                    float z1 = float.Parse(points[i * 3]) / 100;         // 3

                    handPoints2[i - 1].transform.localPosition = new Vector3(x1, y1, z1);
                }
            }
            for (int i = 22; i < 44; i++)
            {
                if (i == 22)
                {
                    handType = points[i * 3 - 2];   // 65 ��°�ϱ� 64
                    //print(handType);
                }
                if (i > 22)
                {
                    // index 3 ��°���� �ްڴ�
                    float x2 = 5 - float.Parse(points[(i - 1) * 3 - 1]) / 100; // 66
                    float y2 = float.Parse(points[(i - 1) * 3]) / 100;         // 67
                    float z2 = float.Parse(points[(i - 1) * 3 + 1]) / 100;     // 68 --- 128��°���� �����ϱ� 127�� ����

                    handPoints1[i - 23].transform.localPosition = new Vector3(x2, y2, z2);
                }
                //if (i == 44)
                //{
                //    // click���� ���� ��!
                //    Debug.Log(points[i]);
                //}
            }
            Debug.Log(points[points.Length - 1]);
            print(data);
        }
    }
}