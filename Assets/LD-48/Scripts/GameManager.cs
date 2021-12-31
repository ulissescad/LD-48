using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityStandardAssets._2D;
using UnityStandardAssets.Cameras;

public class GameManager : MonoBehaviour
{

    public UnityEvent EndGame=new UnityEvent();
    
    [SerializeField]
    private Platformer2DUserControl _playerControl;
    
    [SerializeField]
    private LanternController _lantern;
    
    [SerializeField]
    private CameraSpeedController _cameraController;

    [SerializeField]
    private DialogueController _dialogueController;
    
    [SerializeField]
    private float _decreaseLightScale;

    [SerializeField]
    private GameObject[] _platformPrefabs = new GameObject[0];
    
    [SerializeField]
    private GameObject _lastPlatformPrefab;

    [SerializeField]
    private Vector3 _offset;

    [SerializeField]
    private GameObject _lastPlatform;

    [SerializeField]
    private AudioSource _audioSource;
    
    [SerializeField]
    private AudioClip [] _badAudios = new AudioClip[0];

    [SerializeField]
    private AudioClip _collectitem;
    
    [SerializeField]
    private AudioClip _lastLantern;
    
    [SerializeField]
    private AudioClip _winMusic;
    
    [SerializeField]
    private float _maxLanterns;

    private float actualLanterns = 0;

    private bool canSpawn = true;
    
    private bool canDecrease = false;

    private float lastTime;
    
    public static GameManager SINGLETON;

    // Start is called before the first frame update
    void Start()
    {
        SINGLETON = this;
        EndGame.AddListener(FinishGame);
    }

    private void FinishGame()
    {
        canDecrease = false;
        _cameraController.Running = false;
        _dialogueController.Clear();
        _dialogueController.Gameover();
        Camera.main.GetComponentInParent<AbstractTargetFollower>().m_Target = _playerControl.CameraPosition;
        
        _cameraController.DecreaseSpeed.Invoke();
        _lantern.GrowLightEvent.Invoke();
        //_dialogueController.CreateGoodDialogue.Invoke();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (lastTime < Time.time && canDecrease==true)
        {
            lastTime = Time.time + _decreaseLightScale;
            DecreaseLight();
        }
    }

    public void ItemCollected()
    {
        lastTime = Time.time + _decreaseLightScale * 1f;
        _cameraController.DecreaseSpeed.Invoke();
        _lantern.GrowLightEvent.Invoke();
        _dialogueController.CreateGoodDialogue.Invoke();
        
        _audioSource.PlayOneShot(_collectitem);
        
        actualLanterns++;

        if (actualLanterns == _maxLanterns)
        {
            _audioSource.PlayOneShot(_lastLantern);
            SpawnLastPlatform();
        }
    }

    public void DecreaseLight()
    {
        if (canDecrease==false)
            return;
        
        _cameraController.IncreaseSpeed.Invoke();
        _lantern.ReduceLightEvent.Invoke();
        _dialogueController.CreateBadDialogue.Invoke();
        _audioSource.PlayOneShot(_badAudios[Random.Range(0,_badAudios.Length)]);
    }

    public void SpawnPlatform()
    {
        if (canSpawn==false)
            return;
        
        var platform = Instantiate(_platformPrefabs[Random.Range(0,_platformPrefabs.Length)], _lastPlatform.transform.position - _offset,transform.rotation);
        _lastPlatform = platform;
    }
    
    public void SpawnLastPlatform()
    {
        canSpawn = false;
        var platform = Instantiate(_lastPlatformPrefab, _lastPlatform.transform.position - _offset,transform.rotation);
        _lastPlatform = platform;
        _playerControl.GlowFinal();
        _audioSource.PlayOneShot(_lastLantern);
        _audioSource.volume = 0;
        _audioSource.clip = _winMusic;
        _audioSource.Play();
        _audioSource.DOFade(1, 2f);
    }

    public void IntroOver()
    {
        //InvokeRepeating("DecreaseLight",_decreaseLightScale,_decreaseLightScale);
        _cameraController.Running = true;
        _playerControl.Running = true;
        canDecrease = true;
    }
}
