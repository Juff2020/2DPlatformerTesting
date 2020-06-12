using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpikesKillPlayer : MonoBehaviour
{

    public PlayerController playerController;

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            playerController.LevelRestart();
            Destroy(other.gameObject);
        }       
    }
}
