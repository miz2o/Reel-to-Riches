using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRespawner : MonoBehaviour
{
    public GameObject respawnpoint;
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Water")
        {
            transform.position = respawnpoint.transform.position;
            print("collision respawn");
        }
    }
}
