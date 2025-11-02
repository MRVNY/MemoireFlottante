using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Info : MonoBehaviour
{
    TextMeshProUGUI _infoText;
    public static Info Instance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instance = this;
        _infoText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        //if p clicked
        if ((Keyboard.current != null && Keyboard.current.pKey.wasPressedThisFrame) ||
            (Gamepad.current != null && Gamepad.current.startButton.wasPressedThisFrame))
        {
            SceneManager.LoadScene("EndScreen");
        }
    }
    
    public void OnShowInfo(string text)
    {
        _infoText.enabled = true;
        _infoText.text = text + "\n\n(Press E or X to close)";
        
        if(text=="") _infoText.text = "Hmm, I wonder..." + "\n\n(Press E or X to close)";
    }
    
    public void OnHideInfo()
    {
        _infoText.enabled = false;
        _infoText.text = "";

        if (ObjectManager.Instance.CountPathFollowers() < 2)
        {
            //to end EndScreen
            SceneManager.LoadScene("EndScreen");
        }
    }

    // public IEnumerator ShowText()
    // {
    //     
    //     
    // }
}
