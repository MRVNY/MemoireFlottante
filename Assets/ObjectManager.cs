using System.Collections.Generic;
using System.Linq;
using PathCreation.Examples;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    List<PathFollower> pathFollowers = new List<PathFollower>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pathFollowers = GetComponentsInChildren<PathFollower>().ToList();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnFlipUnder()
    {
        foreach (var obj in pathFollowers)
        {
            obj.OnFlipUnder();
        }
    }

    void OnFlipUp()
    {
        foreach (var obj in pathFollowers)
        {
            obj.OnFlipUp();
        }
    }
    
}
