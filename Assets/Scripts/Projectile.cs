using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public void Hit() //method to be called in order to destroy projectile when it collides with something
    {
        Destroy(gameObject);
    }
}
