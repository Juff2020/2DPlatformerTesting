using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarUpgrade : MonoBehaviour
{

    public PlayerController playerController;

    private bool canCollectStar;
    private bool increaseByOne;

    public void OnTriggerEnter2D(Collider2D other)
    {
        canCollectStar = true;

        if ((other.gameObject.CompareTag("Arrow") || other.gameObject.CompareTag("PlayerCollection")) && canCollectStar == true)
        {
            Destroy(gameObject);
            canCollectStar = false;
            increaseByOne = true;

            IncreaseStarUpgrade();
        }
    }

    public void IncreaseStarUpgrade()
    {
        if (increaseByOne == true)
        {
            increaseByOne = false;
            playerController.starUpgrades++;
        }
    }
}
