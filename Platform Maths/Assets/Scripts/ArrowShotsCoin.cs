using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowShotsCoin : MonoBehaviour
{

    public PlayerController playerController;

    public bool canCollectCoin = false;
    public float timeNotAllowedToCollectCoin;
    public float timeRemainingBeforeNextCollectCoin;

    public void Update()
    {
        timeNotAllowedToCollectCoin = 2f;

        if (canCollectCoin == false)
        {
            timeRemainingBeforeNextCollectCoin = timeRemainingBeforeNextCollectCoin - Time.deltaTime;
        }

    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.gameObject.CompareTag("Arrow") || other.gameObject.CompareTag("PlayerCollection")) && timeRemainingBeforeNextCollectCoin <= 0)
        {
            timeRemainingBeforeNextCollectCoin = timeNotAllowedToCollectCoin;
            CheckOkToCollectCoin();
        }       
    }

    public void CheckOkToCollectCoin()
    {
        canCollectCoin = true;

        if (timeRemainingBeforeNextCollectCoin >= 0)
        {
            if (canCollectCoin == true)
            {
                Destroy(gameObject);
                canCollectCoin = false;

                InCreasePlayerScore();
            }
        }

    }

    public void InCreasePlayerScore()
    {
        playerController.playerScore = playerController.playerScore + 1;
    }


}
