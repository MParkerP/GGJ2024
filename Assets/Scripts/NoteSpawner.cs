using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    public GameObject notePrefab;
    private Vector2 spawnDirection = Vector2.down;
    [SerializeField]
    private float noteSpeed;
    private Vector3 spawnPosition;

    public Queue<GameObject> notesSpawned = new Queue<GameObject>();
    private void Start()
    {
        spawnPosition = transform.position;
    }

    private void Update()
    {

        if(Input.GetKeyDown(KeyCode.W))
        {
            spawnNote();
        }
    }

    public void spawnNote()
    {
        GameObject newNote = GameObject.Instantiate(notePrefab, spawnPosition,transform.rotation);
        notesSpawned.Enqueue(newNote);
        newNote.GetComponent<Rigidbody2D>().velocity = spawnDirection * noteSpeed;
    }
}
