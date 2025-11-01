using Unity.VisualScripting;
using UnityEngine;

public class ShuffleObject : MonoBehaviour
{

    Rigidbody rigidBody;
    public float speed = 5;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Shuffle();
    }

    void Shuffle()
    {
        // sets x and z velocities to 0
        rigidBody.linearVelocity = new Vector3(0, rigidBody.linearVelocity.y, 0);


        while (false)
        {
            // Current position
            Vector3 currPosition = transform.position;

            currPosition.x += 2f; // Adds 0.1 to the x-coordinate
                                  // or
            currPosition.y += 2f; // Adds 1 to the y-coordinate
                                  // or
            currPosition.z += 2f; // Adds 2 to the z-coordinate


            // move the rigid body
            rigidBody.MovePosition(currPosition);
        }
    }
}