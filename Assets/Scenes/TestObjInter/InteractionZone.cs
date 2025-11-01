using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InteractionZone : MonoBehaviour
{
    public float raycastTiltDegree = 25; 
    public float raycastDist = 10;
    public string interactableObjectsMaskName;
    
    [SerializeField] private GameObject mainCamera; 
    LayerMask layerMask;

    //RayCast interaction borders vectors (zone is defined by 2 rays)
    Vector3 leftRayDir;
    Vector3 rightRayDir;

    //Put the script on camera ?
    void Start() {
        layerMask = LayerMask.GetMask(interactableObjectsMaskName);

        Vector3 directionLeft = new Vector3(mainCamera.transform.rotation.x, mainCamera.transform.rotation.y, -1);
        leftRayDir = Quaternion.Euler(0, raycastTiltDegree, 0) * directionLeft;
        Vector3 directionRight = new Vector3(mainCamera.transform.rotation.x, mainCamera.transform.rotation.y, 1);
        rightRayDir = Quaternion.Euler(0, raycastTiltDegree, 0) * directionLeft;
    }

    


    //The objects being in the area are stored in the list 
    List<GameObject> areaObjList = new List<GameObject>();
    

    void Update()
    {
        Debug.DrawRay(mainCamera.transform.position, leftRayDir, Color.green);
        Debug.DrawRay(mainCamera.transform.position, rightRayDir, Color.green);

        RaycastHit hit;
        if(Physics.Raycast(mainCamera.transform.position, leftRayDir, out hit, raycastDist, layerMask)) {
            GameObject hitObj = hit.collider.gameObject;
            if(!FindObjectInList(hitObj, areaObjList)) {
                areaObjList.Add(hitObj);
                //Outlining on
                Debug.Log("Object in interaction");
            } else {
                areaObjList.Remove(hitObj);
                //Outlining off
                Debug.Log("object none interaction");
            }
        }

        //Maybe second hit ?
        if(Physics.Raycast(mainCamera.transform.position, rightRayDir, out hit, raycastDist, layerMask)) {

        }
    }

    bool FindObjectInList(GameObject obj, List<GameObject> list) {
        for(int i = 0; i < list.Count; i++) {
            if(list[i] == obj) { //If object is equal
                return true;
            }
        }

        return false;
    }
}
