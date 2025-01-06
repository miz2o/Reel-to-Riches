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




    // Start is called before the first frame update
    public void trybuy()
    {
        print("TryBuy");
        CashHandler cashHandler = cashHandlerParent.GetComponent<CashHandler>();

        if (cashHandler != null)
        {
            if (cashHandler.cash >= price) // Check if there's enough money
            {
                cashHandler.cash = cashHandler.cash - price;
                buytext.text = "Bought";

                if (rodToBuy.GetComponent<XRGrabInteractable>() != null && rodToBuy.GetComponent<RodHandler>() != null)
                {
                    rodToBuy.GetComponent<XRGrabInteractable>().enabled = true;
                    rodToBuy.GetComponent<RodHandler>().enabled = true;
                }
            }
            else
            {
                buytext.text = "Not enough money";
            }
        }
    }
}
