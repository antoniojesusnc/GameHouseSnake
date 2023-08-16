using GameHouse.Snake.Services;
using UnityEngine;

public class ClockServiceGameObject : MonoBehaviour
{
    private ClockService _clockService;
    
    public void SetClockService(ClockService clockService)
    {
        _clockService = clockService;
    }
    
    void Update()
    {
        _clockService.CallUpdate(Time.deltaTime);
    }
}
