using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevTools : MonoBehaviour
{
    public GameObject cashHandlerParent;

    // List of objects to spawn
    public List<GameObject> objectsToSpawn;

    // Position where the objects will spawn
    public Vector3 spawnPosition;

    // Offset between spawned objects
    public Vector3 spawnOffset;

    // Method to add cash
    public void TryBuy()
    {
        print("Devtools started");
        CashHandler cashHandler = cashHandlerParent.GetComponent<CashHandler>();
        if (cashHandler != null)
        {
            cashHandler.cash += 999999;
           
        }
    }

    // Method to spawn all objects
    public void SpawnObjects()
    {
        for (int i = 0; i < objectsToSpawn.Count; i++)
        {
            // Check if the object in the list is valid
            if (objectsToSpawn[i] != null)
            {
                // Calculate the position for this object with the offset
                Vector3 currentSpawnPosition = spawnPosition + (spawnOffset * i);

                // Spawn the object at the calculated position
                Instantiate(objectsToSpawn[i], currentSpawnPosition, Quaternion.identity);
            }
        }
    }
}
