using System;
using UnityEngine;

namespace OneTwoPlay.Tools
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance != null)
                {
                    return _instance;
                }

                var obj = FindObjectOfType<T>();

                if (obj == null)
                    throw new Exception($"Non Singleton object with type {nameof(T)}");

                _instance = obj;
                
                return _instance;
            }
        }
    }
}