using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestroyer : MonoBehaviour
{
    public float lifeTime;
    public bool useLifeTime = true;

    void Update()
    {
        if (useLifeTime)
        {
            if(lifeTime > 0f)
            {
                lifeTime -= Time.deltaTime;
            }
            else
            {
                DestroyObject();
            }
        }
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }
}
