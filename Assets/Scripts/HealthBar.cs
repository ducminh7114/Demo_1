using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HealthBar : MonoBehaviour
{
    public RectTransform Mask;
    public Health health;

    private float originalWidth;


    void Start()
    {
        originalWidth = Mask.sizeDelta.x;
        UpdateHealthValue();
        health.onHealthChange += UpdateHealthValue;
    }

    private void UpdateHealthValue()
    {
        float scale = (float)health.healthPoint / health.defaultHealthPoint;
        Mask.sizeDelta = new Vector2(scale * originalWidth, Mask.sizeDelta.y); 
    }
}
