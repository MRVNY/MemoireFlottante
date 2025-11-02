using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class TextUIInteraction : MonoBehaviour
{
    public List<GameObject> TextImages = new List<GameObject>();
    public float appearWaitTime = 5f; //In seconds
    
    public void ShowTextUI(GameObject obj) {
        StartCoroutine("ShowTextUICoroutine", obj);
    }

    IEnumerator ShowTextUICoroutine(GameObject obj) {
        GameObject UIText = GetImageTextObject(obj.name+"TextUI");
        UIText.SetActive(true);
        Debug.Log("TextShowed");
        yield return new WaitForSeconds(appearWaitTime);
        UIText.SetActive(false);
        Debug.Log("TextHided");
    }

    private GameObject GetImageTextObject(string name) {
        GameObject obj = null;

        for(int i = 0; i < TextImages.Count; i++) {
            if(TextImages[i].name == name) {
                obj = TextImages[i];
            }
        }

        return obj;
    }
}
