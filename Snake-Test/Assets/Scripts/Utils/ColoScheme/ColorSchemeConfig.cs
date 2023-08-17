using UnityEngine;

namespace GameHouse.Snake.Config
{
    [CreateAssetMenu(fileName = "ColorSchemeConfig", menuName = "GameHouse/ColorSchemeConfig", order = 1)]
    public class ColorSchemeConfig : ScriptableObject
    {
        [field: Header("MainMenu"), SerializeField]
        public Color MenuBackgroundColor { get; private set; }
        [field: SerializeField]
        public Color MenuMainLogoTextColor { get; private set; }
        [field: SerializeField]
        public Color MenuTextColor { get; private set; }
        
        [field: Header("GamePlay"), SerializeField]
        public Color GamePlayBackgroundColor { get; private set; }
        [field: SerializeField]
        public Color GamePlayScoreTextColor { get; private set; }
        [field: SerializeField]
        public Color GamePlayHighScoreTextColor { get; private set; }
    }
}