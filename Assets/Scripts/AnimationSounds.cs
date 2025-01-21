using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSounds : MonoBehaviour
{
    public AudioClip kingLaugh, gunShot, bloodSplat, gunEquip, knightDeath, shing, swordPutAway, lever, scrape, crush, bang, laser, bell, fire, kiss, blow, echo, fall;

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

    public IEnumerator PlayKnightDeath()
    {
        yield return new WaitForSeconds(2.4f);
        GetComponent<AudioSource>().PlayOneShot(shing);
        yield return new WaitForSeconds(1f);
        GetComponent<AudioSource>().PlayOneShot(knightDeath);
        yield return new WaitForSeconds(0.45f);
        GetComponent<AudioSource>().PlayOneShot(swordPutAway);
      
        
    }

    public IEnumerator PlayExecutionerDeath()
    {
        GetComponent<AudioSource>().PlayOneShot(scrape);
        yield return new WaitForSeconds(0.5f);
        GetComponent<AudioSource>().PlayOneShot(lever);
        yield return new WaitForSeconds(0.75f);
        GetComponent<AudioSource>().PlayOneShot(crush);
        GetComponent<AudioSource>().PlayOneShot(bang);


    }

    public IEnumerator PlayPriestDeath()
    {
        yield return new WaitForSeconds(0.5f);
        GetComponent<AudioSource>().PlayOneShot(bell);
        yield return new WaitForSeconds(1.2f);
        GetComponent<AudioSource>().PlayOneShot(laser);
        GetComponent<AudioSource>().PlayOneShot(fire);
        

    }

    public IEnumerator PlayPrincessDeath()
    {
        yield return new WaitForSeconds(0.5f);
        GetComponent<AudioSource>().PlayOneShot(kiss);
        yield return new WaitForSeconds(0.3f);
        GetComponent<AudioSource>().PlayOneShot(blow);
        yield return new WaitForSeconds(1.5f);
        GetComponent<AudioSource>().PlayOneShot(kiss);
        yield return new WaitForSeconds(0.5f);
        GetComponent<AudioSource>().PlayOneShot(echo);
        yield return new WaitForSeconds(1.8f);
        GetComponent<AudioSource>().PlayOneShot(fall);


    }
}
