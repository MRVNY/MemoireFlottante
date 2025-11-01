using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjInteractionZone : MonoBehaviour
{
    public string interactableObjectsMaskName;
    
    //The objects being in the area are stored in the list 
    public List<GameObject> areaObjList = new List<GameObject>();
    private TextAppearInteraction tAppInt;

    void Start() {
        tAppInt = gameObject.GetComponent<TextAppearInteraction>();
    }
    

    void OnTriggerEnter(Collider coll)
    {
        if(coll.gameObject.layer == LayerMask.NameToLayer(interactableObjectsMaskName)) { //Checking if the object is in the layer we need
            GameObject obj = coll.gameObject;
            areaObjList.Add(obj);
            OutlineOn(obj);
            tAppInt.ShowObjText(obj);
            Debug.Log("Object in interaction");
        }
    }

    void OnTriggerExit(Collider coll) {
        GameObject obj = coll.gameObject;
        areaObjList.Remove(obj);
        OutlineOff(obj);
        Destroy(GameObject.Find(obj.name+"Text"));
        Debug.Log("object none interaction");
    }

    bool FindObjectInList(GameObject obj, List<GameObject> list) {
        for(int i = 0; i < list.Count; i++) {
            if(list[i] == obj) {
                return true;
            }
        }
        return false;
    }

    private void OutlineOn(GameObject obj) {
        if(obj.GetComponent<Outline>() == null) {
            var outline = obj.AddComponent<Outline>();

            outline.OutlineMode = Outline.Mode.OutlineAll;
            outline.OutlineColor = Color.white;
            outline.OutlineWidth = 10f;    
        }
        obj.GetComponent<Outline>().enabled = true;
    }

    private void OutlineOff(GameObject obj) {
        obj.GetComponent<Outline>().enabled = false;
    }
}
