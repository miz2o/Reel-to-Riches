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

      rodToBuy.transform.position = storeSpawn.transform.position;
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
