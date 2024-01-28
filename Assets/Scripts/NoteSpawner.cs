using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    public GameObject notePrefab;
    private Vector2 spawnDirection = Vector2.down;
    [SerializeField]
    private float noteSpeed;
    private Vector3 spawnPosition;

    public Queue<GameObject> notesSpawned = new Queue<GameObject>();
    public int difficulty;

    public bool spawning100bpm,spawning150bpm,spawning175bpm = false;
    private float spawnRate100bpm = 0.6f;

    private float spawnRate150bpm = 0.9f;

    private float spawnRate175bpm = 1.05f;



    public string currentBpm = "100";
    public MusicManager musicManager;

    private void Start()
    {
        spawnPosition = transform.position;
        musicManager = GameObject.Find("MusicManager").GetComponent<MusicManager>();
    }


    public void PauseSpawning()
    {
        switch (currentBpm)
        {
            case "100":
                spawning100bpm = false;
                break;
            case "150":
                spawning150bpm = false;
                break;
            case "175":
                spawning175bpm = false;
                break;
        }
    }

    public void ResumeSpawning()
    {
        switch (currentBpm)
        {
            case "100":
                startSpawning100bpm();
                break;
        }
    }


    public void clearNotes()
    {
        while(notesSpawned.Count > 0)
        {
            Destroy(notesSpawned.Dequeue());
        }
    }

    public IEnumerator spawnNote100bpm()
    {
        while (spawning100bpm)
        {
            spawnNote();
            yield return new WaitForSeconds(spawnRate100bpm*2);
        }
    }

    public void startSpawning100bpm()
    {
        spawning100bpm = true;
        StartCoroutine(spawnNote100bpm());
    }

    public IEnumerator spawnNote150bpm()
    {
        while (spawning150bpm)
        {
            spawnNote();
            yield return new WaitForSeconds(spawnRate150bpm * 2);
        }
    }

    public void startSpawning150bpm()
    {
        spawning150bpm = true;
        StartCoroutine(spawnNote150bpm());
    }

    public IEnumerator spawnNote175bpm()
    {
        while (spawning175bpm)
        {
            spawnNote();
            yield return new WaitForSeconds(spawnRate175bpm * 2);
        }
    }

    public void startSpawning175bpm()
    {
        spawning175bpm = true;
        StartCoroutine(spawnNote175bpm());
    }

    private void Update()
    {

/*        if(Input.GetKeyDown(KeyCode.W))
        {
            spawnNote();
        }*/
    }

    public void spawnNote()
    {
        GameObject newNote = GameObject.Instantiate(notePrefab, spawnPosition,transform.rotation);
        notesSpawned.Enqueue(newNote);
        newNote.GetComponent<Rigidbody2D>().velocity = spawnDirection * noteSpeed;
    }
}
