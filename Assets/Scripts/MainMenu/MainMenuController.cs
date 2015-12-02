using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// MainMenuController is a script that will be referenced by the UI to execute certain commands when you interact with the GUI in the MainMenu
/// </summary>
public class MainMenuController : MonoBehaviour
{
    /// <summary>
    /// Used for Unity UI wherein you will be calling this function in the inspector of the 
    /// UI that you will be executing then in the parameter you will be putting the name of 
    /// the scene that you will be loading
    /// </summary>
    /// <param name="sceneToLoad"></param>
    public void LoadScene(string sceneToLoad)
    {
        Application.LoadLevel(sceneToLoad);
    }

    /// <summary>
    /// Used to Quit or Exit the game when executed in the UI
    /// </summary>
    public void QuitGame()
    {
        #if UNITY_EDITOR
                EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }
}
