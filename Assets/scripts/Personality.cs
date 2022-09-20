using UnityEngine;
using System.Collections.Generic;

using UnityEngine;

[CreateAssetMenu(fileName = "New Personality", menuName = "Characters/Personality", order = 0)]
public class Personality : ScriptableObject {
    // a series of values that any character can have

    [Header("Courage Value")]
    [SealializeField]
    int courageValue = 0;
    [Header("Friendliness Value")]
    [SealializeField]
    int amicabilityValue = 0;

    [SerializeField]
    List<string> likesAndPerference = new List<string>();
}