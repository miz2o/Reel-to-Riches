using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RodHandler : MonoBehaviour
{
    public bool throwing;
    public bool iscast;
    public GameObject rod;
    public GameObject throwtopoint;
    public GameObject player;
    public int ThrowIncrease;
    public Vector3 targetpos;
    private bool isWaiting;
    public GameObject bobber;

    void Start()
    {

    }

    void Update()
    {
        float xRotation = rod.transform.eulerAngles.x;

        if (xRotation > 180)
        {
            xRotation -= 360;
        }

        // Check rotation condition and ensure throwing is false
        if (xRotation <= -50 && !throwing)
        {
            print("StartThrow");
            iscast = true;

            // Initialize target position if ThrowIncrease is 0
            if (ThrowIncrease == 0)
            {
                targetpos = player.transform.position;
                throwtopoint.transform.eulerAngles = player.transform.eulerAngles;
            }

            // Increment ThrowIncrease using coroutine
            if (ThrowIncrease < 50 && !isWaiting)
            {
                StartCoroutine(IncrementThrowWithDelay());
            }

            print("ThrowIncrease: " + ThrowIncrease.ToString());

            throwtopoint.transform.position = targetpos;
        }
        else if (xRotation >= -10 && iscast)
        {
            // Additional logic for "casting" state
            print("Casting");
            bobber.transform.position = targetpos;

            StartCoroutine(WaitForFish());
        }
    }

    IEnumerator IncrementThrowWithDelay()
    {
        isWaiting = true; // Set waiting flag
        yield return new WaitForSeconds(0.1f); // Wait for a short delay

        // Update ThrowIncrease and target position
        targetpos.x += 0.1f;
        ThrowIncrease += 1;

        // Allow throwing only when ThrowIncrease is complete
        if (ThrowIncrease >= 50)
        {
            throwing = true;
        }

        isWaiting = false; // Reset waiting flag
    }

    IEnumerator WaitForFish()
    {
        yield return new WaitForSeconds(0.1f);
    }
}
