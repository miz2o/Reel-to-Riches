using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RodHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public bool throwing;
    public bool cast;
        public GameObject rod;
    public GameObject throwtopoint;
    public GameObject player;
    public float ThrowIncrease;
    public Vector3 targetpos;
    void Start()
    {
 
    }

    // Update is called once per frame
    void Update()
    {
        float xRotation = rod.transform.eulerAngles.x;

        if (xRotation > 180)
        {
            xRotation -= 360;
        }

      
        if (xRotation <= -50 & throwing == false)
        {
            print("StartThrow");
                throwing = true;
            cast = false;
            throwtopoint.transform.position = player.transform.position;
            throwtopoint.transform.eulerAngles = player.transform.eulerAngles;

            throwtopoint.transform.position.x += 0.5f;
        }
        else if (throwing == true && xRotation <= -10)
        {
            print("Casting");
        }
    }
}
