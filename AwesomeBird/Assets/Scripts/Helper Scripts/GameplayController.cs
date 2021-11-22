using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayController : MonoBehaviour {

    public static GameplayController instance;
    public Text scoreText, bestScoreText, diamondScoreText, totalDiamondScoreText;

    private int count_Score, count_Diamond;




	void Awake () {
        MakeInstance();
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

}
