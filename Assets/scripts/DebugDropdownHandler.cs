using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DebugDropdownHandler : MonoBehaviour
{
    Transform dropdown;
    void Start()
    {
        dropdown = transform.Find("Dropdown");
        dropdown.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.BackQuote))
        {
            if (dropdown != null)
                dropdown.gameObject.SetActive(!dropdown.gameObject.activeSelf);
            else {
                Debug.LogError("No dropdown found in children of DebugDropdownHandler");
            }
        }
    }
    public void HandleDropdownValueChanged(int index)
    {
        LoadScene(index);
    }
    void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }


    public void HandleResetHealthButton()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            player.GetComponent<PlayerHealth>().ResetFullHealth();
        }
    }
}
