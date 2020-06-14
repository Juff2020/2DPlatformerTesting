using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundCheckScript : MonoBehaviour
{
    public Animator animator;
    public PlayerController playerControllerScript;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            animator.SetBool("isFalling", false);
            animator.SetBool("isJumping", false);
            animator.SetBool("isJetpacking", false);
            playerControllerScript.isJumping = false;
            playerControllerScript.isGrounded = true;
            playerControllerScript.airJumpsRemaining = playerControllerScript.totalAirJumps;

            playerControllerScript.fallVelocityOnImpact = Mathf.Abs(playerControllerScript.fallVelocity);

            if (playerControllerScript.fallVelocityOnImpact >= playerControllerScript.maxSafeFallSpeed)
            {
                playerControllerScript.playerHealthRemaining = playerControllerScript.playerHealthRemaining - (playerControllerScript.fallVelocityOnImpact * playerControllerScript.fallDamageMultiplier);
            }

        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            animator.SetBool("isJetpacking", false);
            animator.SetBool("isFalling", false);
            animator.SetBool("isJumping", false);
            playerControllerScript.isJumping = false;
            playerControllerScript.isGrounded = true;
            playerControllerScript.airJumpsRemaining = playerControllerScript.totalAirJumps;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            if (playerControllerScript.isJumping == false)
            {
                animator.SetBool("isJumping", false);
                animator.SetBool("isJetpacking", false);
                animator.SetBool("isFalling", true);
            }

            playerControllerScript.isGrounded = false;
        }
    }
}
