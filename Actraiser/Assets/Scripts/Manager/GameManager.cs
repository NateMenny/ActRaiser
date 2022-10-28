using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    public CanvasManager currentCanvas;
    public SceneName nextScene;
    public SceneName titleScene;
    public SceneName endScene;
    bool isWinConditionMet = false;
    static GameManager _instance = null;
    public static GameManager instance
    {
        get { return _instance; }
        set { _instance = value; }
    }

    int _score = 0;
    int _lives = 1;

    public int maxLives = 3;
    public GameObject playerPrefab;

    [HideInInspector] public GameObject playerInstance;
    [HideInInspector] public LevelManager currentLevel;

    public int score
    {
        get { return _score; }
        set
        {
            _score = value;
            currentCanvas.SetScoreText(_score.ToString("0000"));
            Debug.Log("Score changed to " + _score);
        }
    }

    public int lives
    {
        get { return _lives; }
        set
        {
            if (!currentCanvas)
                currentCanvas = FindObjectOfType<CanvasManager>();

            _lives = value;
            if (_lives > maxLives)
                _lives = maxLives;

            if (_lives < 0)
            {
                //gameover stuff can go here
                instance.EndGame(false);
                return;
            }

            //if execution reaches here - we need to respawn
            if (currentLevel)
            {
                Destroy(playerInstance);
                SpawnPlayer(currentLevel.spawnPoint);
            }

            if (currentCanvas)
                currentCanvas.SetLivesText(_lives.ToString("00"));

            Debug.Log("Lives changed to " + _lives);

        }
    }

    public bool IsWinConditionMet { get => isWinConditionMet; }
    // Start is called before the first frame update
    private void Awake()
    {
        Debug.Log("f");
        if (instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }
    void Start()
    {
        if (!currentCanvas)
            currentCanvas = FindObjectOfType<CanvasManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (SceneManager.GetActiveScene().name == titleScene.ToString())
            {
                QuitGame();
            }
        }
        /* This part is not necessary for game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (SceneManager.GetActiveScene().name == "MainMenu")
                SceneManager.LoadScene("SampleScene");
            else if (SceneManager.GetActiveScene().name == "SampleScene")
                SceneManager.LoadScene("EndScene");
            else if (SceneManager.GetActiveScene().name == "EndScene")
                SceneManager.LoadScene("MainMenu");
        }
        

        if (Input.GetKeyDown(KeyCode.Backspace))
            QuitGame();
        */

    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
            EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void ReturnToTitle()
    {
        SceneManager.LoadScene(titleScene.ToString());
    }
    public void EndGame(bool isComplete)
    {
        isWinConditionMet = isComplete;
        SceneManager.LoadScene(endScene.ToString());       
    }
    public void SpawnPlayer(Transform spawnLocation)
    {
        if(spawnLocation)
        playerInstance = Instantiate(playerPrefab, spawnLocation.position, spawnLocation.rotation);
    }

    public void MoveToNextScene()
    {
        SceneManager.LoadScene(nextScene.ToString());
    }
}

public enum SceneName
{
    None,
    TitleScreen,
    Level1,
    EndScene,
    SampleScene
}
