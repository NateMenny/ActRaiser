using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public Image[] healthSegments;

    [Header("Buttons")]
    public Button nextSceneButton;
    public Button quitButton;
    public Button settingsButton;
    public Button backButton;
    public Button returnToMenuButton;
    public Button returnToGameButton;

    [Header("Menus")]
    public GameObject mainMenu;
    public GameObject pauseMenu;
    public GameObject settingsMenu;

    [Header("Text")]
    public Text livesText;
    public Text scoreText;
    public Text volSliderText;

    [Header("Slider")]
    public Slider volSlider;

    // Start is called before the first frame update
    void Start()
    {
        if (nextSceneButton)
            nextSceneButton.onClick.AddListener(() => GameManager.instance.MoveToNextScene());

        if (quitButton)
            quitButton.onClick.AddListener(() => GameManager.instance.QuitGame());

        if (settingsButton)
            settingsButton.onClick.AddListener(() => ShowSettingsMenu());

        if (backButton)
            backButton.onClick.AddListener(() => ShowMainMenu());

        if (returnToGameButton)
            returnToGameButton.onClick.AddListener(() => ReturnToGame());

        if (returnToMenuButton)
            returnToMenuButton.onClick.AddListener(() => GameManager.instance.ReturnToTitle());
    }

    public void SetLivesText(string livesValue)
    {
        if(livesText)
        livesText.text = livesValue;
    }

    public void SetScoreText(string scoreValue)
    {
        if(scoreText)
        {
            scoreText.text = scoreValue;
        }
    }

    void ReturnToGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    void ShowMainMenu()
    {
        settingsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    void ShowSettingsMenu()
    {
        settingsMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void UpdateHealthbar(int health)
    {
        for (int i = 0; i < healthSegments.Length; i++)
        {
            healthSegments[i].color = Color.red;
        }
        for (int i = 0; i < health; i++)
        {
            healthSegments[i].color = Color.blue;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (pauseMenu)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                pauseMenu.SetActive(!pauseMenu.activeSelf);

                //HUGE HINT FOR THE LAB.
                if (pauseMenu.activeSelf)
                {
                    //turn off something to pause the game
                    Time.timeScale = 0;
                    
                }
                else
                {
                    //resume game
                    Time.timeScale = 1;
                }
            }
        }

        if (settingsMenu)
        {
            if (settingsMenu.activeSelf)
            {
                if(volSlider && volSliderText)
                volSliderText.text = volSlider.value.ToString();
            }
        }
    }
}
