using UnityEngine;
using UnityEngine.InputSystem;

public class Intro : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Gamepad.current != null && Gamepad.current.aButton.wasPressedThisFrame)
        {
            LoadGameScene();
        }
    }
    
    public void LoadGameScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    }
}
