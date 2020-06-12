using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FallSpeedBarScript : MonoBehaviour
{
    public Slider fallSpeedSlider;

    public float maxFallSpeed;
    public float currentFallSpeed;

    public PlayerController playerController;

    private void Update()
    {
        maxFallSpeed = playerController.maxSafeFallSpeed;
        currentFallSpeed = playerController.fallVelocity - (playerController.fallVelocity*2);

        fallSpeedSlider.maxValue = playerController.maxSafeFallSpeed;
        fallSpeedSlider.value = playerController.fallVelocity - (playerController.fallVelocity * 2);
    }
}
