using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void OnLevel1ButtonPressed()
    {
        SceneManager.LoadScene("Level1");
    }

    public void OnLevel2ButtonPressed()
    {
        SceneManager.LoadScene("Level2");
    }

    public void OnExitToDesktopButtonPressed()
    {
        #if UNITY_EDITOR
        EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
