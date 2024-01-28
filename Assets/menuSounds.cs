using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuSounds : MonoBehaviour
{
    public AudioClip buttonClick;

    public void PlayClick()
    {
        GetComponent<AudioSource>().PlayOneShot(buttonClick);
    }
}
