using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RodHandler : MonoBehaviour
{
     [Header("values that get changed in script")]
    public bool lockthrow;
    public bool isstarted;
    public int throwIncrease;
    private bool isWaiting;//wait for throw
    private bool isWaiting2;//wait for fish
    private bool isWaiting3;//fish escape
    public bool rodinwater;
    public bool isreeling;
    public bool fishtocatch;

    [Space]
    [Header("gameobjects")]
    public GameObject rod;
    public GameObject throwtopoint;
    public GameObject player;
    public GameObject bobber;
    public GameObject dynamicparent;
    public GameObject playercamera;
    [Space]
    [Header("editable values")]
    public int throwrotation;
    public Vector2 catchWait;

    [Space]
    [Header("Effects")]
    public AudioSource reelSFX;
    public AudioSource escapeSFX;







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

        if (!rodinwater)
        {
            // Check rotation condition and ensure throwing is false
            if (xRotation <= -60 && !lockthrow)   //// Calc distance
            {
                StartThrow();

            }
            else if (xRotation >= -10 && isstarted)
            {
                ThrowRod();
            }
        }
        else
        {
            bobber.transform.position = throwtopoint.transform.position;
            if (fishtocatch)
            {
                if (!isWaiting3)
                {
                    StartCoroutine(EscapeDelay());
                }
            }
        }
    }


    private void StartThrow()
    {
     
        isstarted = true;

        // Initialize target position if ThrowIncrease is 0

        if (throwIncrease == 0)
        {

            Vector3 frontPosition = playercamera.transform.position + playercamera.transform.forward * 0.1f;
            throwtopoint.transform.position = new Vector3(frontPosition.x, 0, frontPosition.z);

        }
       

        // Increment ThrowIncrease using coroutine
        if (throwIncrease < 50 && !isWaiting)
        {
            StartCoroutine(IncrementThrowWithDelay());
        }

     

    }

    private void ThrowRod()
    {
        // Additional logic for "casting" state
        print("Casting");
        if (dynamicparent.GetComponent<DynamicBone>() != null)
        {

            dynamicparent.GetComponent<DynamicBone>().enabled = false;
        }
        bobber.transform.position = throwtopoint.transform.position;
        rodinwater = true;
        if (!isWaiting2 && !fishtocatch)
        {
            StartCoroutine(WaitForFish());
        }
    }



    IEnumerator IncrementThrowWithDelay()
    {
        isWaiting = true; // Set waiting flag
        yield return new WaitForSeconds(0.1f); // Wait for a short delay

 
        Vector3 forwardIncrement = playercamera.transform.forward * 0.1f; // Move 0.1 units forward
        forwardIncrement.y = 0; // Ensure y stays at 0
        throwtopoint.transform.position += forwardIncrement; // Add the forward increment to the current position


        Vector3 position = throwtopoint.transform.position;
        position.y = 0;
        throwtopoint.transform.position = position;

        throwtopoint.transform.rotation = playercamera.transform.rotation;


        throwIncrease += 1;

 
        if (throwIncrease >= 50)
        {
            lockthrow = true;
        }

        isWaiting = false; // Reset waiting flag
    }

    IEnumerator WaitForFish()
    {
        isWaiting2 = true;
        float waitTime = UnityEngine.Random.Range(catchWait.x, catchWait.y);
        yield return new WaitForSeconds(waitTime);

        print("StartCatchingFish");
        fishtocatch = true;
        isWaiting2 = false;
        escapeSFX.Play();
    }

  void ReelRequest()
  {
       if (fishtocatch)
        {
            if (!isreeling)
            {
                isreeling = true;
                reelSFX.Play();
            }

            Vector3 forwardIncrement = playercamera.transform.forward * 0.1f; // Move 0.1 units forward
            forwardIncrement.y = 0; // Ensure y stays at 0
            throwtopoint.transform.position -= forwardIncrement;
            isWaiting3 = false;
        }

  }     
    {
        isWaiting3 = true;
      
        yield return new WaitForSeconds(0.1f);
     
        Vector3 forwardIncrement = playercamera.transform.forward * 0.1f; // Move 0.1 units forward
        forwardIncrement.y = 0; // Ensure y stays at 0
        throwtopoint.transform.position += forwardIncrement;
        isWaiting3 = false;
    }

}
