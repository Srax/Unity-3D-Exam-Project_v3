using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameMasterScript : MonoBehaviour
{
    public bool gameIsPaused = false;
    public GameObject player;

    [Header("Canvases")]
    public GameObject deathCanvas;
    public GameObject pauseCanvas;
    public GameObject mainCanvas;
    public TextMeshProUGUI levelText;


    // Start is called before the first frame update
    void Awake()
    {
        deathCanvas.SetActive(false);
        pauseCanvas.SetActive(false);
        mainCanvas.SetActive(true);
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        levelText.SetText(player.GetComponent<CharacterStats>().level.ToString());
        if(player.GetComponent<CharacterStats>().isDead == false)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                gameIsPaused = gameIsPaused ? false : true;
                if (gameIsPaused == true)
                {
                    pauseCanvas.SetActive(true);
                    Time.timeScale = 0.1f;
                }
                else
                {
                    pauseCanvas.SetActive(false);
                    Time.timeScale = 1f;
                }
            }
        } else {
            PlayerIsDead();
        }

        void PlayerIsDead()
        {
            deathCanvas.SetActive(true);
            pauseCanvas.SetActive(false);
            Time.timeScale = 0.1f;
        }
    }
}
