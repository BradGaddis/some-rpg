// create character

using UnityEngine;

public class NPC : MonoBehaviour
{
    public GameObject character;
    public GameObject characterPrefab;
    public GameObject characterParent;


    public void CreateCharacter()
    {
        character = Instantiate(characterPrefab, characterParent.transform);
    }

    public void DestroyCharacter()
    {
        Destroy(character);
    }

    // if clicked on character, start dialogue
    private void OnMouseDown()
    {
        // if character exists, start dialogue
        if (character != null)
        {
            // start dialogue
        }
    }

    public void SayHello()
    {
        Debug.Log("Hello!");
    }
}

// create dialogue system

public class DialogueSystem : MonoBehaviour
{
    public GameObject dialogueBox;
    public GameObject dialogueText;
    public GameObject dialogueName;
    public GameObject dialoguePortrait;

    public void ShowBox(string dialogue, string name, Sprite portrait)
    {
        dialogueBox.SetActive(true);
        dialogueText.GetComponent<UnityEngine.UI.Text>().text = dialogue;
        dialogueName.GetComponent<UnityEngine.UI.Text>().text = name;
        dialoguePortrait.GetComponent<UnityEngine.UI.Image>().sprite = portrait;
    }

    public void HideBox()
    {
        dialogueBox.SetActive(false);
    }

}