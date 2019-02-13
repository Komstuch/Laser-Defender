using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayGS : MonoBehaviour
{
    [SerializeField] GameObject GS;
    [SerializeField] float delayTime = 1f;
    void Start()
    {
        StartCoroutine(DelayGameSpark());
    }

    IEnumerator DelayGameSpark()
    {
        yield return new WaitForSeconds(delayTime);
        GS.SetActive(true);
    }
}
