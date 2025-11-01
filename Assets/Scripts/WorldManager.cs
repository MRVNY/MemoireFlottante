using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

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
    private bool _isDown = false;
    
    [SerializeField] private Volume postProcessingVolume;
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
        if(!_isDown && (Keyboard.current.fKey.wasPressedThisFrame || Gamepad.current.yButton.wasPressedThisFrame))
        {
            Flip();
        }
        
    }

    public void Flip()
    {

        // _animator.Play("FlipTo");

        if (!_flipping)
        {
            if (!_isDown)
            {
                _isDown = true;
                StartCoroutine(FlipAsync());
                
                if(transform.eulerAngles.z < 90 || transform.eulerAngles.z > 270)
                {
                    //flipping to down world
                    ObjectManager.Instance.OnFlipUnder();
                    // postProcessingVolume.gameObject.SetActive(true);
                }
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
        Vector3 rotCenter = new Vector3(playerPos.x, 0, playerPos.z);
        Vector3 cameraDir = Camera.main.transform.position - Player.Instance.transform.position;
        Vector3 rotDic = new Vector3(cameraDir.x, 0, cameraDir.z).normalized;
        Player.Instance.SwitchFlip(true);
        
        _upWorldCollider.enabled = false;
        _downWorldCollider.enabled = false;
        
        while (elapsed < duration)
        {
            float step = angle * (Time.deltaTime / duration);
            //rotate around Player position
            transform.RotateAround(rotCenter, rotDic, step);
            // transform.Rotate(Vector3.forward, step);
            elapsed += Time.deltaTime;
            yield return null;
        }
        // Ensure final rotation is exact
        transform.eulerAngles = originalRotation + new Vector3(0, 0, angle);
        transform.position = new Vector3(playerPos.x, 0, playerPos.z);
        
        
        _upWorldCollider.enabled = true;
        _downWorldCollider.enabled = true;
        _flipping = false;
        Player.Instance.SwitchFlip(false);

        if (_isDown)
        {
            postProcessingVolume.gameObject.SetActive(true);
            StartCoroutine(CountSeconds());
        }
        else
        {
            postProcessingVolume.gameObject.SetActive(false);
        }
    }
    
    IEnumerator CountSeconds()
    {
        
        yield return new WaitForSeconds(5f);
        _isDown = false;
        ObjectManager.Instance.OnFlipUp();
        // postProcessingVolume.gameObject.SetActive(false);
        StartCoroutine(FlipAsync(false));
    }
    
    
}
