using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

// �齺���̽� �����ϴ� �ڵ�
// 0.5�� ���� ���� position�� �����ؼ� Ư�� threshold(��������)���� ũ�� ����!
// �� ���� ����� 0.5�� ���� ����

public class BackSpaceDetected : MonoBehaviour
{
    public GameObject[] hand2points;
    private Vector3[] handPointsPosition = new Vector3[16];
    float[] handPointXposition = new float[16];

    public float trigerThreshold = 0.3f;
    public bool isBackspaceTriggered_ = false;

    float timer = 0f;
    float compareTime = 0.5f;
    int triggerCount = 0;

    float currentPosition = 0f;
    float laterPosition = 10f;
    public float threshold = 0.2f;

    public InputText t;
    public GameObject backspaceUI;

    // Start is called before the first frame update
    void Start()
    {
        //t = GameObject.Find("Input").GetComponent<InputText>();
    }

    // Update is called once per frame
    void Update()
    {
        backspaceUI.SetActive(false);
        for (int i = 5; i < 21; i++)    // ������ ���� �� �հ����� ���Ϸ� �� 
        {
            GameObject handPoint = hand2points[i];
            //Debug.Log(handPoint.transform.position);
            handPointsPosition[i-5] = handPoint.transform.position;
            handPointXposition[i-5] = handPoint.transform.position.z;
        }
        //print(handPointsPosition[5]);
        //print(handPointXposition[5]);
        //print(handPointsPosition.Length);

        double standardDerivation = GetStandartDerivation(handPointXposition);
        //print(standardDerivation);

        if (triggerCount == 0)
        {
            isBackspaceTriggered_ = isBackspaceTriggered(standardDerivation);
        }
        if (isBackspaceTriggered_)
        {
            triggerCount += 1;
            Debug.Log("Backspace triggered"); //"Backspace triggered" + triggerCount.ToString()

            backspaceUI.SetActive(true);

            //����ð������� x������ ���
            if (timer == 0)
            {
                currentPosition = handPointXposition.Average();
            }
            if (timer > compareTime)
            {
                // 1�� �� x������ ���
                laterPosition = handPointXposition.Average();
                if (laterPosition - currentPosition > threshold)
                {
                    Debug.Log("Backspace!!");
                    t.backSpace();
                }
                timer = 0f;
                isBackspaceTriggered_ = false;

                triggerCount = 0;  // 0.5�ʰ� ������ triggrCount �ٽ� 0���� ����. Trigger �Ǵ� �ϰ�.
            }
            timer += Time.deltaTime;
        }
    }

    // ǥ������ ���ϱ�
    private double GetStandartDerivation(float[] xPositionArray)
    {
        double average_ = xPositionArray.Average();
        double sumOfDerivation = 0;
        foreach (double value_ in xPositionArray)
        {
            sumOfDerivation += (value_) * (value_);
        }
        double sumOfDerivationAverage = sumOfDerivation / (xPositionArray.Length - 1);
        double standardDerivation_ = Math.Sqrt(sumOfDerivationAverage - (average_ * average_));
        // ǥ��������, �� �� ��ġ�� 1, ������ �Ǹ� 0.4~0.5, ����� 0.2 ���� �����ϱ� threshold�� 0.3������ �ϰ���
        return standardDerivation_;
    }

    // �� ���� �����°�
    private bool isBackspaceTriggered(double standardDerivation)
    {
        bool isTriggered = false;
        if (standardDerivation < trigerThreshold && standardDerivation > 0)
        {
            isTriggered = true;
            
        }
        return isTriggered;
    }
}
