using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stamina : MonoBehaviour
{
    public float v = 0.5f;
    public RectTransform im;
    public float refillTime = 4.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        v += Time.deltaTime/refillTime;
        if (v > 1.0f) { v = 1.0f; }
        im.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 300.0f * v);

    }
}
