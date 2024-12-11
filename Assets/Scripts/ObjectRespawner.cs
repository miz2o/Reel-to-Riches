using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRespawner : MonoBehaviour
{
    public Transform respawnpoint;
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Water")
        {
            transform.position = respawnpoint.position;
            print("collision respawn");
        }
    }
}
