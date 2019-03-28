using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShreder : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision) //to prevent projectiles fly forever in space
    {
        Destroy(collision.gameObject);
    }
}
