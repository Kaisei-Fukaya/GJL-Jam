using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float damage;
    public float maxTimeAlive;

    float _timeSinceCreation = 0f;


    private void FixedUpdate()
    {
        transform.Translate(Vector3.forward * speed * Time.fixedDeltaTime);

        _timeSinceCreation += Time.fixedDeltaTime;

        if(_timeSinceCreation > maxTimeAlive)
        {
            Destroy(gameObject);
        }
    }

    public void HitPlayer()
    {
        PlayerData.Instance.ReduceBattery(damage);
        Destroy(gameObject);
    }

    public void HitOther()
    {
        print("hit other");
        Destroy(gameObject);
    }
}
