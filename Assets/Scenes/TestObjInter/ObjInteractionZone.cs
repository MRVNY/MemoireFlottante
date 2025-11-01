using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjInteractionZone : MonoBehaviour
{
    public string interactableObjectsMaskName;
    
    //The objects being in the area are stored in the list 
    public List<GameObject> areaObjList = new List<GameObject>();

    void OnTriggerEnter(Collider coll)
    {
        if(coll.gameObject.layer == LayerMask.NameToLayer(interactableObjectsMaskName)) { //Checking if the object is in the layer we need
            GameObject obj = coll.gameObject;
            areaObjList.Add(obj);
            OutlineOn(obj);
            Debug.Log("Object in interaction");
        }
    }

    void OnTriggerExit(Collider coll) {
        GameObject obj = coll.gameObject;
        areaObjList.Remove(obj);
        OutlineOff(obj);
        Debug.Log("object none interaction");
    }

    bool FindObjectInList(GameObject obj, List<GameObject> list) {
        for(int i = 0; i < list.Count; i++) {
            if(list[i] == obj) { //If object is equal
                return true;
            }
        }

        return false;
    }

    public void OutlineOn(GameObject obj) {

    }

    public void OutlineOff(GameObject obj) {

    }
}
