using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class PlayerAnimationController : MonoBehaviour
{
    PlayerInput _pInput;

    [SerializeField] Animator _anim;

    public static PlayerAnimationController Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        _pInput = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        _anim.SetFloat("movement", _pInput.MovementInput.magnitude);
    }

    public void SetAnimatorParameter(string param, float value)
    {
        _anim.SetFloat(param, value);
    }
    public void SetAnimatorParameter(string param, int value)
    {
        _anim.SetInteger(param, value);
    }
    public void SetAnimatorParameter(string param, bool value)
    {
        _anim.SetBool(param, value);
    }
    public void SetAnimatorTrigger(string param)
    {
        _anim.SetTrigger(param);
    }
}
