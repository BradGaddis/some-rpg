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

        public GameObject textContainer;
        public GameObject textPrefab;
        List<FloatingText> floatingTexts = new List<FloatingText>();

        [SerializeField]
        float speed;

        FloatingText GetFloatingText()
        {
            FloatingText text = floatingTexts.Find(t => !t.active);

            if (text == null)
            {
                text = new FloatingText();
                text.gObj = Instantiate(textPrefab);
                text.gObj.transform.SetParent(textContainer.transform);
                text.text = text.gObj.GetComponent<Text>();

                floatingTexts.Add(text);
            }
            return text;    
        }
        
        public void ShowFloatingText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration){
            FloatingText fText = GetFloatingText();
            fText.text.text = msg;
            fText.text.fontSize = fontSize;
            fText.text.color = color;
            fText.gObj.transform.position = Camera.main.WorldToScreenPoint(position);
            fText.position = motion;
            fText.duration = duration;        
            fText.Show();
        }

        void Update()
        {
            foreach (FloatingText fText in floatingTexts)
            {
                fText.UpdateFloatingText(speed);
            }
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