using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CashHandler : MonoBehaviour
{

    public int cash;
    public TMP_Text Cashdisplay;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Cashdisplay.text = cash.ToString() + "$";
    }
}
