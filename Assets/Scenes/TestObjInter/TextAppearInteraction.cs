using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TextAppearInteraction : MonoBehaviour
{
    public List<GameObject> objTexts = new List<GameObject>();

    public void ShowObjText(GameObject obj) {
        GameObject textObj = FindTextObjectByName(obj.name+"Text");
        var instantiatedObj = Instantiate(textObj, obj.transform.position, Quaternion.Euler(0f, 0f, 0f), obj.transform);
        instantiatedObj.GetComponent<ObjectText>().parentObj = obj;
    }

    private GameObject FindTextObjectByName(string objName) {
        for(int i = 0; i < objTexts.Count; i++) {
            if(objTexts[i].name == objName) {
                return objTexts[i];
            }
        }
        return null; //null return attention
    }
}
