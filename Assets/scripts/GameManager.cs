using System.Collections;
using System.Collections.Generic;
using RPG.Dialogue;
using UnityEngine;
using UnityEngine.UI;

namespace RPG
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance = null;

        void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }
            DontDestroyOnLoad(gameObject);
        }

        void Update()
        {
        }
    }

    
    // game state
    public enum GameState
    {
        MainMenu,
        InGame,
        Paused,
        GameOver
    }
}