using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private float barScale;
    private float maxHP;


    void Start()
    {
        transform.localScale = new Vector2(1f, 1f);
    }

    public void SetMaxHP(float hp)
    {
        maxHP = hp;
    }

    public void UpdateBar(float health)
    {
        float currentScale = health / maxHP;
        transform.localScale = new Vector2(currentScale, 1f);
    }
}
