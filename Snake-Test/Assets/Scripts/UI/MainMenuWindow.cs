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
using UnityEngine;
using CodeMonkey.Utils;
using GameHouse.Snake.Extensions;
using GameHouse.Snake.Scenes;
using GameHouse.Snake.Services;

public class MainMenuWindow : MonoBehaviour {

    private const string MAIN_SUB = "mainSub";
    private const string HOW_TO_PLAY_SUB = "howToPlaySub";
    private const string PLAY_BUTTON = "playBtn";
    private const string QUIT_BUTTON = "quitBtn";
    private const string BACK_BUTTON = "backBtn";
    private const string HOW_TO_PLAY_BUTTON = "howToPlayBtn";
    private enum Sub {
        Main,
        HowToPlay,
    }

    private void Awake() {
        transform.Find(HOW_TO_PLAY_SUB).GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        transform.Find(MAIN_SUB).GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

        transform.Find(MAIN_SUB).Find(PLAY_BUTTON).GetComponent<Button_UI>().ClickFunc = 
            () => ServiceLocator.GetService<ILoaderService>().Load(SceneTypes.GameScene);
        transform.Find(MAIN_SUB).Find(PLAY_BUTTON).GetComponent<Button_UI>().AddButtonSounds();

        transform.Find(MAIN_SUB).Find(QUIT_BUTTON).GetComponent<Button_UI>().ClickFunc = () => Application.Quit();
        transform.Find(MAIN_SUB).Find(QUIT_BUTTON).GetComponent<Button_UI>().AddButtonSounds();

        transform.Find(MAIN_SUB).Find(HOW_TO_PLAY_BUTTON).GetComponent<Button_UI>().ClickFunc = () => ShowSub(Sub.HowToPlay);
        transform.Find(MAIN_SUB).Find(HOW_TO_PLAY_BUTTON).GetComponent<Button_UI>().AddButtonSounds();

        transform.Find(HOW_TO_PLAY_SUB).Find(BACK_BUTTON).GetComponent<Button_UI>().ClickFunc = () => ShowSub(Sub.Main);
        transform.Find(HOW_TO_PLAY_SUB).Find(BACK_BUTTON).GetComponent<Button_UI>().AddButtonSounds();

        ShowSub(Sub.Main);
    }

    private void ShowSub(Sub sub) {
        transform.Find(MAIN_SUB).gameObject.SetActive(false);
        transform.Find(HOW_TO_PLAY_SUB).gameObject.SetActive(false);

        switch (sub) {
        case Sub.Main:
            transform.Find(MAIN_SUB).gameObject.SetActive(true);
            break;
        case Sub.HowToPlay:
            transform.Find(HOW_TO_PLAY_SUB).gameObject.SetActive(true);
            break;
        }
    }

}
