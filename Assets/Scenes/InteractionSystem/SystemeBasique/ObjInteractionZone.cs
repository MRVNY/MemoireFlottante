using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class ObjInteractionZone : MonoBehaviour
{
    public string interactableObjectsMaskName;
    public AudioClip interactionSoundClip;
    
    //The objects being in the area are stored in the list 
    public List<GameObject> areaObjList = new List<GameObject>();
    private TextAppearInteraction tAppInt;

    void Start() {
        tAppInt = gameObject.GetComponent<TextAppearInteraction>();
    }

    void Update() {
        if((Keyboard.current!=null && Keyboard.current.eKey.wasPressedThisFrame) || 
            (Gamepad.current!=null && Gamepad.current.yButton.wasPressedThisFrame)) //ChangeButton
        {
            Interaction(areaObjList[0]);
            Debug.Log("E pressed");
        }
        
    }
    

    void OnTriggerEnter(Collider coll)
    {
        if(coll.gameObject.layer == LayerMask.NameToLayer(interactableObjectsMaskName)) { //Checking if the object is in the layer we need
            GameObject obj = coll.gameObject;
            areaObjList.Add(obj);
            OutlineOn(obj);
            tAppInt.ShowObjText(obj);
        }
    }

    void OnTriggerExit(Collider coll) {
        GameObject obj = coll.gameObject;
        areaObjList.Remove(obj);
        OutlineOff(obj);
        Destroy(GameObject.Find(obj.name+"Text"));
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

    private void Interaction(GameObject obj) {
        gameObject.GetComponent<TextUIInteraction>().ShowTextUI(obj);
        //Animation
        GameObject.Find("MainCamera").GetComponent<AudioSource>().PlayOneShot(interactionSoundClip, 1.0f);
        Destroy(obj);
    }
}
