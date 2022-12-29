using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineCode : MonoBehaviour
{
    LineRenderer lineRenderer;

    public Transform origin;
    public Transform destination;
    public float lineWidth = 0.01f;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;
    }

    // Update is called once per frame
    void Update()
    {
        // startingpoint�� ������ ����. �ε����� 0��
        lineRenderer.SetPosition(0, origin.localPosition);
        // endingpoint�� ������ ����. �ε����� 1��
        lineRenderer.SetPosition(1, destination.localPosition);
    }
}
