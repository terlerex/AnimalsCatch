using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class ScoreManager : MonoBehaviour
{
    [Header("Score Main")] 
    public int score;
    public TextMeshPro txtScore;
    public TextMeshProUGUI txtGameOverScoreDisplayScore;
    public TextMeshProUGUI txtCurrentScore;
    public int currentScore;
    
    [Header("Score Classic Mode")]
    public int highScoreClassic;
    public TextMeshProUGUI txtHighScoreClassic;

    [Header("Score Survival Mode")]
    public int highScoreSurvival;
    public TextMeshProUGUI txtHighScoreSurvival;

    [Header("Score Instant Mode")]
    public int highScoreInsane;
    public TextMeshProUGUI txtHighScoreInsane;

    //Singleton
    [NonSerialized] public static ScoreManager Instance;
    
    private void Awake() {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Update()
    {
        //Set all score text and UI for all modes
        txtCurrentScore.text = "Statistics : \n " + currentScore;
        txtGameOverScoreDisplayScore.text = "Your Score : \n" + score;
        txtScore.text = "Score: " + score;
        
        //Set all score text and UI for Classic Mode
        txtHighScoreClassic.text = "High Score : \n " + highScoreClassic;

        //Set all score text and UI for Survival Mode
        txtHighScoreSurvival.text = "High Score : \n " + highScoreSurvival;
        
        //Set all score text and UI for Insane Mode
        txtHighScoreInsane.text = "High Score : \n " + highScoreInsane;

        
    }
    
    /// <summary>
    /// Set the High Score
    /// </summary>
    public void HighScore()
    {
        if (score > highScoreClassic && GameManager.BClassicMode)
        {
            highScoreClassic = score;
        }
        else if(score > highScoreSurvival && GameManager.BSurvivalMode)
        {
            highScoreSurvival = score;
        }
        else if(score > highScoreInsane && GameManager.BInsaneMode)
        {
            highScoreInsane = score;
        }
    }
    
    /// <summary>
    ///  Add Score to the current score
    /// </summary>
    public void AllScore()
    {
        currentScore += score;
    }
    
    public void SaveScore()
    {
        PlayerPrefs.SetInt("HighScoreClassic", highScoreClassic);
        PlayerPrefs.SetInt("HighScoreSurvival", highScoreSurvival);
        PlayerPrefs.SetInt("HighScoreInsane", highScoreInsane);
        PlayerPrefs.SetInt("CurrentScore", currentScore);
        PlayerPrefs.Save();
    }
    
    public void LoadScore()
    {
        highScoreClassic = PlayerPrefs.GetInt("HighScoreClassic");
        highScoreSurvival = PlayerPrefs.GetInt("HighScoreSurvival");
        highScoreInsane = PlayerPrefs.GetInt("HighScoreInsane");
        currentScore = PlayerPrefs.GetInt("CurrentScore");
    }
    
    public void ResetScore()
    {
        PlayerPrefs.DeleteAll();
    }
}
