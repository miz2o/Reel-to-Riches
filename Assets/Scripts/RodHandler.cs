using System.Collections;
using System.Collections.Generic;
using Unity.VRTemplate;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR.OpenXR.Input;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class RodHandler : MonoBehaviour
{
    [Header("Values That Get Changed in Script")]
    public bool lockthrow;
    public bool isstarted;
    public int throwIncrease;
    private bool isWaiting; // Wait for throw
    private bool isWaiting2; // Wait for fish
    private bool isWaiting3; // Fish escape
    public bool rodinwater;
    public bool isreeling;
    public bool fishtocatch;
    public float lastKnobValue;
    public GameObject currentFish;
    public bool bobberinwater;




    [Space]
    [Header("GameObjects")]
    public GameObject rod;
    public GameObject throwtopoint;
    public GameObject player;
    public GameObject bobber;
    public GameObject dynamicparent;
    public GameObject playercamera;
    public GameObject reeler;
    public TMP_Text EscapeCounter;





    [Space]
    [Header("Editable Values")]
    public Vector3 fishoncatchrotation;
    public int throwrotation;
    public Vector2 catchWait;
    public float minimumReel;
    public float distanceDecrease;
    public float howClose;


    [Space]
    [Header("Effects")]
    public AudioSource reelSFX;
    public AudioSource escapeSFX;

    [Space]
    [Header("Fish and their rarities")] // easier fish should go first
    public List<GameObject> fishPrefabs;
    public List<int> fishRaritys;



    [Space]
    [Header("Values from fish")]
    public float escapeTimer;
    public float distanceIncrease;
    public int howMuchTillBreak;

    void Start()
    {
  
    }

    void Update()
    {

        if (currentFish != null)
        {
            currentFish.transform.localPosition = Vector3.zero;
        }

        XRKnob knob = reeler.GetComponent<XRKnob>();
        if (knob != null)
        {
            float currentValue = knob.value;

            // Detect value change
            if (!Mathf.Approximately(currentValue, lastKnobValue))
            {
                if (lastKnobValue - currentValue >= minimumReel)
                {
                    OnKnobValueChanged(currentValue);
                }
                lastKnobValue = currentValue;

            }



            float xRotation = rod.transform.eulerAngles.x;

            if (xRotation > 180)
            {
                xRotation -= 360;
            }

            if (!rodinwater)
            {
                // Check rotation condition and ensure throwing is false
                if (xRotation <= throwrotation && !lockthrow)
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
                if (fishtocatch && !isWaiting3) // Trigger escape when fish is present and not already escaping
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
          throwtopoint.transform.position = player.transform.position;
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
        Vector3 startPosition = Vector3.zero; // Declare startPosition outside the condition for reuse

        if (throwIncrease == 0)
        {
            // Get the forward vector and project it onto the horizontal plane
            Vector3 forward = playercamera.transform.forward;
            forward.y = 0; // Remove the vertical component
            forward.Normalize(); // Ensure the vector is of unit length

            // Calculate the initial position
            startPosition = playercamera.transform.position + forward * howClose;

            // Set the initial position
            throwtopoint.transform.position = startPosition;

            // Set the initial rotation
            throwtopoint.transform.rotation = playercamera.transform.rotation;
        }

        isWaiting = true; // Set waiting flag
        yield return new WaitForSeconds(0.1f); // Wait for a short delay

        // Incrementally move throwtopoint forward from startPosition
        Vector3 forwardDirection = playercamera.transform.forward;
        forwardDirection.y = 0; // Remove vertical component
        forwardDirection.Normalize(); // Ensure it's normalized

        // Calculate the new position relative to the startPosition
        Vector3 newPosition = startPosition + forwardDirection * (throwIncrease * 0.1f);

        // Set the position and ensure it stays on the horizontal plane
        newPosition.y = 0;
        throwtopoint.transform.position = newPosition;

        // Update the rotation to match the camera
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
        if (bobberinwater)
        {
            PickFish();
            print("StartCatchingFish");
            fishtocatch = true;
            isWaiting2 = false;
            escapeSFX.Play();
        }
        else
        {
            print("bobbero n land, reset previous values");
            TakenOffHook();
        }
    }


    public void OnKnobValueChanged(float value)
    {

        if (fishtocatch)
        {

            if (!isreeling)
            {
                isreeling = true;
                reelSFX.Play();


                Vector3 forwardIncrement = playercamera.transform.forward * distanceDecrease;
                forwardIncrement.y = 0;
                throwtopoint.transform.position -= forwardIncrement;
                StartCoroutine(ResetReeling());
            }
        }
    }
    IEnumerator ResetReeling()
    {
        yield return new WaitForSeconds(0.03f); // Adjust the delay as needed
        isreeling = false;

    }

    IEnumerator EscapeDelay()
    {
        isWaiting3 = true; // Set waiting flag for escape

        yield return new WaitForSeconds(escapeTimer);
        Vector3 forwardIncrement = playercamera.transform.forward * distanceIncrease; // Move forward
        forwardIncrement.y = 0; // Ensure y stays at 0
        throwtopoint.transform.position += forwardIncrement;

        howMuchTillBreak -= 1;
        EscapeCounter.text = howMuchTillBreak.ToString();

        if (howMuchTillBreak <= 0)
        {
            FishBreakFree();
        }

        isWaiting3 = false; // Reset waiting flag
    }
    public void PickFish()
    {
        int totalWeight = 0;

        // Calculate the total weight of all fish
        foreach (int rarity in fishRaritys)
        {
            totalWeight += rarity;
        }

        // Generate a random number within the total weight
        int generatedNumber = Random.Range(0, totalWeight);
        Debug.Log($"Generated Number: {generatedNumber}, Total Weight: {totalWeight}");

        int cumulativeWeight = 0;
        int index = 0;

        // Iterate through fish and find the match
        foreach (int rarity in fishRaritys)
        {
            cumulativeWeight += rarity;

            if (generatedNumber < cumulativeWeight)
            {
                Debug.Log($"Chosen index: {index}, Rarity: {rarity}");
                currentFish = Instantiate(fishPrefabs[index], throwtopoint.transform);
                if (currentFish.GetComponent<FishInfo>())
                {
                    currentFish.GetComponent<FishInfo>().rod = gameObject;

                    escapeTimer = currentFish.GetComponent<FishInfo>().escapetimer;
                    distanceIncrease = currentFish.GetComponent<FishInfo>().distanceIncrease;
                    howMuchTillBreak = currentFish.GetComponent<FishInfo>().howMuchTillBreak;

                }
                currentFish.transform.localPosition = Vector3.zero;
                return;
            }

            index++;
        }

        // Fallback in case no fish is selected
        Debug.Log("No fish chosen. Defaulting to index 0.");
        currentFish = Instantiate(fishPrefabs[0], throwtopoint.transform);
        currentFish.transform.localPosition = Vector3.zero;
    }


    public void FishOnHook()
    {
        print("Fish is fished");
        lockthrow = true;
        fishtocatch = false;
        rodinwater = false;
        isstarted = false;
        if (currentFish != null)
        {
            currentFish.transform.parent = bobber.transform;
            currentFish.transform.eulerAngles = fishoncatchrotation;
        }

        if (dynamicparent.GetComponent<DynamicBone>() != null)
        {
            dynamicparent.GetComponent<DynamicBone>().enabled = true;
        }

    }

    public void TakenOffHook()
    {
        // Detach the current fish
        if (currentFish != null)
        {
            currentFish.transform.parent = null;
            currentFish = null;
        }

        // Reset fishing state variables
        lockthrow = false;
        isstarted = false;
        rodinwater = false;
        fishtocatch = false;
        throwIncrease = 0;

        // Reset the dynamic parent
        if (dynamicparent.GetComponent<DynamicBone>() != null)
        {
            dynamicparent.GetComponent<DynamicBone>().enabled = true;
        }

        // Stop any playing audio effects
        if (reelSFX.isPlaying)
        {
            reelSFX.Stop();
        }

        if (escapeSFX.isPlaying)
        {
            escapeSFX.Stop();
        }

        // Reset other flags
        isWaiting = false;
        isWaiting2 = false;
        isWaiting3 = false;

        print("Fishing state reset. Ready to fish again.");
    }

    public void FishBreakFree()
    {
        print("Fish too far, break free");
        Destroy(currentFish);
        TakenOffHook();
    }

}
