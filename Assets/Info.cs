using System.Collections;
using TMPro;
using UnityEngine;

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
        
    }
    
    public void OnShowInfo(string text)
    {
        _infoText.enabled = true;
        _infoText.text = text + "\n\n(Press E or X to close)";
    }
    
    public void OnHideInfo()
    {
        _infoText.enabled = false;
        _infoText.text = "";
    }

    // public IEnumerator ShowText()
    // {
    //     
    //     
    // }
}
