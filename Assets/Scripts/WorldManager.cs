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

        if (!_flipping)
        {
            StartCoroutine(FlipAsync());
            
            if(transform.eulerAngles.z < 90 || transform.eulerAngles.z > 270)
            {
                //flipping to down world
                ObjectManager.Instance.OnFlipUnder();
            }
            else
            {
                //flipping to up world
                ObjectManager.Instance.OnFlipUp();
            }
        }
        

    }
    
    IEnumerator FlipAsync(bool foward = true)
    {
        _flipping = true;
        float angle = 180 * (foward ? 1 : -1);
        float duration = 0.5f;
        float elapsed = 0f;
        Vector3 originalRotation = transform.eulerAngles;
        Vector3 playerPos = Player.Instance.transform.position;
        Vector3 rotCenter = new Vector3(playerPos.x, playerPos.y, 0);
        Player.Instance.SwitchFlip(true);
        
        _upWorldCollider.enabled = false;
        _downWorldCollider.enabled = false;
        
        while (elapsed < duration)
        {
            float step = angle * (Time.deltaTime / duration);
            //rotate around Player position
            if(foward) transform.RotateAround(rotCenter, Vector3.forward, step);
            else transform.RotateAround(rotCenter, Vector3.forward, -step);
            // transform.Rotate(Vector3.forward, step);
            elapsed += Time.deltaTime;
            yield return null;
        }
        // Ensure final rotation is exact
        transform.eulerAngles = originalRotation + new Vector3(0, 0, angle);
        
        _upWorldCollider.enabled = true;
        _downWorldCollider.enabled = true;
        _flipping = false;
        Player.Instance.SwitchFlip(false);
    }
    
    
}
