using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

// 백스페이스 감지하는 코드
// 0.5초 전과 후의 position을 감지해서 특정 threshold(왼쪽으로)보다 크면 동작!
// 손 날을 세우면 0.5초 세기 시작

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
        for (int i = 5; i < 21; i++)    // 엄지쪽 빼고 네 손가락을 비교하려 함 
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

            //현재시간에서의 x포지션 평균
            if (timer == 0)
            {
                currentPosition = handPointXposition.Average();
            }
            if (timer > compareTime)
            {
                // 1초 뒤 x포지션 평균
                laterPosition = handPointXposition.Average();
                if (laterPosition - currentPosition > threshold)
                {
                    Debug.Log("Backspace!!");
                    t.backSpace();
                }
                timer = 0f;
                isBackspaceTriggered_ = false;

                triggerCount = 0;  // 0.5초가 지나면 triggrCount 다시 0으로 해줌. Trigger 판단 하게.
            }
            timer += Time.deltaTime;
        }
    }

    // 표준편차 구하기
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
        // 표준편차가, 손 쫙 펼치면 1, 검지만 피면 0.4~0.5, 세우면 0.2 정도 나오니까 threshold를 0.3정도로 하겠음
        return standardDerivation_;
    }

    // 손 날을 세웠는가
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
