using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Filtering;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class FishInfo : MonoBehaviour
{
    public int Index;
    public string Name;
    public string Description;
    public float escapetimer;
    public float distanceIncrease;
    public int howMuchTillBreak;
    public int worth;

    public bool catched;
    public bool done;

    public GameObject rod;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (done)
        {
            transform.parent = null;
        }
    }
    void OnTriggerEnter(Collider other)
    {
  


        if (!catched && !done)
        {
     
            if (other.CompareTag("Land"))
            {
                print("Catched");

                if (gameObject.transform.GetComponent<XRGrabInteractable>()) // enable grab
                {
                
                    gameObject.GetComponent<XRGrabInteractable>().enabled = true;
                }
                if (gameObject.transform.GetComponent<Rigidbody>()) /// enable grabbable
                {
               
                    gameObject.GetComponent<Rigidbody>().isKinematic = false;
                }
                if (rod != null && rod.GetComponent<RodHandler>())
                {

                    rod.GetComponent<RodHandler>().FishOnHook();
                }
                catched = true;
            }
        }
    }
    public void grabfish()
    {
        if (!done)
        {
            // Detach the fish from its current parent (if it was parented)
            transform.parent = null;
            print("Grabbed fish");

            // Reset the 'catched' state and update done flag
            catched = false;
            done = true;

            // Notify the rod to reset the fishing state (detach fish)
            if (rod != null && rod.GetComponent<RodHandler>())
            {
                rod.GetComponent<RodHandler>().TakenOffHook();  // This ensures the rod removes the fish from the bobber
            }
        }
    }

}
