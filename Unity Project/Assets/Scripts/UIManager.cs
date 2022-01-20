using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public Text FianalScoreText;
    public Text CurreanScoreText;
    [SerializeField]GameOverScreen gameover;

    GameObject obj;
    PlayerController player;
    void Start()
    {
        
        obj = GameObject.Find("Player");
        if (obj!=null)
        {
            player = obj.GetComponent<PlayerController>();
            EventSystemCustom.Instance.OnIncreaseScore.AddListener(IncreaseScoreBoard);
            EventSystemCustom.Instance.OnGameOver.AddListener(GameOver);
        }
       
    }

    public void IncreaseScoreBoard(float scoreToAdd)
    {
        player.Score += scoreToAdd;
        if (PlayerPrefs.GetFloat("High Score",0)< player.Score)
        {
            PlayerPrefs.SetFloat("High Score", player.Score);
        }
        FianalScoreText.text = player.Score.ToString();
        CurreanScoreText.text = "+"+scoreToAdd.ToString();
        Debug.Log(player.Score);
    }
    public void GameOver()
    {
        gameover.SetUp(player.Score);
    }


}
