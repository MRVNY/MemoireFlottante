using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;
    private Collider _playerCollider;
    private Rigidbody _playerRigidbody;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instance = this;
        _playerCollider = GetComponent<Collider>();
        _playerRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchFlip(bool isFlipping)
    {
        // _playerCollider.enabled = !isFlipping;
        // _playerRigidbody.useGravity = !isFlipping;
    }
    
}
