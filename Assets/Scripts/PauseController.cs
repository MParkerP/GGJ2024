using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    [SerializeField] private Canvas pauseScreen;
    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void UnpauseGame()
    {
        Time.timeScale = 1;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            pauseScreen.gameObject.SetActive(!pauseScreen.gameObject.activeSelf);
            if (Time.timeScale == 0)
            {
                GameObject.Find("MusicManager").GetComponent<AudioSource>().UnPause();
                Time.timeScale = 1;
                
            }
            else
            {
                GameObject.Find("MusicManager").GetComponent<AudioSource>().Pause();
                Time.timeScale = 0;
            }
        }
    }
}
