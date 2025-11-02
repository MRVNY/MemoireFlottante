using UnityEngine;
using UnityEngine.InputSystem;

public class TitleScreen : MonoBehaviour
{
    [SerializeField] GameObject creditsPanel;
    [SerializeField] private GameObject titleVideo;
    [SerializeField] private GameObject startVideo;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        creditsPanel.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Gamepad.current != null)
        {
            if(Gamepad.current.aButton.wasPressedThisFrame)
                OnStartClicked();
            else if (Gamepad.current.yButton.wasPressedThisFrame)
                OnCreditsClicked();
            else if (Gamepad.current.bButton.wasPressedThisFrame && creditsPanel.activeSelf)
                OnCloseCreditsClicked();
                
            
        }
        
    }

    public void OnStartClicked()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
    }

    public void OnCreditsClicked()
    {
        creditsPanel.SetActive(true);
    }
    
    public void OnCloseCreditsClicked()
    {
        creditsPanel.SetActive(false);
    }
}
