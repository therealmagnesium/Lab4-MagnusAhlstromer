using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constrain : MonoBehaviour
{
    private bool shouldDie = false;

    // Initialize components
    void Start()
    {
        
    }

    // Do these all the time
    void Update()
    {
        // Don't let the player fall off the stage
        if (transform.position.y < -5f)
            shouldDie = true;

        if (shouldDie)
        {
            Debug.Log($"{gameObject.tag} just died");
            Destroy(gameObject);
            shouldDie = false;
        }
    }
}
