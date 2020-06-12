using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowsRemainingScript : MonoBehaviour
{
    public Slider arrowSlider;

    public float maxArrows;
    public float currentArrows;

    public ShootAtMousePosition shootAtMousePosition;

    private void Update()
    {
        maxArrows = shootAtMousePosition.startingArrows;
        currentArrows = shootAtMousePosition.remainingArrows;

        arrowSlider.maxValue = shootAtMousePosition.startingArrows;
        arrowSlider.value = shootAtMousePosition.remainingArrows;
    }
}
