using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deathByFallDamageScript : MonoBehaviour
{

    private float startTimerForDeathByFallDamagePanel = 3f;
    private float remainingTimeForDeathByFallDamagePanel;

    public bool isDeadByFallDamage;

    public PlayerController playerController;
    public GameObject deathByFallDamagePanel;

    private void Start()
    {
        deathByFallDamagePanel.SetActive(false);
        isDeadByFallDamage = false;
        remainingTimeForDeathByFallDamagePanel = startTimerForDeathByFallDamagePanel;
    }

    private void Update()
    {
        if (isDeadByFallDamage == true && remainingTimeForDeathByFallDamagePanel >= 0f)
        {
            remainingTimeForDeathByFallDamagePanel = remainingTimeForDeathByFallDamagePanel - Time.deltaTime;
        }

        if (isDeadByFallDamage == true && remainingTimeForDeathByFallDamagePanel <= 0f)
        {
            HideDeathByFallDamagePanel();
        }
    }

    public void ShowDeathByFallDamagePanel()
    {
        deathByFallDamagePanel.SetActive(true);
    }

    public void HideDeathByFallDamagePanel()
    {
        isDeadByFallDamage = false;
        deathByFallDamagePanel.SetActive(false);
        playerController.LevelRestart();
    }
}
