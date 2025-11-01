using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WorldManager : MonoBehaviour
{
    public static WorldManager Instance;
    
    private Animator _animator;
    private PlayerInput _input;
    
    [SerializeField] private World upWorld;
    [SerializeField] private World downWorld;
    
    private MeshCollider _upWorldCollider;
    private MeshCollider _downWorldCollider;
    
    private bool _flipping = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instance = this;
        
        _animator = GetComponent<Animator>();
        _upWorldCollider = upWorld.GetComponent<MeshCollider>();
        _downWorldCollider = downWorld.GetComponent<MeshCollider>();
        _input = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        //on f click flip the world input player settings
        if(Keyboard.current.fKey.wasPressedThisFrame)
        {
            Flip();
        }
        
    }

    public void Flip()
    {

        // _animator.Play("FlipTo");

        if(!_flipping) StartCoroutine(FlipAsync());

    }
    
    IEnumerator FlipAsync()
    {
        _flipping = true;
        float angle = 180;
        float duration = 1f;
        float elapsed = 0f;
        
        _upWorldCollider.enabled = false;
        _downWorldCollider.enabled = false;
        
        while (elapsed < duration)
        {
            float step = angle * (Time.deltaTime / duration);
            transform.Rotate(Vector3.forward, step);
            elapsed += Time.deltaTime;
            yield return null;
        }
        // Ensure final rotation is exact
        transform.Rotate(Vector3.forward, angle - (elapsed - duration) * (angle / duration));
        
        _upWorldCollider.enabled = true;
        _downWorldCollider.enabled = true;
        _flipping = false;
    }
    
    
}
