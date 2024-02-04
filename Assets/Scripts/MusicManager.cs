using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = System.Random;

public class MusicManager : MonoBehaviour
{
    [SerializeField]
    private AudioClip easy1, easy2, easy3, medium1, medium2, medium3, hard1, hard2, hard3;

    public int currentLevel = 1;

    private AudioSource musicSource;

    [SerializeField]
    private float happinessTracker, happinessThreshhold, happyLoss, happyGain;

    public float startDelay;
    public float levelUpDelay;

    private Target target;
    private NoteSpawner noteSpawner;



    public Animator SceneAn;
    public Animator PlayerAn;

    private bool gameOver = false;
    private AnimationSounds animationSounds;

    private void Start()
    {
        musicSource = GetComponent<AudioSource>();
        animationSounds = GameObject.Find("SceneAnimation").GetComponent<AnimationSounds>();
        target = GameObject.Find("Target").GetComponent<Target>();
        noteSpawner = GameObject.Find("NoteSpawner").GetComponent<NoteSpawner>();
        SceneAn = GameObject.Find("SceneAnimation").GetComponent<Animator>();
        PlayerAn = GameObject.Find("Player").GetComponent<Animator>();
        StartCoroutine(startGameCR());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            endGame();
        }
        if (happinessTracker < 0 && !gameOver)
        {
            Random rand = new Random();
            int deathAnim = rand.Next(0, 4);
            switch (deathAnim)
            {
                case 0:
                    StartCoroutine(loseGame("PriestKill"));
                    break;
                case 1:
                    StartCoroutine(loseGame("KnightKill"));
                    break;
                case 2:
                    StartCoroutine(loseGame("PrincessKill"));
                    break;
                case 3:
                    StartCoroutine(loseGame("ExecutionerKill"));
                    break;

            }
        }
        if (happinessTracker >= happinessThreshhold)
        {
            target.StopAll();
            noteSpawner.clearNotes();
            happinessTracker = 50f;
            currentLevel++;
            switch (currentLevel)
            {
                case 1:
                    StartCoroutine(levelUp(easy2));
                    break;
                case 2:
                    StartCoroutine(levelUp(easy3));
                    break;
                case 3:
                    noteSpawner.noteSpeed = .27f;
                    noteSpawner.difficulty = 2;
                    noteSpawner.currentBpm = "150";
                    StartCoroutine(levelUp(medium1));
                    break;
                case 4:
                    StartCoroutine(levelUp(medium2));
                    noteSpawner.noteSpeed = .34f;
                    break;
                case 5:
                    StartCoroutine(levelUp(medium3));
                    break;
                case 6:
                    noteSpawner.difficulty = 3;
                    noteSpawner.currentBpm = "175";
                    noteSpawner.noteSpeed = .22f;
                    target.inputBuffer = 0.1f;

                    StartCoroutine(levelUp(hard1));
                    break;
                case 7:
                    StartCoroutine(levelUp(hard2));
                    noteSpawner.noteSpeed = .20f;
                    break;
                case 8:
                    StartCoroutine(levelUp(hard3));
                    noteSpawner.noteSpeed = .22f;
                    break;
                case 9:
                    endGame();
                    break;
            }
        }
    }

    public void gainHappy()
    {
        happinessTracker += happyGain;
    }

    public void gainHappyPartial()
    {
        happinessTracker += happyGain * 0.5f;
    }

    public void loseHappy()
    {
        happinessTracker -= happyLoss;
    }

    public void startGame()
    {
        PlayerAn.SetBool("CanMove", true);
        musicSource.volume = 3f;
        musicSource.clip = easy1;
        musicSource.Play();
        SceneAn.SetTrigger("HeadBob");
        noteSpawner.GetComponent<NoteSpawner>().startSpawning100bpm();
    }

    private IEnumerator startGameCR()
    {
        yield return new WaitForSeconds(startDelay);
        startGame();
    }

    private IEnumerator levelUp(AudioClip nextClip)
    {
        musicSource.Stop();
        musicSource.clip = nextClip;
        noteSpawner.PauseSpawning();
        SceneAn.SetTrigger("HandTap");
        yield return new WaitForSeconds(levelUpDelay);
        SceneAn.SetTrigger("HeadBob");
        noteSpawner.ResumeSpawning();
        musicSource.Play();
    }

    private void endGame()
    {
        StartCoroutine(endGameCR());
    }

    private IEnumerator endGameCR()
    {
        target.StopAll();
        GameObject.Find("Target").GetComponent<SpriteRenderer>().enabled = false;
        noteSpawner.clearNotes();
        musicSource.Stop();
        noteSpawner.PauseSpawning();
        SceneAn.SetTrigger("Win");
        PlayerAn.SetTrigger("Win");
        StartCoroutine(endGameSounds());
        PlayerAn.SetBool("Up", false);
        PlayerAn.SetBool("Down", false);
        PlayerAn.SetBool("Right", false);
        PlayerAn.SetBool("Left", false);
        PlayerAn.SetBool("CanMove", false);
        //death animation
        gameOver = true;
        yield return new WaitForSeconds(13);
        SceneManager.LoadScene("Title");
    }

    private IEnumerator endGameSounds()
    {
        yield return new WaitForSeconds(2);
        animationSounds.PlayKingLaugh();
        yield return new WaitForSeconds(5.5f);
        animationSounds.PlayGunEquip();
        yield return new WaitForSeconds(3);
        animationSounds.PlayGunShot();
        yield return new WaitForSeconds(.25f);
        animationSounds.PlaybloodSplat();
    }

    private IEnumerator playerDeath(string deathAnimation)
    {
        float playerDeathTimer = 0f;
        switch (deathAnimation)
        {
            case "PriestKill":
                playerDeathTimer = 3f;
                SceneAn.SetBool(deathAnimation, true);
                yield return new WaitForSeconds(2.4f);
                PlayerAn.SetBool("Gone", true);
                break;
            case "KnightKill":
                playerDeathTimer = 3f;
                SceneAn.SetBool(deathAnimation, true);
                PlayerAn.SetBool("KnightDeath", true);
                break;
            case "PrincessKill":
                playerDeathTimer = 3f;
                SceneAn.SetBool(deathAnimation, true);
                PlayerAn.SetBool("PrincessDeath", true);
                break;
            case "ExecutionerKill":
                playerDeathTimer = 3f;
                yield return new WaitForSeconds(2);
                SceneAn.SetBool(deathAnimation, true);
                yield return new WaitForSeconds(1.2f);
                PlayerAn.SetBool("Gone", true);
                break;

        }
        yield return new WaitForSeconds(playerDeathTimer);
        //GameObject.Find("Player").GetComponent<SpriteRenderer>().enabled = false;
    }


    private IEnumerator loseGame(string deathAnimation)
    {
        target.StopAll();
        noteSpawner.clearNotes();
        musicSource.Stop();
        noteSpawner.PauseSpawning();
        PlayerAn.SetTrigger("Stop");
        PlayerAn.SetBool("Up", false);
        PlayerAn.SetBool("Down", false);
        PlayerAn.SetBool("Right", false);
        PlayerAn.SetBool("Left", false);
        PlayerAn.SetBool("CanMove", false);
        SceneAn.SetTrigger("KingAngry");
        StartCoroutine(playerDeath(deathAnimation));
       
        
        //death animation
        gameOver = true;
        yield return new WaitForSeconds(6);
        SceneManager.LoadScene("Title");
    }
}
