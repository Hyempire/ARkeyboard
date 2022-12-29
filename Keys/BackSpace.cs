using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackSpace : MonoBehaviour
{

    InputText t;

    // Start is called before the first frame update
    void Start()
    {
        t = GameObject.Find("Input").GetComponent<InputText>();
    }

    // Update is called once per frame
    void OnMouseDown()
    {
        t.backSpace();
    }
}