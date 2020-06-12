using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    public static AudioClip playerJumpSound, airJumpSound, dashSound, runSound, jetpackSound;
    static AudioSource audioSource;

    public AudioClip jump;
    public AudioClip airJump;
    public AudioClip dash;
    public AudioClip run;
    public AudioClip jetpack;

    private void Start()
    {
        playerJumpSound = jump;
        airJumpSound = airJump;
        dashSound = dash;
        runSound = run;
        jetpackSound = jetpack;

        audioSource = GetComponent<AudioSource>();
    }

    public static void PlaySound (string clip)
    {
        switch (clip)
        {
            case "jump":
                audioSource.PlayOneShot(playerJumpSound);
                break;
            case "airJump":
                audioSource.PlayOneShot(airJumpSound);
                break;
            case "dash":
                audioSource.PlayOneShot(dashSound);
                break;
            case "run":
                audioSource.PlayOneShot(runSound);
                break;
            case "jetpack":
                audioSource.PlayOneShot(jetpackSound);
                break;
        }
    }
}
