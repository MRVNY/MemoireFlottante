using UnityEngine;

public class ObjectText : MonoBehaviour
{
    private Transform mainCameraTs;
    public GameObject parentObj;

    public float textGapX = 1.01f;
    public float textGapY = 0.52f;
    public float textGapZ = 0.43f;

    void Start() {
        Vector3 gaps = new Vector3(textGapX, textGapY, textGapZ);
        gameObject.transform.position = parentObj.transform.position + gaps;
        mainCameraTs = GameObject.Find("MainCamera").transform;
    }

    void Update()
    {
        gameObject.transform.LookAt(mainCameraTs);
    }
}
