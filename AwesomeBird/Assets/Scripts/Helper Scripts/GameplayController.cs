using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameplayController : MonoBehaviour {

    public static GameplayController instance;
    public Text scoreText, bestScoreText, diamondScoreText, totalDiamondScoreText;

    private int count_Score, count_Diamond;

    public GameObject mainMenuObj, gameOverObj, birdMenuObj, UIObj;
    /*To turn them off.. No need to have the UIObj (score) displayed when we are in the main menu*/



    [HideInInspector]
    public bool playGame; //we need it to control the gameplay but it should not be visible in the inspector.. because we are not gonna check it in the inspector panel

    public Text mainMenu_DisplayBestScoreText,birdMenu_DisplayBestScoreText,
        birdMenu_DisplayDiamondScoreText;

	void Awake () {
        MakeInstance();
	}
	
    void Start()
    {
        mainMenu_DisplayBestScoreText.text = "Best :" + GameManager.instance.bestScore; //displays the best score to the user in the main menu
    }


	void Update () {
        
	}

    void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void DisplayScore(int score, int diamond)
    {
        count_Score += score; // adding parameter to the score 
        count_Diamond += diamond; // adding parameter to the diamond

        scoreText.text = count_Score.ToString();
        diamondScoreText.text = count_Diamond.ToString();
    }

    public void GameOver()
    {
        
        playGame = false;
        gameOverObj.SetActive(true);
        gameOverObj.GetComponent<Animator>().Play(TagManager.FADE_IN_ANIMATION);
        //Time.timeScale = 0f; this works for a pause but does not fade in.. can be made a coroutine

        int currentBestScore = GameManager.instance.bestScore;

        if (currentBestScore < count_Score) { //if current best score is lesser than the current score ingame

            GameManager.instance.bestScore = count_Score;
            
            //currentBestScore = count_Score;

        }

        GameManager.instance.diamondScore += count_Diamond; //we are appending to it

        if (count_Score >= 25) //to unlock the last bird
        {
            GameManager.instance.birds[GameManager.instance.birds.Length - 1] = true; //can also write 6 or 7-1 in the index
        }
        
        GameManager.instance.SaveGameData();


    }
    
    
    public void PlayGame() 
    {
        //This needs to be public void for this to work.. it can take 1 or 0 parameters.. bool, int, float, a string and an object


        //We add this function to the mainmenu button, OnClick. Drag the GamePlayController (gameobject) onto the mainmenu
        //If we are not playing the game, we should play the game.
        
        if (!playGame)
        {
            mainMenuObj.SetActive(false);
            UIObj.SetActive(true);

            playGame = true;
        }


    }

    public void RestartGame()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene(TagManager.GAMEPLAY_SCENE);
        
    }

    public void MainMenu()
    {
        gameOverObj.SetActive(false);
        birdMenuObj.SetActive(false);
        mainMenuObj.SetActive(true);
    }

    public void BirdMenu()
    {
        birdMenuObj.SetActive(true);
        mainMenuObj.SetActive(false);
        //gameOverObj.SetActive(false);

        birdMenu_DisplayBestScoreText.text = "Best: " +GameManager.instance.bestScore;
        birdMenu_DisplayDiamondScoreText.text = "Best: " + GameManager.instance.diamondScore.ToString();
    }

}
