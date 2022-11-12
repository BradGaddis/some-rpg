// // create start menu

// using UnityEngine;
// using UnityEngine.SceneManagement;

// public class StartMenu : MonoBehaviour
// {
//     // Create start game gui button
//     void OnGUI()
//     {
//         if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 25, 100, 50), "Start Game"))
//         {
//             // Load active scene    
//             SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
//         }

//         if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2 + 25, 100, 50), "Quit Game"))
//         {
//             Application.Quit();
//             Debug.Log("Quit Game");
//         }
        
//     }

//     public void PlayGame()
//     {
//         SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
//     }  

//     public void QuitGame()
//     {
//         Debug.Log("Quit");
//         Application.Quit();
//     }
// }
