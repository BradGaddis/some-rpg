using System.Collections;
using System.Collections.Generic;
using RPG.Dialogue;
using UnityEngine;
using UnityEngine.UI;
public class FloatingTextManager : MonoBehaviour
{

    public static FloatingTextManager instance;
    public GameObject floatingTextPrefab;
    public GameObject canvas;

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }
    }
    public GameObject textContainer;
    public GameObject textPrefab;
    List<FloatingText> floatingTexts = new List<FloatingText>();



        [SerializeField]
        float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }


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

    // Update is called once per frame
    void Update()
    {
        foreach (FloatingText fText in floatingTexts)
        {
            fText.UpdateFloatingText(speed);
        }
    }
}
