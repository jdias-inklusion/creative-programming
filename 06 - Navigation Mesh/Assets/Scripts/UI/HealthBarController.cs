using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    public Slider _slider;
    public Gradient gradient;
    public Image fill;

    public void SetHealth(int health)
    {
        _slider.value = health;

        fill.color = gradient.Evaluate(_slider.normalizedValue);
    }

    public void setMaxHealth(int health)
    {
        _slider.maxValue = health;
        _slider.value = health;

        fill.color = gradient.Evaluate(1f);
    }
}
