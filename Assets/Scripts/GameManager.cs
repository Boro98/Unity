using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.Audio;
using UnityEditor.Rendering;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    [SerializeField] Slider volumeSlider;

    public enum GameState { GAME, PAUSE_MENU, LEVEL_COMPLETED, GAME_OVER, GS_OPTIONS };
    public GameState currentGameState = GameState.GAME;

    public Canvas pauseMenuCanvas;
    public Canvas levelCompletedCanvas;
    public Canvas optionsCanvas;
    public Canvas inGameCanvas;

    public TMP_Text scoreText;
    public TMP_Text livesText;
    public TMP_Text highScoreText;
    public TMP_Text tempScoreText;

    private int score = 0;
    private int hearts = 3;
    public Image[] keysTab;
    private int keysFound = 0;
    public int highScore;
    public const string keyHighScore = "HighScoreLevel1";


    public void onClickDecreaseBtn()
    {
        QualitySettings.SetQualityLevel(0, true); 

        //QualitySettings.DecreaseLevel(true);

        //QualitySettings.names = [QualitySettings.GetQualityLevel()];
    }

    public void onClickNxtLvl()
    {
        SceneManager.LoadScene("Level2");
    }

    public void onClickIncrease()
    {
        QualitySettings.SetQualityLevel(5, true);
    }

    public void OnResumeButtonClicked()
    {
        InGame();
    }

    public void OnRestartButtonClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
    }

    public void OnOptionsClicked()
    {
        Options();
        
    }

    public void OnReturnToMainMButtonClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void OnReturnToMainMButnLvlEnd()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void OnRestartButtonLvlEnd()
    {
        SceneManager.LoadScene("Level1");

    }


    void Start()
    {
        if (!PlayerPrefs.HasKey("music"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            Load();
        }
        else
        {
            Load();
        }
    }

    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
        Save();
    }

    private void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }



    private void Update()
    {

        if (Input.GetKey(KeyCode.Escape))
        {
            if (currentGameState == GameState.GAME)
            {
                PauseGame();
                PauseMenu();
            }
            else if (currentGameState == GameState.PAUSE_MENU)
            {
                InGame();
                ResumeGame();
            }
        }

    }

    private void Awake()
    {

        InGame();

        void HasKey(string keyHighScore)
        {
            if (PlayerPrefs.HasKey(keyHighScore))
            {
                Debug.Log("The key " + keyHighScore + " exists");
            }
            else
                PlayerPrefs.SetInt(keyHighScore, 0);
        }


        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }


        foreach (var key in keysTab)
        {
            key.color = Color.black;
        }

        scoreText.text = score.ToString();
        livesText.text = hearts.ToString();
    }

    void SetGameState(GameState newGameState)
    {
        

        currentGameState = newGameState;

        /*

        if (currentGameState == GameState.GAME)
        {
            _ = inGameCanvas.enabled;
        }
        else
        {
            
            inGameCanvas.enabled = false;
        }

        */

        inGameCanvas.enabled = currentGameState == GameState.GAME;

        optionsCanvas.enabled = currentGameState == GameState.GS_OPTIONS;

        pauseMenuCanvas.enabled = currentGameState == GameState.PAUSE_MENU;

        levelCompletedCanvas.enabled = currentGameState == GameState.LEVEL_COMPLETED;

        if(newGameState == GameState.LEVEL_COMPLETED)
        {
            Scene currentScene = SceneManager.GetActiveScene();

            if(currentScene.name == "Level1")
            {
                highScore = PlayerPrefs.GetInt(keyHighScore, 0);
            }

            if(highScore < score)
            {
                highScore = score;
                PlayerPrefs.SetInt(keyHighScore, highScore);
            }


            //highScoreText.text = highScore.ToString();

            tempScoreText.SetText("YOUR SCORE = " + score);

            highScoreText.SetText("THE BEST SCORE = " + highScore);


        }


    }

    public void AddPoints(int points)
    {
        score += points;
        scoreText.text = score.ToString();
    }

    public void AddLives(int lives)
    {
        hearts += lives;
        livesText.text = hearts.ToString();
    }

    public void RevokeLives(int lives)
    {
        hearts -= lives;
        livesText.text = hearts.ToString();
    }

    public void AddKeys(Color keyColor)
    {
        keysTab[keysFound].enabled = true;
        keysTab[keysFound].color = keyColor;
        keysFound++;
    }

    private void Options()
    {
        Time.timeScale = 0f;
        SetGameState(GameState.GS_OPTIONS);
    }

    private void PauseGame()
    {
        Time.timeScale = 0f; 
        SetGameState(GameState.PAUSE_MENU);
    }

    private void ResumeGame()
    {
        Time.timeScale = 1f; 
        SetGameState(GameState.GAME);
        InGame();
    }

    public void PauseMenu()
    {
        SetGameState(GameState.PAUSE_MENU);
    }

    public void InGame()
    {
        SetGameState(GameState.GAME);
        Time.timeScale = 1f;
    }

    public void LevelCompleted()
    {
        SetGameState(GameState.LEVEL_COMPLETED);
    }

    public void GameOver()
    {
        SetGameState(GameState.GAME_OVER);
    }


}
