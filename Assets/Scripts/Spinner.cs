using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    [SerializeField] float speedOfRotation = 300f;
    Rigidbody2D projectile;

    private void Start()
    {
        projectile = gameObject.GetComponent<Rigidbody2D>();
        projectile.angularVelocity = speedOfRotation;
    }
}
