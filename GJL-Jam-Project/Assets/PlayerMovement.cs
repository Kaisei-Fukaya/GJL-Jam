using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInput), typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public float baseMoveSpeed;
    public float maxMomentum;
    public float decelerationSpeed;
    public float jumpPower;
    public float jumpBonusAmount;
    public LayerMask groundMask;
    float _moveSpeed;
    float _momentumMultiplier;
    float _timeSinceGrounded;
    float _jumpBonus;
    bool _isGrounded;
    PlayerInput _pInput;
    Rigidbody _rb;
    [SerializeField] float _cameraTiltDegrees;

    private void Start()
    {
        _pInput = GetComponent<PlayerInput>();
        _rb = GetComponent<Rigidbody>();
        _moveSpeed = baseMoveSpeed;
        _momentumMultiplier = 1f;
    }

    private void FixedUpdate()
    {
        //Check grounded
        if(Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z), -transform.up, 0.2f, groundMask))
        {
            _isGrounded = true;
            _timeSinceGrounded += Time.deltaTime;
        }
        else
        {
            _isGrounded = false;
            _timeSinceGrounded = 0f;
        }

        //Handle movement
        Vector3 currentVelocity = _rb.velocity;
        currentVelocity.y = 0f;

        _moveSpeed = baseMoveSpeed * (_momentumMultiplier + _jumpBonus/10f);

        //Increase momentum until max speed, reset when not moving
        if (_pInput.IsPressingMovementKey && currentVelocity.magnitude > 0.1f)
        {
            if (_momentumMultiplier < maxMomentum)
            {
                _momentumMultiplier += 0.005f;

                float percentage = GetPercentageMomentum();                             //Get percentage momentum
                PlayerCamera.Instance.MomentumFOVShift(percentage);                     //Set camera fov based on momentum
                PostProcessingController.Instance.SetVignetteIntensity(percentage);     //Set vignette based on momentum
            }
            else
            {
                _momentumMultiplier = maxMomentum;
            }
        }
        else
        {
            _momentumMultiplier = Mathf.Lerp(_momentumMultiplier, 1f, Time.deltaTime * decelerationSpeed);
            float percentage = GetPercentageMomentum();
            PlayerCamera.Instance.MomentumFOVShift(percentage);
            PostProcessingController.Instance.SetVignetteIntensity(percentage);
        }

        //Tilt camera
        PlayerCamera.Instance.SetCameraTilt(_cameraTiltDegrees * -_pInput.MovementInput.x);

        //Handle Jump
        if (_pInput.JumpPressed && _isGrounded)
        {
            //Jump
            if(_timeSinceGrounded < 0.5f)
            {
                _jumpBonus += jumpBonusAmount;                                                                       //Bonus Added for jumping soon after landing
            }
            else
            {
                _jumpBonus = 0f;
            }
            _rb.AddForce(transform.up * (jumpPower + _jumpBonus), ForceMode.Impulse);             

            //Reset flag
            _pInput.ResetJump();
        }

        //Set velocity
        Vector3 horizontalMoveForce = transform.right *  _pInput.MovementInput.x * (_moveSpeed*0.8f);   //Horizontal speed is reduced for more natural feel
        Vector3 forwardMoveForce = transform.forward * _pInput.MovementInput.z * _moveSpeed;

        currentVelocity = horizontalMoveForce + forwardMoveForce;

        currentVelocity = Vector3.ClampMagnitude(currentVelocity, _moveSpeed);                          //Clamp magnitude so that moving diagonally is not faster
        currentVelocity.y = _rb.velocity.y;                                                             //Re-combine with vertical velocity

        _rb.velocity = currentVelocity;

    }

    float GetPercentageMomentum()
    {
        return Mathf.InverseLerp(1f, maxMomentum, _momentumMultiplier);
    }
}
