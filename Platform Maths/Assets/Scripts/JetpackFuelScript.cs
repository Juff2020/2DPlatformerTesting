using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JetpackFuelScript : MonoBehaviour
{

    public Slider fuelSlider;

    public float maxFuel;
    public float currentFuel;

    public PlayerController playerController;

    private void Update()
    {
        maxFuel = playerController.jetpackFuel;
        currentFuel = playerController.jetpackFuelRemaining;

        fuelSlider.value = playerController.jetpackFuelRemaining;
        fuelSlider.maxValue = playerController.jetpackFuel;
    }
}
