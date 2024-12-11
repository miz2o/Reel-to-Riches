using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterRespawner : MonoBehaviour
{
    public Transform respawnpoint;
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water")) // Ensure case-sensitive tag check
        {
            print("Respawn player");

           gameObject.transform.position = respawnpoint.position;
            
        }
    }
}
