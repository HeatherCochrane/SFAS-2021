using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraderButton : MonoBehaviour
{
    Trader trader;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setTrader(Trader t)
    {
        trader = t;
    }

    public void buyItem()
    {
        trader.buyItem();
    }

    public void leave()
    {
        trader.stopTrading();
    }
}
