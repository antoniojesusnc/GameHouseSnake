using CodeMonkey.Utils;
using GameHouse.Snake.Services;
using GameHouse.Snake.Sounds;

namespace GameHouse.Snake.Extensions
{
    public static class UIExtensions
    {
        public static void AddButtonSounds(this Button_UI buttonUI)
        {
            var soundService = ServiceLocator.GetService<ISoundService>();
            buttonUI.MouseOverOnceFunc += () => soundService.PlaySound(SoundTypes.ButtonOver);
            buttonUI.ClickFunc += () => soundService.PlaySound(SoundTypes.ButtonClick);
        }
    }
}