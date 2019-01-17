using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireButton : MonoBehaviour
{
    Color startButtonColor;

    private void Start()
    {
        startButtonColor = GetComponent<Image>().color;
    }

    public void FireColor()
    {
        GetComponent<Image>().color = new Color(0.35f, 0.35f, 0.35f);
    }

    public void IdleColor()
    {
        GetComponent<Image>().color = startButtonColor;
    }
}
