using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


[Serializable] public class GameData {

	private int diamond_Score; //when the game is not played just once but multiple times
	private int best_Score;

	private bool[] birds; //to tell which birds have been unlocked
	private int selected_Index; //we need to know which bird we have selected.. because we are saving our data. If we are selecting the 3rd bird, it stores this
	
	//public accessor methods for the private variables here

	public int DiamondScore 
    {
        get{ return diamond_Score; }


        set{ diamond_Score=value;}
    }

	public int BestScore
    {
        get { return best_Score; }
        set { best_Score = value; }
    }

    public bool[] Birds
    {
        get{ return birds;}
        set { birds = value; }
    }

    public int SelectedIndex
    {
        get { return selected_Index; }
        set { selected_Index = value; }

    }


}
