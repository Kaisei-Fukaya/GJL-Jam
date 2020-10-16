using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FlyingRobotAI : MonoBehaviour
{
    public Vector3 positionOffsets;
    public float moveSpeed, lookSpeed, timeBetweenShots;
    public float forceDeathThreshold;
    public float maxDistanceFromPlayer, detectionRange;
    public bool activatedOverride = true;
    bool activated = false;

    [SerializeField] GameObject _projectilePrefab;
    [SerializeField] GameObject _deathEffect;
    [SerializeField] GameObject _shotPoint;
    Vector3 _targetLocation;
    Vector3 _moveDir;
    Vector3 _lookDir;
    Rigidbody _rb;
    float _shotTimer;
    bool _inViewOfPlayer;
    bool _canFire;

    PlayerInput player;

    // Start is called before the first frame update
    void Start()
    {
        player = PlayerInput.Instance;
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _targetLocation = player.transform.position;
        LookTowardPlayer();
        if (_moveDir.magnitude > maxDistanceFromPlayer)
        {
            DestroyBot();
        }

        if (activated && activatedOverride && !UIManager.Instance.IsPaused)
        {
            MoveTowardPlayer();
            if (_inViewOfPlayer && _canFire)
            {
                Shoot();
                _canFire = false;
                _shotTimer = timeBetweenShots;
            }

            if (_shotTimer > 0f)
            {
                _shotTimer -= Time.fixedDeltaTime;
            }
            else
            {
                _shotTimer = 0f;
                _canFire = true;
            }
        }
        else
        {
            _rb.velocity = Vector3.zero;
            CheckForPlayer();
        }
    }

    void OnBecameVisible()
    {
        print("visible");
        _inViewOfPlayer = true;
    }

    void OnBecameInvisible()
    {
        _inViewOfPlayer = false;
    }

    void MoveTowardPlayer()
    {
        Vector3 relativeOffsets = player.transform.forward * positionOffsets.z;
        relativeOffsets.y = positionOffsets.y;
        _moveDir = (_targetLocation + relativeOffsets) - _rb.position;    /*Vector3.Lerp(transform.position, _targetLocation + relativeOffsets, moveSpeed * Time.fixedDeltaTime);*/
        Vector3 moveForce = _moveDir.normalized * (moveSpeed + _moveDir.magnitude) * Time.fixedDeltaTime;
        _rb.AddForce(moveForce, ForceMode.VelocityChange);
    }

    void LookTowardPlayer()
    {
        Vector3 lookTarget = _targetLocation;
        lookTarget.y += 1.5f;
        _lookDir = (lookTarget - transform.position).normalized;

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(_lookDir), lookSpeed * Time.fixedDeltaTime);
    }

    void CheckForPlayer()
    {
        var overlaps = Physics.OverlapSphere(transform.position, detectionRange);
        foreach (var o in overlaps)
        {
            if(o.tag == "Player")
            {
                activated = true;
            }
        }
    }

    void Shoot()
    {
        var newProjectile = Instantiate(_projectilePrefab);
        newProjectile.transform.rotation = transform.rotation;
        newProjectile.transform.position = _shotPoint.transform.position;
        print("shot");
    }

    private void OnCollisionEnter(Collision collision)
    {
        print(_rb.velocity.magnitude + " Collision!");
        if (collision.transform.tag == "Surface" && _rb.velocity.magnitude > forceDeathThreshold)
        {
            print(_rb.velocity.magnitude + " Destroyed!");
            //Smashed!
            DestroyBot();
        }
    }

    public void DestroyBot()
    {
        var dEffect = Instantiate(_deathEffect);
        dEffect.transform.position = transform.position;
        Destroy(gameObject);
    }
}
