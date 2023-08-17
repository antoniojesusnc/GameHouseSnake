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

public class PauseWindow : MonoBehaviour
{

    private const string RESUME_BUTTON = "resumeBtn";
    private const string MAIN_MENU_BUTTON = "mainMenuBtn";

    private static PauseWindow instance;

    private void Awake()
    {
        instance = this;

        transform.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        transform.GetComponent<RectTransform>().sizeDelta = Vector2.zero;

        transform.Find(RESUME_BUTTON).GetComponent<Button_UI>().ClickFunc =
            () => ServiceLocator.GetService<IGamePlayService>().ResumeGame();
        transform.Find(RESUME_BUTTON).GetComponent<Button_UI>().AddButtonSounds();

        transform.Find(MAIN_MENU_BUTTON).GetComponent<Button_UI>().ClickFunc =
            () => ServiceLocator.GetService<ILoaderService>().Load(SceneTypes.MainMenu);
        transform.Find(MAIN_MENU_BUTTON).GetComponent<Button_UI>().AddButtonSounds();

        Hide();
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    public static void ShowStatic()
    {
        instance.Show();
    }

    public static void HideStatic()
    {
        instance.Hide();
    }
}
