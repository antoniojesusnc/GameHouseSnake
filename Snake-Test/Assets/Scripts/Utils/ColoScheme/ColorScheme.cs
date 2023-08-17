using GameHouse.Snake.Config;
using GameHouse.Snake.Services;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace GameHouse.Snake.Utils
{
    public class ColorScheme : MonoBehaviour
    {
        private const string COLOR_SCHEME_ADDRESS_NAME = "ColorSchemeConfig";
        
        [SerializeField] 
        private ColorSchemeType _colorSchemeType;
 
        private static ColorSchemeConfig _colorSchemeConfig;

        private void Awake()
        {
            if (_colorSchemeConfig == null)
            {
                ServiceLocator.GetService<IAssetService>().
                               LoadWithAddress<ColorSchemeConfig>(COLOR_SCHEME_ADDRESS_NAME, OnLoadColorScheme);
            }
            else
            {
                LoadColorScheme();
            }
        }

        private void OnLoadColorScheme(ColorSchemeConfig colorSchemeConfig)
        {
            _colorSchemeConfig = colorSchemeConfig;

            LoadColorScheme();
        }

        private void LoadColorScheme()
        {
            switch(_colorSchemeType)
            {
                case ColorSchemeType.MenuBackgroundColor:
                    ChangeCameraBackground(_colorSchemeConfig.MenuBackgroundColor);
                    break;
                case ColorSchemeType.MenuMainLogoTextColor:
                    ChangeTextColor(_colorSchemeConfig.MenuMainLogoTextColor);
                    break;
                case ColorSchemeType.MenuTextColor:
                    ChangeTextColor(_colorSchemeConfig.MenuTextColor);
                    break;
                case ColorSchemeType.GamePlayBackgroundColor:
                    ChangeCameraBackground(_colorSchemeConfig.GamePlayBackgroundColor);
                    break;
                case ColorSchemeType.GamePlayScoreTextColor:
                    ChangeTextColor(_colorSchemeConfig.GamePlayScoreTextColor);
                    break;
                case ColorSchemeType.GamePlayHighScoreTextColor:
                    ChangeTextColor(_colorSchemeConfig.GamePlayHighScoreTextColor);
                    break;
            }
        }

        private void ChangeCameraBackground(Color color)
        {
            GetComponent<Camera>().backgroundColor = color;
        }

        private void ChangeTextColor(Color color)
        {
            var textMeshPro = GetComponent<TextMeshProUGUI>();
            if (textMeshPro != null)
            {
                textMeshPro.color = color;
            }
        }
    }
}