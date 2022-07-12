using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElixerBar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxElixer(int elixer)
    {
        slider.maxValue = elixer;
        slider.value = elixer;
    }

    public void SetElixer(int elixer)
    {
        slider.value = elixer;
    }
}
