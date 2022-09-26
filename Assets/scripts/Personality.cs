using UnityEngine;
using System.Collections.Generic;


[CreateAssetMenu(fileName = "New Personality", menuName = "Characters/Personality", order = 0)]
public class Personality : ScriptableObject {
    // a series of values that any character can have

    [Header("Courage Value")]
    [SerializeField]
    int courageValue = 0;
    [Header("Friendliness Value")]
    [SerializeField]
    int amicabilityValue = 0;

    [SerializeField]
    List<string> likesAndPerference = new List<string>();
}