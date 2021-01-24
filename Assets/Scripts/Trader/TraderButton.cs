using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraderButton : MonoBehaviour
{
    Trader trader;

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
