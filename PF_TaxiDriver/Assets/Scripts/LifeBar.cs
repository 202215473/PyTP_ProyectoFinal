using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;

public class LifeBar : MonoBehaviour
{
    public Slider lifeSlider;
    public Gradient lifeGradient;
    public Image fill;

    public void SetMaxLife(int life)
    {
        lifeSlider.maxValue = life;
        lifeSlider.value = life;

        fill.color = lifeGradient.Evaluate(1f);
    }

    public void SetLife(int life)
    {
        lifeSlider.value = life;
        fill.color = lifeGradient.Evaluate(lifeSlider.normalizedValue);
    }
}
