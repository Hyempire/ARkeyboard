using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �浹�ϸ� �ڽ��� ���� �ٲٴ� �ڵ�
// ���ش� �ڵ� ����, �������� �ɾ ������ ��� ������Ʈ(other)�� ���� �ٲٴ� �ڵ�� �ٲ�� �Ҽ���

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
            // ���⿡ Ű�е帶���� ����� �־����!!!
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
