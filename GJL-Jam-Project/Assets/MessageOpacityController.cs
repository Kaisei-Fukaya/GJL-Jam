using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class MessageOpacityController : MonoBehaviour
{
    public float opacityChangeSpeed = 0.5f;

    float _opacityTarget;
    TMP_Text _textComponent;


    private void Start()
    {
        _textComponent = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        if(_textComponent.alpha < _opacityTarget - 0.05f || _textComponent.alpha > _opacityTarget + 0.05f)
        {
            _textComponent.alpha = Mathf.Lerp(_textComponent.alpha, _opacityTarget, opacityChangeSpeed * Time.deltaTime);
        }
    }

    private void OnEnable()
    {
        _opacityTarget = 1f;
    }

    private void OnDisable()
    {
        _textComponent.alpha = 0f;
    }
}
