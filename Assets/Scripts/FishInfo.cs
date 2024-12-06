using System.Collections;
using System.Collections.Generic;
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
    public int worth;


    public GameObject rod;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Land"))
        {
            print("getfish");
            if (gameObject.GetComponent<XRGrabInteractable>()) // enable grab
            {
                gameObject.GetComponent<XRGrabInteractable>().enabled = true;
            }
            if (gameObject.GetComponent<Rigidbody>()) /// enable grabbable
            {
                gameObject.GetComponent<Rigidbody>().isKinematic = false;
            }
            if (rod != null && rod.GetComponent<RodHandler>())
            {
                rod.GetComponent<RodHandler>().FishOnHook();
            }
        }
    }
}
