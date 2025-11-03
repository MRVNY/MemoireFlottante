using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    
    [SerializeField] private AudioClip UpMusic;
    [SerializeField] private AudioClip DownMusic;
    private float _musicProgress = 0f;
    private AudioSource _audioSource;
    
    private MeshCollider _upWorldCollider;
    private MeshCollider _downWorldCollider;
    
    private bool _flipping = false;
    private bool _isDown = false;
    
    [SerializeField] private Volume postProcessingVolume;
    [SerializeField] private TextMeshProUGUI counter;
    
    public int secondsDown = 5;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instance = this;
        
        _animator = GetComponent<Animator>();
        _upWorldCollider = upWorld.GetComponent<MeshCollider>();
        _downWorldCollider = downWorld.GetComponent<MeshCollider>();
        _input = GetComponent<PlayerInput>();
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //on f click flip the world input player settings
        if(!_isDown && !_flipping &&
           ((Keyboard.current!=null && Keyboard.current.fKey.wasPressedThisFrame) || 
            (Gamepad.current!=null && Gamepad.current.yButton.wasPressedThisFrame)))
        {
            StartCoroutine(FlipAsync());
            ObjectManager.Instance.OnFlipUnder();
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
            
            // color adjustment
            postProcessingVolume.weight = Mathf.Clamp01(elapsed / duration * (foward ? 1 : -1));
            
            //music pitch adjustment 1 to 0
            _audioSource.pitch = Mathf.Clamp01(1 - (elapsed / duration));
                
            // transform.Rotate(Vector3.forward, step);
            elapsed += Time.deltaTime;
            yield return null;
        }
        // Ensure final rotation is exact
        //make sure the plane is horizontal
        
        transform.eulerAngles = new Vector3((foward ? 180 : 0), transform.eulerAngles.y + (foward ? 180 : 0));
        transform.position = new Vector3(playerPos.x, 0, playerPos.z);
        
        postProcessingVolume.weight = foward ? 1 : 0;
        
        
        _upWorldCollider.enabled = true;
        _downWorldCollider.enabled = true;
        _flipping = false;
        Player.Instance.SwitchFlip(false);

        _audioSource.pitch = 1f;
        if (_isDown)
        {
            // postProcessingVolume.gameObject.SetActive(true);
            StartCoroutine(IncreaseSeconds());
            _isDown = false;

            _musicProgress = _audioSource.time;
            _audioSource.clip = UpMusic;
            _audioSource.time = _musicProgress % _audioSource.clip.length;
            _audioSource.Play();
        }
        else
        {
            // postProcessingVolume.gameObject.SetActive(false);
            StartCoroutine(DecreaseSeconds());
            _isDown = true;
            
            _musicProgress = _audioSource.time;
            _audioSource.clip = DownMusic;
            _audioSource.time = _musicProgress % _audioSource.clip.length;
            _audioSource.Play();
        }
    }
    
    IEnumerator DecreaseSeconds()
    {
        counter.color = Color.red;
        for (int i = 0; i < secondsDown; i++)
        {
            counter.text = (secondsDown - i).ToString();
            yield return new WaitForSeconds(1f);
        }
        counter.color = Color.grey;
        
        ObjectManager.Instance.OnFlipUp();
        StartCoroutine(FlipAsync(false));
    }

    IEnumerator IncreaseSeconds()
    {
        float interval = 2f / secondsDown;
        for (int i = 0; i < secondsDown; i++)
        {
            counter.text = (i + 1).ToString();
            yield return new WaitForSeconds(interval);
        }
        counter.color = Color.white;
    }
    
    
}
