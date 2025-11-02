using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PathCreation.Examples;
using UnityEngine.InputSystem;

public class ObjInteractionZone : MonoBehaviour
{
    public string interactableObjectsMaskName;
    public AudioClip interactionSoundClip;
    
    //The objects being in the area are stored in the list 
    public List<GameObject> areaObjList = new List<GameObject>();
    private GameObject currentObj = null;
    private TextAppearInteraction tAppInt;
    
    private bool interacting = false;

    void Start() {
        // tAppInt = gameObject.GetComponent<TextAppearInteraction>();
        areaObjList = new List<GameObject>();
    }

    void Update() {
        if((Keyboard.current!=null && Keyboard.current.eKey.wasPressedThisFrame) || 
            (Gamepad.current!=null && Gamepad.current.xButton.wasPressedThisFrame)) 
        {
            Debug.Log("E pressed");
            if (interacting)
            {
                Info.Instance.OnHideInfo();
                currentObj.gameObject.SetActive(false);
                areaObjList.Clear();
                interacting = false;
            }
            else
            {
                if (areaObjList.Count > 0)
                {
                    currentObj = areaObjList[0];
                    Interaction(currentObj);
                }
            }
        }     
    }

    void OnTriggerEnter(Collider coll)
    {
        // print("Trigger Entered by " + coll.gameObject.name);
        // if(coll.gameObject.layer == LayerMask.NameToLayer(interactableObjectsMaskName)) { //Checking if the object is in the layer we need
        PathFollower targetCollider = coll.GetComponent<PathFollower>();
        print(targetCollider);
        if (targetCollider == null) targetCollider = coll.transform.parent.GetComponent<PathFollower>();
        // print("parent " + targetCollider.gameObject.name);
        if(targetCollider!=null){ 
            print("Valid Entered by " + targetCollider.gameObject.name);
            GameObject obj = targetCollider.gameObject;
            areaObjList.Add(obj);
            // OutlineOn(obj);
            // tAppInt.ShowObjText(obj);
        }
    }
    
    

    void OnTriggerExit(Collider coll) {
        // print("Trigger Entered by " + coll.gameObject.name);
        // if(coll.gameObject.layer == LayerMask.NameToLayer(interactableObjectsMaskName)) { //Checking if the object is in the layer we need
        PathFollower targetCollider = coll.GetComponent<PathFollower>();
        print(targetCollider);
        if (targetCollider == null) targetCollider = coll.transform.parent.GetComponent<PathFollower>();
        // print("parent " + targetCollider.gameObject.name);
        if(targetCollider!=null){ 
            print("Valid Entered by " + targetCollider.gameObject.name);
            GameObject obj = targetCollider.gameObject;
            areaObjList.Remove(obj);
            // OutlineOn(obj);
            // tAppInt.ShowObjText(obj);
        }
    }

    // private void OnTriggerStay(Collider other)
    // {
    //     throw new NotImplementedException();
    // }

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
        // gameObject.GetComponent<TextUIInteraction>().ShowTextUI(obj);
        Info.Instance.OnShowInfo(obj.GetComponentInChildren<PathFollower>().story);
        interacting = true;
        //Animation
        // GameObject.Find("Main Camera").GetComponent<AudioSource>().PlayOneShot(interactionSoundClip, 1.0f);
        // Destroy(obj);
    }
}
