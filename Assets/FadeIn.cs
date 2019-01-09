using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeIn : MonoBehaviour
{
    [SerializeField] float fadeInTime = 2f;
    float transparency;

    void Start()
    {
        gameObject.SetActive(true);
        transparency = this.GetComponent<CanvasRenderer>().GetAlpha();
    }

    void Update()
    {
        if (transparency > 0)
        {
            transparency -= Time.deltaTime/fadeInTime;
            this.GetComponent<CanvasRenderer>().SetAlpha(transparency);
        }
        if (transparency <= 0 & gameObject.activeSelf == true)
        {
            gameObject.SetActive(false);
        }
    }
}
