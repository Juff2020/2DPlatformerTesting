using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpikesKillPlayer : MonoBehaviour
{

    public PlayerController playerController;
    public GameObject deathBySpikesPanel;

    private float startTimerForDeathBySpikesPanel = 3f;
    private float remainingTimeForDeathBySpikesPanel;

    public bool isDeadBySpikes;

    private void Start()
    {
        deathBySpikesPanel.SetActive(false);
        isDeadBySpikes = false;
        remainingTimeForDeathBySpikesPanel = startTimerForDeathBySpikesPanel;
    }

    private void Update()
    {
        if (isDeadBySpikes == true && remainingTimeForDeathBySpikesPanel >= 0f)
        {
            remainingTimeForDeathBySpikesPanel = remainingTimeForDeathBySpikesPanel - Time.deltaTime;
        }

        if (isDeadBySpikes == true && remainingTimeForDeathBySpikesPanel <= 0f)
        {
            HideDeathBySpikesPanel();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            isDeadBySpikes = true;
            Destroy(other.gameObject);
            ShowDeathBySpikesPanel();
        }       
    }

    public void ShowDeathBySpikesPanel()
    {
        deathBySpikesPanel.SetActive(true);
    }

    public void HideDeathBySpikesPanel()
    {
        isDeadBySpikes = false;
        deathBySpikesPanel.SetActive(false);
        playerController.LevelRestart();
    }
}
