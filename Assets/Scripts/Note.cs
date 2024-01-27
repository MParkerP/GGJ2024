using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;

public class Note : MonoBehaviour
{
    Random rand = new Random();
    private string direction = "";

    private void Start()
    {
        int num = rand.Next(0,4);
        switch (num)
        {
            case 0:
                direction = "up";
                break;

            case 1:
                direction = "right";
                break;

            case 2:
                direction = "left";
                break;

            case 3:
                direction = "down";
                break;
        }
    }
    private void Update()
    {
        if (transform.position.y <= -5)
        {
            GameObject.Destroy(gameObject);
            GameObject.Find("NoteSpawner").GetComponent<NoteSpawner>().notesSpawned.Dequeue();
        }
    }

    public string getDirection()
    {
        return direction;
    }
}
