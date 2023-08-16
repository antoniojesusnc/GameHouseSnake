using System;
using System.Collections.Generic;
using UnityEngine.Assertions;

namespace GameHouse.Snake.Services
{
    public class ServiceLocator
    {
        private static Dictionary<Type, IService> _services = new Dictionary<Type, IService>();

        public static void RegisterService<T>(T service) where T : IService
        {
            var type = typeof(T);
            Assert.IsFalse(_services.ContainsKey(type),
                           $"Service {type} already registered");

            _services.Add(type, service);
            service.Init();
        }

        public static T GetService<T>() where T : IService
        {
            var type = typeof(T);
            if (!_services.TryGetValue(type, out var service))
            {
                throw new Exception($"Service {type} not found");
            }

            return (T)service;
        }
    }
}