using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Language: MonoBehaviour
{
    InputText t;

    // Start is called before the first frame update
    void Start()
    {
        t = GameObject.Find("Input").GetComponent<InputText>();
    }

    void OnMouseDown()
    {
        t.changeLanguage();
    }
}
