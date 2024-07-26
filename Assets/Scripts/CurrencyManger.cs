using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyManger : MonoBehaviour
{
    [SerializeField] Text CurrencyText = null;
    
    public int Currency = 100;

    void Start()
    {
        UpdateText();
    }

    void UpdateText()
    {
        if (Currency < 0)
        {
            Currency = 0;
            Debug.Log("how did you go NEGATIVE CASH???");
        }

        if (CurrencyText == null) return;

        CurrencyText.text = "$" + Currency.ToString();
    }

    public void IncreaseCurrency(int Amount)
    {
        Currency = Currency + Amount;
        UpdateText();
    }

    public void SetCurrency(int Amount)
    {
        Currency = Amount;
        UpdateText();
    }
}
