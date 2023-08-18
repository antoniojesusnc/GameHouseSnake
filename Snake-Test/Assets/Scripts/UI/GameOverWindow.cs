using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;
using GameHouse.Snake.Scenes;
using GameHouse.Snake.Services;

namespace GameHouse.Snake.UI
{
    public class GameOverWindow : MonoBehaviour
    {
        private const string RETRY_BUTTON = "retryBtn";

        private static GameOverWindow instance;

        private void Awake()
        {
            instance = this;

            transform.Find(RETRY_BUTTON).GetComponent<Button_UI>().ClickFunc = () =>
            {
                ServiceLocator.GetService<ILoaderService>().Load(SceneTypes.GameScene);
            };

            Hide();
        }

        private void Show(bool isNewHighscore)
        {
            gameObject.SetActive(true);

            transform.Find("newHighscoreText").gameObject.SetActive(isNewHighscore);

            var scoreService = ServiceLocator.GetService<IScoreService>();
            transform.Find("scoreText").GetComponent<Text>().text =
                scoreService.GetScore().ToString();
            transform.Find("highscoreText").GetComponent<Text>().text = "HIGHSCORE " + scoreService.GetHighscore();
        }

        private void Hide()
        {
            gameObject.SetActive(false);
        }

        public static void ShowStatic(bool isNewHighscore)
        {
            instance.Show(isNewHighscore);
        }
    }
}