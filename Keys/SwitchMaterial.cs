using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchMaterial : MonoBehaviour
{
    InputText inputText;
    bool isKorean;
    public Material[] mat = new Material[2];

    // Start is called before the first frame update
    void Start()
    {
        inputText = GameObject.Find("Input").GetComponent<InputText>();
    }

    // Update is called once per frame
    void Update()
    {
        isKorean = inputText.language;
        if (isKorean)
        {
            gameObject.GetComponent<MeshRenderer>().material = mat[0];
            print(isKorean);
        }
        else if (!isKorean)
        {
            gameObject.GetComponent<MeshRenderer>().material = mat[1];
            print(isKorean);
        }
        else
        {
            gameObject.GetComponent<MeshRenderer>().material = mat[0];
            print(isKorean);
        }
    }
}
