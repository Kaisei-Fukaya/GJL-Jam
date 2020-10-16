using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatCell : MonoBehaviour
{
    public float replenishValue;

    [SerializeField] GameObject _effectOnPickup;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            Use();
        }
    }

    void Use()
    {
        PlayerData.Instance.IncreaseBattery(replenishValue);
        var effect = Instantiate(_effectOnPickup);
        effect.transform.position = transform.position;
        Destroy(gameObject);
    }
}
