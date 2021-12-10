using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int scoreToWin;
    public int curScore;
    public bool gamePaused;
    // Instance of Game Manager
    public static GameManager instance;

    void Awake()
    {
        //set the instance of this script
        instance = this;

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton("Cancel"))
        {
            TogglePauseGame();
        }
    }

    public void TogglePauseGame() 
    {
        //Freeze Game
        gamePaused = !gamePaused;
        Time.timeScale = gamePaused == true ? 0.0f : 1.0f;

        //toggle pause menu
        GameUI.instance.TogglePauseMenu(gamePaused);
    }

    public void AddSCore(int score)
    {
        curScore += score;

        //update score text
        GameUI.instance.UpdateScoreText(curScore);

        //Have we reached the score to win
        if(curScore >= scoreToWin)
            WinGame();
    }

    public void WinGame(){
        //Set game screen
        GameUI.instance.SetEndGameScreen(true,curScore);
    }
}
