using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public class GameManager : MonoBehaviour {

	public static GameManager instance;
	//GameManager.instance.(methodname) to access public method

	private GameData gameData;

	[HideInInspector]
	public int bestScore, diamondScore;

	[HideInInspector]
	public bool[] birds; //which birds are locked and which are not

	[HideInInspector]
	public int selected_Index;


	void Awake()
    {
		MakeInstance();
		InitializeGameData();
    }
	
	void Start()
    {
		/*To see where the  Game data file is stored*/
		//print("Path: "+Application.persistentDataPath+TagManager.GAME_DATA ); 
    }


	
	void MakeInstance()
	{

        if (instance == null)
        {
			instance = this;
        }


	}

	public void InitializeGameData()
    {
		/*first we are gonna try and load the game data if it is there.. if not,  test if game data is absent*/
		LoadGameData();

		//If we haven't saved game data before, save initial values
        if (gameData == null) {

			//Initializing game data for the first time
			//print("Initializing game data for the first time");
			bestScore = 0;
			diamondScore = 0;
			
			selected_Index = 0;
			birds = new bool[7];

			birds[0] = true; //first bird is unlocked

			for(int i = 1; i < birds.Length; i++) //start with 1 because first bird is already unlocked
            {
				birds[i] = false;
            }
			gameData = new GameData();

			gameData.BestScore = bestScore;
			gameData.DiamondScore = diamondScore;
			gameData.SelectedIndex = selected_Index;
			gameData.Birds = birds;

			SaveGameData();

			/*this will be done only once.. the next time will be if you uninstall the game and try to reinstall the game 
			 to set up initial data*/

		}

       // else { print("Game data already initialized"); }

	}


	public void SaveGameData()
    {
		FileStream file = null;

        try {
			BinaryFormatter bf = new BinaryFormatter();
			file = File.Create(Application.persistentDataPath + TagManager.GAME_DATA);

            if (gameData != null) //we have data to save
            {
				/*all the variables that we want to save*/
				gameData.BestScore = bestScore;
				gameData.DiamondScore = diamondScore;
				gameData.SelectedIndex = selected_Index;
				gameData.Birds = birds;
				
				//to serialize, you need the location and the file name. Serializes and/or encrypts it

				bf.Serialize(file, gameData);
				 
            }
		
		}
		/*it allows us to run our game even when we have an exception thrown. It will not turn off*/
		catch (Exception e) {  
		
		
		}

        finally
        {
            if (file != null)
            {
				file.Close(); //important to close the file after using
            }

        }

    } // Save Game Data

	void LoadGameData()
    {
		FileStream file = null;
        
		try {

			BinaryFormatter bf = new BinaryFormatter();
			file = File.Open(Application.persistentDataPath + TagManager.GAME_DATA, FileMode.Open);
			gameData = (GameData)bf.Deserialize(file); // give it back to us as GameData 

            if (gameData != null)
            {
				bestScore = gameData.BestScore;
				diamondScore = gameData.DiamondScore;
				selected_Index = gameData.SelectedIndex;
				birds = gameData.Birds;

            }


		}
		
		catch(Exception e) { 
		
		
		}

        finally
        {
            if (file != null)
            {
				file.Close();
            }

        }


    } //Load Game Data




}
