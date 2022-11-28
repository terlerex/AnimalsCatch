using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Game Mode")]
    private static readonly string[] GameModes = new string[] { "Classic Mode", "Survival Mode ", "Insane Mode" };
    public TextMeshProUGUI gameModeText;
    public static bool BInsaneMode = false;
    public static bool BClassicMode = false;
    public static bool BSurvivalMode = false;
    
    [Header("Timer")] 
    private static float TimeRemaining = 60;
    private static bool TimerIsRunning = false;
    public TextMeshPro timerText;
    
    [Header("Game Over")]
    public TextMeshProUGUI gameOverText;
    private static Canvas _gameOverCanvas;
    public static bool _gameOvers = false;
    private Canvas EscapeMenu;
    
    [Header("Game is Running ?")]
    public static bool _gameIsRunning = false;

    //Show FPS
    public float deltaTime;
    public TextMeshProUGUI fpsText;
    
    private void Start()
    {   
        InvokeRepeating(nameof(DeleteAllAreaSpawner), 1.0f, 20f);
        
        //Load PlayerPref
        ScoreManager.Instance.LoadScore();
        
        QualitySettings.SetQualityLevel(0);

        _gameOverCanvas = GameObject.Find("GameOverCanvas").GetComponent<Canvas>();
        EscapeMenu = GameObject.Find("EscapeCanvas").GetComponent<Canvas>();
        
        _gameOverCanvas.enabled = false;
        TimerIsRunning = true;

        gameModeText.text = GameModes[0];
        
        //Time scale is set to 0 so that the game doesn't start until the player presses the start button
        Time.timeScale = 0;
        
    }
    
    public void Update()
    {
        ShowFPS();

        //Check if the game is running
        if (!_gameIsRunning)
        {
            gameModeText.enabled = false;
            timerText.enabled = false;
            ScoreManager.Instance.txtScore.enabled = false;
        }

        //Timer for the game
        timerText.text = "Time left : \n" + TimeRemaining.ToString("0");

        //Check if the timer is not running
        if (!TimerIsRunning || !_gameIsRunning) return;
        
        //Decrease the timer
        if (TimeRemaining > 0)
        {
            TimeRemaining -= Time.deltaTime;
        }
        //If the timer is over "Game over"
        else
        {
            GameOver();
        }
    }

    public void ShowFPS()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        fpsText.text = Mathf.Ceil(fps).ToString() + " FPS";
    }

    /// <summary>
    /// This method is called when the player click on "Evil animals" for insane mode or the time is over
    /// </summary>
    public static void GameOver()
    {
        GameObject.Find("GamesThemes").GetComponent<AudioSource>().Stop();
        
        //Play the game over sound
        AudioManager.GameOverSound();
        
        //Bool to know if the game is over
        _gameOvers = true;
        
        //Stop the timer
        TimeRemaining = 0;
        
        //Stop the spawning of animals and timer with bool
        TimerIsRunning = false;
        _gameIsRunning = false;
        
        //Delete de possibility to click on the animals
        DestroyAnimals.Clickable = false;
        
        //Save the score
        ScoreManager.Instance.AllScore();
        ScoreManager.Instance.HighScore();
        
        //Display the game over canvas
        _gameOverCanvas.enabled = true;
        
        //If mode is active desactive it
        while (BClassicMode || BSurvivalMode || BInsaneMode)
        {
            BClassicMode = false;
            BSurvivalMode = false;
            BInsaneMode = false;
        }
        
        //Current animal pass to 0
        ItemAreaSpawner.currentAnimal = 0;

    }
    
    
    /// <summary>
    /// This method is called when the player click on "Evil animals" for insane mode or the time is over
    /// </summary>
    public void ExitMode()
    {
        GameObject.Find("GamesThemes").GetComponent<AudioSource>().Stop();
        
        //Play the win sound
        AudioManager.WinSound();
        
        //Bool to know if the game is over
        _gameOvers = true;
        
        //Stop the timer
        TimeRemaining = 0;
        
        //Stop the spawning of animals and timer with bool
        TimerIsRunning = false;
        _gameIsRunning = false;
        
        //Delete de possibility to click on the animals
        DestroyAnimals.Clickable = false;
        
        //Save the score
        ScoreManager.Instance.AllScore();
        ScoreManager.Instance.HighScore();
        
        //Display the game over canvas
        _gameOverCanvas.enabled = true;
        
        //If mode is active desactive it
        while (BClassicMode || BSurvivalMode || BInsaneMode)
        {
            BClassicMode = false;
            BSurvivalMode = false;
            BInsaneMode = false;
        }
        //Current animal pass to 0
        ItemAreaSpawner.currentAnimal = 0;

        //Exit to the escape menu
        ToggleMenu.isMenuOpen = false;
        EscapeMenu.enabled = false;
    }

    /// <summary>
    /// Quit the game
    /// </summary>
    public void ExitGames()
    {
        ScoreManager.Instance.SaveScore();
        Application.Quit();
    }
    
    /// <summary>
    /// This method is called for delete all animals in the scene when the game is over
    /// </summary>
    public void DeleteAllAnimals()
    {
        var tagsToDestroy = new[] {"EvilAnimal", "Animal"};

        foreach (var tag in tagsToDestroy)
        {
            GameObject[] allObjects = GameObject.FindGameObjectsWithTag(tag);
            foreach (GameObject go in allObjects)
            {
                Destroy(go);
            }
        }
    }
    
    /// <summary>
    /// This method is called every 20 second for clear not used Area spawner
    /// </summary>
    private void DeleteAllAreaSpawner()
    {
        var allObjects = GameObject.FindGameObjectsWithTag("RaycastNoOverlap");
        foreach (var go in allObjects)
        {
            Destroy(go);
        }
    }
    
    /// <summary>
    /// Classic Mode Options
    /// </summary>
    public void ClassicMode()
    {
        GameObject.Find("MainThemes").GetComponent<AudioSource>().Stop();
        AudioManager.GameTheme();
        
        //Change color and text of game over text according to the game mode
        gameOverText.color = Color.green;
        gameOverText.text = "Finished !";

        //Change all bool for the game mode
        BClassicMode = true;
        _gameIsRunning = true;
        gameModeText.enabled = true;
        timerText.enabled = true;
        ScoreManager.Instance.txtScore.enabled = true;
        DestroyAnimals.Clickable = true;
        TimerIsRunning = true;
        _gameOvers = false;
        
        //Change the game mode text
        gameModeText.text = GameModes[0];
        
        //Change the time to infinite
        TimeRemaining = Mathf.Infinity;

        //Reinitialize the score
        ScoreManager.Instance.score = 0;
        
        //Time to 1 
        Time.timeScale = 1;
    }

    /// <summary>
    /// Survival Mode Options
    /// </summary>
    public void SurvivalMode()
    {
        GameObject.Find("MainThemes").GetComponent<AudioSource>().Stop();
        AudioManager.GameTheme();
        
        //Change color and text of game over text according to the game mode
        gameOverText.color = Color.green;
        gameOverText.text = "Time Up !";
        
        //Change all bool for the game mode
        BSurvivalMode = true;
        _gameIsRunning = true;
        gameModeText.enabled = true;
        timerText.enabled = true;
        ScoreManager.Instance.txtScore.enabled = true;
        DestroyAnimals.Clickable = true;
        TimerIsRunning = true;
        _gameOvers = false;
        
        //Change the game mode text
        gameModeText.text = GameModes[1];
        
        //Change the time to 90
        TimeRemaining = 90;
        
        //Reinitialize the score
        ScoreManager.Instance.score = 0;
        
        //Time to 1 
        Time.timeScale = 1;
    }
    
    /// <summary>
    /// Insane Mode Options
    /// </summary>
    public void InsaneMode()
    {
        GameObject.Find("MainThemes").GetComponent<AudioSource>().Stop();
        AudioManager.GameTheme();
        
        //Change color and text of game over text according to the game mode
        gameOverText.color = Color.red;
        gameOverText.text = "Game Over";
        
        //Change all bool for the game mode
        BInsaneMode = true;
        _gameIsRunning = true;
        gameModeText.enabled = true;
        timerText.enabled = true;
        ScoreManager.Instance.txtScore.enabled = true;
        DestroyAnimals.Clickable = true;
        TimerIsRunning = true;
        _gameOvers = false;
        
        //Change the game mode text
        gameModeText.text = GameModes[2];
        
        //Change the time to 60
        TimeRemaining = 60;
        
        //Reinitialize the score
        ScoreManager.Instance.score = 0;
        
        //Time to 1
        Time.timeScale = 1;
    }
}
