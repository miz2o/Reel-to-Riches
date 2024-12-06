using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishInfo : MonoBehaviour
{
    public int Index;
    public string Name;
    public string Description;
    public float escapetimer;
    public float distanceIncrease;
    public int worth;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Floor")
        {
            print("getfish");

        }
    }
}
