using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.Experimental.Rendering.Universal;

public class LanternController : MonoBehaviour
{
    public UnityEvent GrowLightEvent = new UnityEvent();
    public UnityEvent ReduceLightEvent = new UnityEvent();

    [SerializeField]
    private float _growScaleFactor;

    [SerializeField]
    private float _reduceScaleFactor;

    [SerializeField]
    private float _maxScaleFactor;
    
    [SerializeField]
    private float _minScaleFactor;

    [SerializeField]
    private Light2D _light;

    private float actualScale;

    void Start()
    {
        GrowLightEvent.AddListener(GrowLight);
        ReduceLightEvent.AddListener(ReduceLight);

        actualScale = _maxScaleFactor;
    }

    private void Update()
    {
        _light.pointLightInnerRadius= Mathf.Lerp( _light.pointLightInnerRadius,actualScale,0.5f*Time.deltaTime);
        _light.pointLightOuterRadius= Mathf.Lerp( _light.pointLightOuterRadius,actualScale+2,1*Time.deltaTime);
    }

    void GrowLight()
    {
        actualScale = Mathf.Clamp(actualScale+_growScaleFactor,_minScaleFactor,_maxScaleFactor);

    }
    
    void ReduceLight()
    {
        actualScale = Mathf.Clamp(actualScale-_reduceScaleFactor,_minScaleFactor,_maxScaleFactor);
    }
}
