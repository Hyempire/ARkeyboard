using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �ѿ� ��ȯ�� ���� �ڵ�
// �׳� �޼� �չٴ� �Ʒ��ʿ� ������ ���� ������ �����ϴ� �ɷ� �ٲ�
// �޼� �չٴ� �Ʒ��� ���̱�

public class LangSwitchDetected : MonoBehaviour
{
    public GameObject switchingFinger; // ������ ����

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
            Debug.Log("Switch!");    // ���⿡ �ѿ� ��ȯ bool �ֱ�!!-----------

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
