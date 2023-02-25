using UnityEngine;
using UnityEngine.SceneManagement;

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
}
