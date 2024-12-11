using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobberCollision : MonoBehaviour
{
    public GameObject rod;
    public void OnTriggerEnter(Collider other)
    {
        if (rod.GetComponent<RodHandler>())
        {
            RodHandler rodhandler = rod.GetComponent<RodHandler>();
            // Check if the triggering object has the tag "water"
            if (other.CompareTag("Water"))
            {
                Debug.Log("Bobber entered the water!");
                rodhandler.bobberinwater = true;
            }
            if (other.CompareTag("Land"))
            {
                Debug.Log("Bobber is on land!");
                rodhandler.bobberinwater = false;
            }
        }
    }
}
