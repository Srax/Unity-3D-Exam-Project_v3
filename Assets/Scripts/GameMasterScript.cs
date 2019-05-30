using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMasterScript : MonoBehaviour
{
    public bool gameIsPaused = false;
    public GameObject player;
    public GameObject enemies;

    [Header("Canvases")]
    public GameObject deathCanvas;
    public GameObject pauseCanvas;
    public GameObject mainCanvas;

    [Header("Active Quest")]
    public Quest quest;

    // Start is called before the first frame update
    void Start()
    {
        deathCanvas.SetActive(false);
        pauseCanvas.SetActive(false);
        mainCanvas.SetActive(true);
        Time.timeScale = 1f;
        PlayerPrefs.SetInt("questIndex", 0);
    }

    // Update is called once per frame
    void Update()
    {
        //If the player is alive, do this
        if (player.GetComponent<CharacterStats>().isDead == false)
        {
            TogglePauseMenu();

            if (quest.isActive)
            {
                if (quest.goal.isReached())
                {
                    player.GetComponent<CharacterStats>().AddExp(quest.expReward);
                    quest.Complete();
                }
            }
        }

        //Else, if the player is dead, do this
        else {
            PlayerIsDead();
        }

        void PlayerIsDead()
        {
            deathCanvas.SetActive(true);
            pauseCanvas.SetActive(false);
            Time.timeScale = 0.1f; //Slow time to 1/10 of normal time.
        }

        void TogglePauseMenu()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                gameIsPaused = gameIsPaused ? false : true;
                if (gameIsPaused == true)
                {
                    pauseCanvas.SetActive(true);
                    Time.timeScale = 0.1f; //Slow time to 1/10 of normal time.
                }
                else
                {
                    pauseCanvas.SetActive(false);
                    Time.timeScale = 1f; //Set time back to normal (1)
                }
            }
        }
    }
}
