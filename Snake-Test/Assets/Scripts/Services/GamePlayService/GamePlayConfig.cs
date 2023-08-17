using UnityEngine;

namespace GameHouse.Snake.Config
{
    [CreateAssetMenu(fileName = "GamePlayConfig", menuName = "GameHouse/GamePlayConfig", order = 1)]
    public class GamePlayConfig : ScriptableObject
    {
        [field: Header("World"), SerializeField]
        public Vector2Int GridSize { get; private set; }
    }
}