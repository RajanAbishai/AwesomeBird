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

    [HideInInspector]
    public bool playGame;

    public Text mainmenu_DisplayBestScoreText, birdMenu_DisplayBestScoreText,birdMenu_DisplayDiamondScoreText;

    public GameObject[] birds;
    public GameObject[] bird_Price_Text;
    public GameObject[] bird_Icons;
    

    void Awake () {
        MakeInstance();
	}

    void Start()
    {
        mainmenu_DisplayBestScoreText.text = "Best: "+GameManager.instance.bestScore;

        
        InstantiatePlayer(); 
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

        //calling the animation directly instead of setting a trigger/bool/float.. calling it directly by using Play()

        //This will save game data

        int currentBestScore = GameManager.instance.bestScore;

        if (currentBestScore < count_Score)
        {
            //currentBestScore = count_Score; 
            GameManager.instance.bestScore = count_Score;
        }

        GameManager.instance.diamondScore += count_Diamond;

        if (count_Score >= 25)
        {
            GameManager.instance.birds[GameManager.instance.birds.Length - 1] = true; // 7-1 = 6. 6th is the last bird on the array
        }
        //
        GameManager.instance.SaveGameData(); 




    }
    
    //this function that needs to be used like this has to be public void. Can take one or no parameters    
    public void PlayGame()
    {
        //If we are not playing the game, then we should start playing the game
        if (!playGame)
        {
            mainMenuObj.SetActive(false);
            UIObj.SetActive(true);

            playGame = true;

            bestScoreText.text = "Best: " + GameManager.instance.bestScore;
            totalDiamondScoreText.text = "Total: " + GameManager.instance.diamondScore; //changed to best when "Best: " was there instead of "Total: "
        }
    }

    public void RestartGame()
    {
        //SceneManager.LoadScene(TagManager.GAMEPLAY_SCENE_TAG);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

    public void MainMenu()
    {
        gameOverObj.SetActive(false);
        birdMenuObj.SetActive(false);
        mainMenuObj.SetActive(true);

        InstantiatePlayer();
    }

    public void BirdMenu()
    {
        birdMenuObj.SetActive(true);
        mainMenuObj.SetActive(false);

        birdMenu_DisplayBestScoreText.text = "Best: " +GameManager.instance.bestScore;
        birdMenu_DisplayDiamondScoreText.text = GameManager.instance.diamondScore.ToString();

        CheckBirds();

    }

    void CheckBirds()
    {
        //we are gonna filter over the array where we added these price texts. When the bird is unlocked, we will display the icons instead of cost
        for (int i = 0; i < bird_Price_Text.Length; i++) 
        {
            bird_Price_Text[i].SetActive(!GameManager.instance.birds[i + 1]);

            //birds_Price_Text array has 6 elements (because first bird is already unlocked) and birds array has 7 elements.. 
            //+1 is used to avoid the first element in the array. If what's in the bracket returns false, it will return true


            bird_Icons[i].SetActive(GameManager.instance.birds[i + 1]); //if this returns true, activate the bird icon

        }


    }


    public void UnlockAndSelectBird()  
    {
        //we get the name of the object that is pressed
        //UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name returns the name of the game object that is currently touched
        int selectedBirdIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);

        /* We use selectedBirdIndex.ToString to convert to a string.. but to convert string to an integer, we use int.Parse()
         *int.Parse -> Convert into an integer
         */

        if (!GameManager.instance.birds[selectedBirdIndex]) //if the bird at this index is locked, we are gonna unlock it
        {
            //Type code to unlock the bird

            switch (selectedBirdIndex)
            {
                case 1:
                    if (GameManager.instance.diamondScore >= 25) {
                        GameManager.instance.birds[selectedBirdIndex] = true;
                        GameManager.instance.diamondScore -= 25;
                        GameManager.instance.selected_Index = selectedBirdIndex;
                        print("bought and selected bird 2");
                    }
                    break;
                
                case 2:
                    if (GameManager.instance.diamondScore >= 50) {
                        GameManager.instance.birds[selectedBirdIndex] = true;
                        GameManager.instance.diamondScore -= 50;
                        GameManager.instance.selected_Index = selectedBirdIndex;
                        print("bought and selected bird 3");
                    }            
                    break;

                case 3:
                    if (GameManager.instance.diamondScore >= 75) {
                        GameManager.instance.birds[selectedBirdIndex] = true;
                        GameManager.instance.diamondScore -= 75;
                        GameManager.instance.selected_Index = selectedBirdIndex;
                        print("bought and selected bird 4");
                    }
                    break;

                case 4:
                    if (GameManager.instance.diamondScore >= 100) {
                        GameManager.instance.birds[selectedBirdIndex] = true;
                        GameManager.instance.diamondScore -= 100;
                        GameManager.instance.selected_Index = selectedBirdIndex;
                        print("bought and selected bird 5");
                    }
                    break;

                case 5:
                    if (GameManager.instance.diamondScore >= 100) {
                        GameManager.instance.birds[selectedBirdIndex] = true;
                        GameManager.instance.diamondScore -= 100;
                        GameManager.instance.selected_Index = selectedBirdIndex;
                        print("bought and selected bird 6");
                    }
                    break;

                //we don't have case 6 because the last bird will be unlocked when we reach level 25

            }



        }

        else
        {
            GameManager.instance.selected_Index = selectedBirdIndex;
                
        }


        CheckBirds();
        birdMenu_DisplayDiamondScoreText.text = GameManager.instance.diamondScore.ToString();
        GameManager.instance.SaveGameData();
        



    }

    void InstantiatePlayer()
    {
        /*Get the player, his position and turn him off*/
        GameObject player = GameObject.FindGameObjectWithTag(TagManager.PLAYER_TAG); 
        Vector3 pos = player.transform.position;
        Destroy(player);
        
        //player.SetActive(false);
        

        
        Instantiate(birds[GameManager.instance.selected_Index], pos, Quaternion.identity); 


        Camera.main.gameObject.GetComponent<CameraFollow>().FindPlayer();

        
        
       
        

    }



}
