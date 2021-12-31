using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    [SerializeField]
    private float _fadeTime;

    private Image _image;


    // Start is called before the first frame update
    void Start()
    {
        _image = GetComponent<Image>();
        _image.DOFade(0, _fadeTime);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
