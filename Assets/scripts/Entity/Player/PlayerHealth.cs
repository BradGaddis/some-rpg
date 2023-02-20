using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : Health
{
    private void Awake() {
        isPlayer = true;
        isEnemy = false;
    }
}
