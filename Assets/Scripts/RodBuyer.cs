using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Filtering;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class RodBuyer : MonoBehaviour
{
    public GameObject rodToBuy;
    public int price;
    public GameObject cashHandlerParent;
    public TMP_Text buytext;
    public GameObject storeSpawn;

    public void trybuy()
    {
        print("TryBuy");
        CashHandler cashHandler = cashHandlerParent.GetComponent<CashHandler>();

        if (cashHandler != null)
        {
            if (cashHandler.cash >= price) // Check if there's enough money
            {
                cashHandler.cash -= price;
                buytext.text = "Bought";

                // Check if rodToBuy and storeSpawn are set
                if (rodToBuy != null && storeSpawn != null)
                {
                    // Instantiate the rodToBuy at the storeSpawn location and rotation
                    GameObject newRod = Instantiate(rodToBuy, storeSpawn.transform.position, storeSpawn.transform.rotation);

                    // Check if the new rod has a Rigidbody and set isKinematic to false
                    Rigidbody rodRigidbody = newRod.GetComponent<Rigidbody>();
                    if (rodRigidbody != null)
                    {
                        rodRigidbody.isKinematic = false;
                    }
                }
                else
                {
                    Debug.LogWarning("Rod or Spawn location is not set!");
                }
            }
            else
            {
                buytext.text = "Not enough money";
            }
        }
        else
        {
            Debug.LogError("CashHandler not found!");
        }
    }
}
