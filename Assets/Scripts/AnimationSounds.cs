using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSounds : MonoBehaviour
{
    public AudioClip kingLaugh, gunShot, bloodSplat, gunEquip;

    public void PlayKingLaugh()
    {
        GetComponent<AudioSource>().PlayOneShot(kingLaugh);
    }

    public void PlayGunShot()
    {
        GetComponent<AudioSource>().PlayOneShot(gunShot);
    }

    public void PlaybloodSplat()
    {
        GetComponent<AudioSource>().PlayOneShot(bloodSplat);
    }

    public void PlayGunEquip()
    {
        GetComponent<AudioSource>().PlayOneShot(gunEquip);
    }
}
