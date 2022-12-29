using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetectedForPointFinger : MonoBehaviour
{

    private int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (count < 20)
        {
            other.GetComponent<MeshRenderer>().material.color = Color.white;
            count += 0;
        }
        else if (count >= 1)
        {
            other.GetComponent<MeshRenderer>().material.color = Color.red;
        }
    }
}
