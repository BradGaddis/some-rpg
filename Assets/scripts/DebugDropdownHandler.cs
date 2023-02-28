using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using TMPro;

public class DebugDropdownHandler : MonoBehaviour
{
    Transform dropdown;
    bool hide = false;
    void Start()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
        dropdown = transform.Find("Dropdown");
        TMP_Dropdown dropdownComponent = dropdown.GetComponent<TMP_Dropdown>();
        foreach (string name in GetSceneNames())
        {
            dropdownComponent.options.Add(new TMP_Dropdown.OptionData(name));
            dropdownComponent.onValueChanged.AddListener(HandleDropdownValueChanged);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.BackQuote))
        {
            if (hide)
                Hide();
            else
                Show();
        }
    }
    public void HandleDropdownValueChanged(string name)
    {
        LoadScene(name);
    }
    public void HandleDropdownValueChanged(int index)
    {
        LoadScene(index);
    }

    void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }


    public void HandleResetHealthButton()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            player.GetComponent<PlayerHealth>().ResetFullHealth();
        }
    }

    public void Show()
    {
        hide = true;
        foreach(Transform child in transform){
            child.gameObject.SetActive(true);
        }
    }
    public void Hide()
    {
        hide = false;
        foreach(Transform child in transform){
            child.gameObject.SetActive(false);
        }
    }

    private List<string> GetSceneNames()
    {
        List<string> sceneNames = new List<string>();
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string path = SceneUtility.GetScenePathByBuildIndex(i);
            int lastSlash = path.LastIndexOf("/");
            string name = path.Substring(lastSlash + 1, path.LastIndexOf(".") - lastSlash - 1);
            sceneNames.Add(name);
        }
        return sceneNames;
    }
}
