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

    private enum Sub {
        Main,
        HowToPlay,
    }

    private void Awake() {
        transform.Find("howToPlaySub").GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        transform.Find("mainSub").GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

        transform.Find("mainSub").Find("playBtn").GetComponent<Button_UI>().ClickFunc = 
            () => ServiceLocator.GetService<ILoaderService>().Load(SceneTypes.GameScene);
        transform.Find("mainSub").Find("playBtn").GetComponent<Button_UI>().AddButtonSounds();

        transform.Find("mainSub").Find("quitBtn").GetComponent<Button_UI>().ClickFunc = () => Application.Quit();
        transform.Find("mainSub").Find("quitBtn").GetComponent<Button_UI>().AddButtonSounds();

        transform.Find("mainSub").Find("howToPlayBtn").GetComponent<Button_UI>().ClickFunc = () => ShowSub(Sub.HowToPlay);
        transform.Find("mainSub").Find("howToPlayBtn").GetComponent<Button_UI>().AddButtonSounds();

        transform.Find("howToPlaySub").Find("backBtn").GetComponent<Button_UI>().ClickFunc = () => ShowSub(Sub.Main);
        transform.Find("howToPlaySub").Find("backBtn").GetComponent<Button_UI>().AddButtonSounds();

        ShowSub(Sub.Main);
    }

    private void ShowSub(Sub sub) {
        transform.Find("mainSub").gameObject.SetActive(false);
        transform.Find("howToPlaySub").gameObject.SetActive(false);

        switch (sub) {
        case Sub.Main:
            transform.Find("mainSub").gameObject.SetActive(true);
            break;
        case Sub.HowToPlay:
            transform.Find("howToPlaySub").gameObject.SetActive(true);
            break;
        }
    }

}
