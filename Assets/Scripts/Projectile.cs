using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float schade = 100f;

    public float GetDamage()
    {
        return schade;
    }

    public void Hit()
    {
        Destroy(gameObject);
    }
}
