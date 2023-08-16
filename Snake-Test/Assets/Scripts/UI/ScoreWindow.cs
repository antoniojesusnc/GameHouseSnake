/* 
    ------------------- Code Monkey -------------------

    Thank you for downloading this package
    I hope you find it useful in your projects
    If you have any questions let me know
    Cheers!

               unitycodemonkey.com
    --------------------------------------------------
 */

using System.Collections;
using System.Collections.Generic;
using GameHouse.Snake.Services;
using UnityEngine;
using UnityEngine.UI;

public class ScoreWindow : MonoBehaviour {

    private static ScoreWindow instance;

    private Text scoreText;

    private IScoreService _scoreServiceService;
    
    private void Awake() {
        instance = this;
        scoreText = transform.Find("scoreText").GetComponent<Text>();
        _scoreServiceService = ServiceLocator.GetService<IScoreService>();
    }

    private void Start() {
        _scoreServiceService.OnHighscoreChanged += Score_OnHighscoreChanged;
        UpdateHighscore();
    }

    private void Score_OnHighscoreChanged(object sender, System.EventArgs e) {
        UpdateHighscore();
    }

    private void Update() {
        scoreText.text = _scoreServiceService.GetScore().ToString();
    }

    private void UpdateHighscore() {
        int highscore = _scoreServiceService.GetHighscore();
        transform.Find("highscoreText").GetComponent<Text>().text = "HIGHSCORE\n" + highscore.ToString();
    }

    public static void HideStatic() {
        instance.gameObject.SetActive(false);
    }
}
