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
    public float airDrag;
    public LayerMask groundMask;
    float _moveSpeed;
    float _momentumMultiplier;
    float _timeSinceLanded;
    float _jumpBonus;
    bool _isGrounded;
    bool _frameOfJumpFlag;
    Vector3 _lastVelocity;
    PlayerInput _pInput;
    Rigidbody _rb;
    [SerializeField] float _cameraTiltDegrees = 5f;

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
            _timeSinceLanded += Time.deltaTime;
        }
        else
        {
            _isGrounded = false;
            _timeSinceLanded = 0f;
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
            if(_timeSinceLanded < 0.1f)
            {
                _jumpBonus += jumpBonusAmount;                                                          //Bonus Added for jumping soon after landing
            }
            else
            {
                _jumpBonus = 0f;
            }
            _rb.AddForce(transform.up * (jumpPower + _jumpBonus), ForceMode.Impulse);             

            //Reset flag
            _pInput.ResetJump();

            //Flag jumped
            _frameOfJumpFlag = true;
        }

        //Set velocity
        Vector3 horizontalMoveForce = transform.right *  _pInput.MovementInput.x * (_moveSpeed*0.8f);   //Horizontal speed is reduced for more natural feel
        Vector3 forwardMoveForce = transform.forward * _pInput.MovementInput.z * _moveSpeed;

        currentVelocity = horizontalMoveForce + forwardMoveForce;

        currentVelocity = Vector3.ClampMagnitude(currentVelocity, _moveSpeed);                          //Clamp magnitude so that moving diagonally is not faster
        currentVelocity.y = _rb.velocity.y;                                                             //Re-combine with vertical velocity

        if (_isGrounded)
        {
            _rb.velocity = currentVelocity;
        }
        else if(!_frameOfJumpFlag)
        {
            currentVelocity.x = currentVelocity.x / 2;
            currentVelocity.z = currentVelocity.z / 2;
            _rb.velocity = _lastVelocity + currentVelocity;

            //Air drag
            _lastVelocity -= _lastVelocity * airDrag * Time.fixedDeltaTime;
        }
        else
        {
            _lastVelocity = currentVelocity;                                                    //store velocity just before jumping
            _lastVelocity.y = 0f;
            _frameOfJumpFlag = false;
        }
    }

    float GetPercentageMomentum()
    {
        return Mathf.InverseLerp(1f, maxMomentum, _momentumMultiplier);
    }
}
