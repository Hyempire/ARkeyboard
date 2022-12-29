using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 충돌하면 자신의 색을 바꾸는 코드
// 태준님 코드 보고, 검지에다 심어서 검지에 닿는 오브젝트(other)의 색을 바꾸는 코드로 바꿔야 할수도

public class CollisionDetected : MonoBehaviour
{
    public GameObject pointFinger;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<MeshRenderer>().material.color = Color.white;
    }


    void OnTriggerStay(Collider target)
    {
        if (target.gameObject == pointFinger)
        {
            GetComponent<MeshRenderer>().material.color = Color.red;
            // 여기에 키패드마다의 기능을 넣어야함!!!
        }
    }
    private void OnTriggerExit(Collider target)
    {
        if (target.gameObject == pointFinger)
        {
            GetComponent<MeshRenderer>().material.color = Color.white;
        }
    }

}
