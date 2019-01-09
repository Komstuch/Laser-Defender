using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{
    [SerializeField] float fadeInTime = 2f;

    Image fadePanel;
    Color currentColor;
    float transparency;

    private void Awake()
    {
        gameObject.SetActive(true);
    }

    void Start()
    {
        currentColor = new Color(0, 0, 0, 1f);
        fadePanel = GetComponent<Image>();
        fadePanel.color = currentColor;
        transparency = fadePanel.color.a;
    }

    void Update()
    {
        if (transparency > 0)
        {
            transparency -= Time.deltaTime / fadeInTime;
            currentColor.a = transparency;
            fadePanel.color = currentColor;            
        }
        if (transparency <= 0 & gameObject.activeSelf == true)
        {
            gameObject.SetActive(false);
        }
    }
}
