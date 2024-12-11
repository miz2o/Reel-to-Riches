using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobberCollision : MonoBehaviour
{

    public void OnTriggerEnter(Collider other)
    {
        // Check if the triggering object has the tag "water"
        if (other.CompareTag("Water"))
        {
            Debug.Log("Bobber entered the water!");
            // Add your logic here
        }
    }
}
