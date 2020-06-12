using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelComplete : MonoBehaviour
{
    public PlayerController playerController;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Arrow") || other.gameObject.CompareTag("Player"))
        {
            playerController.LevelComplete();
        }
    }
}
