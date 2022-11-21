using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Dialog Scriptable Object
[CreateAssetMenu(fileName = "New Dialogue", menuName = "Speech/Dialogue")]
public class Dialogue : ScriptableObject
{
    [SerializeField]
    DialogueNode[] nodes;
}