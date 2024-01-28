using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Json;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField]
    private float innerRadius;

    [SerializeField]
    public float outerRadius;

    [SerializeField]
    private NoteSpawner noteSpawner;

    [SerializeField]
    private bool upPressed,downPressed,rightPressed,leftPressed;

    [SerializeField]
    private float inputBuffer;

    [SerializeField]
    private bool isRunning = false;

    private MusicManager musicManager;
    private AudioSource soundPlayer;

    public Animator PlayerAn;

    public AudioClip correctSound, incorrectSound, niceSound;
    private Color originalColor = new Color(1, 1, 1, 0.5f);

    private void Start()
    {
        musicManager = GameObject.Find("MusicManager").GetComponent<MusicManager>();
        soundPlayer = GetComponent<AudioSource>();
        PlayerAn = GameObject.Find("Player").GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow)){upPressed = true;
            PlayerAn.SetBool("Up", true);
        }
        if (Input.GetKeyUp(KeyCode.UpArrow)){ StartCoroutine(InputReleaseBuffer("up"));
          
        }

        if (Input.GetKeyDown(KeyCode.DownArrow)){downPressed = true;
            PlayerAn.SetBool("Down", true);
        }
        if (Input.GetKeyUp(KeyCode.DownArrow)){ StartCoroutine(InputReleaseBuffer("down"));
          
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow)) { leftPressed = true;
            PlayerAn.SetBool("Left", true);
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow)) { StartCoroutine(InputReleaseBuffer("left"));
           
        }

        if (Input.GetKeyDown(KeyCode.RightArrow)) { rightPressed = true;
            PlayerAn.SetBool("Right", true);
        }
        if (Input.GetKeyUp(KeyCode.RightArrow)) { StartCoroutine(InputReleaseBuffer("right"));
           
        }


        if ((Input.GetKeyDown(KeyCode.UpArrow)) || (Input.GetKeyDown(KeyCode.DownArrow)) ||
            (Input.GetKeyDown(KeyCode.LeftArrow)) || (Input.GetKeyDown(KeyCode.RightArrow)))
        {

            if (!isRunning)
            {
                StartCoroutine(InputBuffer());
            }
        }
    }

    private IEnumerator InputBuffer()
    {
       isRunning = true;
       yield return new WaitForSeconds(inputBuffer);
       HandleArrowInput(); 
        
    }
    private void HandleArrowInput()
    {
        Queue<GameObject> notesSpawned = noteSpawner.gameObject.GetComponent<NoteSpawner>().notesSpawned;
        GameObject currentNote = null;
        if (notesSpawned.Count > 0)
        {
            currentNote = notesSpawned.Peek();
        }
        if (currentNote != null)
        {
            float currentNoteX = currentNote.GetComponent<Transform>().position.x;
            float currentNoteY = currentNote.GetComponent<Transform>().position.y;
            float targetX = transform.position.x;
            float targetY = transform.position.y;

            float distance = Mathf.Sqrt((currentNoteX - targetX) * (currentNoteX - targetX) + (currentNoteY - targetY) * (currentNoteY - targetY));

            Note currentNoteScript = currentNote.GetComponent<Note>();
            if (currentNoteScript.Up == upPressed &&
                currentNoteScript.Down == downPressed &&
                currentNoteScript.Right == rightPressed &&
                currentNoteScript.Left == leftPressed)
            {
                if (distance < innerRadius)
                {
                    setGreen();
                    Debug.Log("Perfect!");
                    musicManager.gainHappy();

                }
                else if (distance < outerRadius)
                {
                    setYellow();
                    Debug.Log("Nice");
                    musicManager.gainHappyPartial();
                }
                else
                {
                    setRed();
                    Debug.Log("Miss");
                    musicManager.loseHappy();
                }
            }
            else
            {
                setRed();
                Debug.Log("Incorrect");
                musicManager.loseHappy();
            }


            notesSpawned.Dequeue();
            GameObject.Destroy(currentNote);
        }
        isRunning = false;
    }

    public void setGreen()
    {
        GetComponent<SpriteRenderer>().color = new Color(0, 1, 0, 1);
        soundPlayer.PlayOneShot(correctSound,0.25f);
        StartCoroutine(restoreColor());
    }

    public void setYellow()
    {
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 0, 1);
        soundPlayer.PlayOneShot(niceSound);
        StartCoroutine(restoreColor());
    }

    public void setRed()
    {
        GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
        soundPlayer.PlayOneShot(incorrectSound);
        StartCoroutine(restoreColor());
    }

    public IEnumerator restoreColor()
    {
        yield return new WaitForSeconds(.5f);
        GetComponent<SpriteRenderer>().color = originalColor;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, innerRadius);
        Gizmos.DrawWireSphere(transform.position, outerRadius);
    }


    IEnumerator InputReleaseBuffer(string arrowKey)
    {
        yield return new WaitForSeconds(inputBuffer);
        switch (arrowKey)
        {
            case "up":
                upPressed = false;
                PlayerAn.SetBool("Up", false);
                break;
            case "down":
                downPressed = false;
                PlayerAn.SetBool("Down", false);
                break;
            case "right":
                rightPressed = false;
                PlayerAn.SetBool("Right", false);
                break;
            case "left":
                leftPressed = false;
                PlayerAn.SetBool("Left", false);
                break;
        }
    }

    public void StopAll()
    {
        StopAllCoroutines();
    }
}
