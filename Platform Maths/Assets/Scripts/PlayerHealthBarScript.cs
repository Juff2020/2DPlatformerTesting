using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBarScript : MonoBehaviour
{
    public Slider healthSlider;

    public float maxHealth;
    public float currentHealth;

    public PlayerController playerController;

    private void Update()
    {
        maxHealth = playerController.playerHealthStart;
        currentHealth = playerController.playerHealthRemaining;

        healthSlider.value = playerController.playerHealthRemaining;
        healthSlider.maxValue = playerController.playerHealthStart;
    }
}
