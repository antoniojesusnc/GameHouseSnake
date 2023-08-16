using GameHouse.Snake.Services;
using UnityEngine;

namespace GameHouse.Snake.GamePlay
{
    
public class GamePlayGameObject : MonoBehaviour
{
    void Start()
    {
        ServiceLocator.GetService<IGamePlayService>().BeginLevel();
    }
}
}
