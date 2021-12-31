using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraSpeedController : MonoBehaviour
{
    public UnityEvent IncreaseSpeed=new UnityEvent();
    public UnityEvent DecreaseSpeed=new UnityEvent();
    
    public bool Running = false;
    
    [SerializeField]
    private float _incrementFactor;
    
    [SerializeField]
    private float _decrementFactor;
    
    [SerializeField]
    private float _currentSpeed;
    
    [SerializeField]
    private float _maxSpeed;
    
    [SerializeField]
    private float _minSpeed;

    // Start is called before the first frame update
    void Start()
    {
        IncreaseSpeed.AddListener(Increase);
        DecreaseSpeed.AddListener(Decrease);
        
    }

    private void Increase()
    {
        _currentSpeed = Mathf.Clamp(_currentSpeed+_incrementFactor,_maxSpeed,_minSpeed);
    }
    
    private void Decrease()
    {
        _currentSpeed = _minSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (Running)
        {
            transform.Translate(0,_currentSpeed*Time.deltaTime,0);  
        }
    }
}
