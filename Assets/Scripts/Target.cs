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
    private float outerRadius;

    [SerializeField]
    private NoteSpawner noteSpawner;

    [SerializeField]
    private bool upPressed,downPressed,rightPressed,leftPressed;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow)){upPressed = true;}
        if (Input.GetKeyUp(KeyCode.UpArrow)){upPressed = false;}

        if (Input.GetKeyDown(KeyCode.DownArrow)){downPressed = true;}
        if (Input.GetKeyUp(KeyCode.DownArrow)){downPressed = false;}

        if (Input.GetKeyDown(KeyCode.LeftArrow)) { leftPressed = true; }
        if (Input.GetKeyUp(KeyCode.LeftArrow)) { leftPressed = false; }

        if (Input.GetKeyDown(KeyCode.RightArrow)) { rightPressed = true; }
        if (Input.GetKeyUp(KeyCode.RightArrow)) { rightPressed = false; }


        if ((Input.GetKeyDown(KeyCode.UpArrow)) || (Input.GetKeyDown(KeyCode.DownArrow)) ||
            (Input.GetKeyDown(KeyCode.LeftArrow)) || (Input.GetKeyDown(KeyCode.RightArrow)))
        {
            Queue<GameObject> notesSpawned = noteSpawner.gameObject.GetComponent<NoteSpawner>().notesSpawned;
            GameObject currentNote = null;
            if (notesSpawned.Count > 0 )
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
                if (distance < innerRadius)
                {
                    Debug.Log("Perfect!");
                }
                else if (distance < outerRadius)
                {
                    Debug.Log("Nice");
                }
                else if (distance > innerRadius)
                {
                    Debug.Log("Miss");
                }

                notesSpawned.Dequeue();
                GameObject.Destroy(currentNote);
            }

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, innerRadius);
        Gizmos.DrawWireSphere(transform.position, outerRadius);
    }
}
