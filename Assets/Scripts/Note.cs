using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Drawing.Inspector.PropertyDrawers;
using UnityEngine;
using Random = System.Random;

public class Note : MonoBehaviour
{
    Random rand = new Random();
    public bool Up, Down, Left, Right = false;

    public SpriteRenderer UpSprite, DownSprite, LeftSprite, RightSprite;
    private Color visible;
    private GameObject spawner;

    private GameObject target;
    private Vector2 targetPosition;
    private float targetOuterRadius;

    public AudioClip correctSound, niceSound, incorrectSound;
    private AudioSource soundPlayer;

    private void Start()
    {
        visible = RightSprite.color;
        visible.a = 255f;

        spawner = GameObject.Find("NoteSpawner");
        SetDirections(spawner.GetComponent<NoteSpawner>().difficulty);

        target = GameObject.Find("Target");
        targetPosition = target.GetComponent<Transform>().position;
        targetOuterRadius = target.GetComponent<Target>().outerRadius;

        soundPlayer = GetComponent<AudioSource>();

    }

    private void Update()
    {
        if (transform.position.y <= (targetPosition.y - targetOuterRadius))
        {
            GameObject.Destroy(gameObject);
            spawner.GetComponent<NoteSpawner>().notesSpawned.Dequeue();
            Debug.Log("Miss");
        }
    }


    private void SetDirections(int difficulty)
    {
        

        for (int i = 0; i < difficulty; i++)
        {
            int num = rand.Next(0, 4);
            switch (num)
            {
                case 0:
                    Up = true;
                    UpSprite.color = visible;
                    break;

                case 1:
                    Down = true;
                    DownSprite.color = visible;
                    break;

                case 2:
                    Left = true;
                    LeftSprite.color = visible;
                    break;

                case 3:
                    Right = true;
                    RightSprite.color = visible;
                    break;
            }
        }
    }
}
