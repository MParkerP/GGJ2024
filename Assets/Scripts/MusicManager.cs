using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;

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

    private void Start()
    {
        musicSource = GetComponent<AudioSource>();
        target = GameObject.Find("Target").GetComponent<Target>();
        noteSpawner = GameObject.Find("NoteSpawner").GetComponent<NoteSpawner>();
        SceneAn = GameObject.Find("SceneAnimation").GetComponent<Animator>();
        PlayerAn = GameObject.Find("Player").GetComponent<Animator>();
        StartCoroutine(startGameCR());
    }

    private void Update()
    {
        if (happinessTracker < 0 && !gameOver)
        {
            loseGame();
        }
        if (happinessTracker >= happinessThreshhold)
        {
            target.StopAll();
            noteSpawner.clearNotes();
            happinessTracker = 0.0f;
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
                    noteSpawner.difficulty = 2;
                    noteSpawner.currentBpm = "150";
                    StartCoroutine(levelUp(medium1));
                    break;
                case 4:
                    StartCoroutine(levelUp(medium2));
                    break;
                case 5:
                    StartCoroutine(levelUp(medium3));
                    break;
                case 6:
                    noteSpawner.difficulty = 3;
                    noteSpawner.currentBpm = "175";
                    StartCoroutine(levelUp(hard1));
                    break;
                case 7:
                    StartCoroutine(levelUp(hard2));
                    break;
                case 8:
                    StartCoroutine(levelUp(hard3));
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
        musicSource.Stop();
    }

    private void loseGame()
    {
        target.StopAll();
        noteSpawner.clearNotes();
        musicSource.Stop();
        noteSpawner.PauseSpawning();
        SceneAn.SetTrigger("KingAngry");
        SceneAn.SetBool("KnightKill", true);
        PlayerAn.SetTrigger("Stop");
        PlayerAn.SetBool("KnightDeath",true);
        //death animation
        gameOver = true;
    }
}
