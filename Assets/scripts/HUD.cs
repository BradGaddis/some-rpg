using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    HUD instance = null;
    PlayerHealth playerHealth;
    Slider playerHealthSlider;
    

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            // destroy all chirldren of this object
            foreach (Transform child in transform) {
                Destroy(child.gameObject);
            }
            Destroy(gameObject);
        }
        Object.DontDestroyOnLoad(gameObject);
        // find slider by name
        playerHealthSlider = GameObject.Find("Health Bar").GetComponent<Slider>();
        playerHealth = FindObjectOfType<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        playerHealthSlider.value = playerHealth.GetHealth();
    }
}
