using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 한영 전환을 위한 코드
// 그냥 왼손 손바닥 아래쪽에 오른손 검지 닿으면 동작하는 걸로 바꿈
// 왼손 손바닥 아래에 붙이기

public class LangSwitchDetected : MonoBehaviour
{
    public GameObject switchingFinger; // 오른쪽 검지

    InputText t;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<MeshRenderer>().material.color = Color.white;

        t = GameObject.Find("Input").GetComponent<InputText>();
    }

    private void Update()
    {
        //GetComponent<MeshRenderer>().material.color = Color.white;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == switchingFinger)
        {
            Debug.Log("Switch!");    // 여기에 한영 전환 bool 넣기!!-----------

            t.changeLanguage();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == switchingFinger)
        {
            GetComponent<MeshRenderer>().material.color = Color.red;
        }
    }
    private void OnTriggerExit(Collider target)
    {
        if (target.gameObject == switchingFinger)
        {
            GetComponent<MeshRenderer>().material.color = Color.white;
        }
    }
}
