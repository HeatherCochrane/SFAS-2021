using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Trader : MonoBehaviour
{
    public struct Slot
    {
        public Sprite image;
        public GameObject slotObject;
        public ScriptableObject objectData;
        public int price;

    }

    [SerializeField]
    List<Slot> slots = new List<Slot>();

    bool spawnedInventory = false;


    //Trader variables
    int totalMoney = 0;

    [SerializeField]
    List<Item> stock = new List<Item>();


    [SerializeField]
    GameObject traderInventory;

    [SerializeField]
    GameObject slotParent;

    [SerializeField]
    GameObject infoBox;

    Item activeItem;
    TraderSlot activeSlot;

    // Start is called before the first frame update
    void Start()
    {
        if (!spawnedInventory)
        {
            for (int i = 0; i < stock.Count; i++)
            {
                Slot newSlot = new Slot();
                newSlot.slotObject = slotParent.transform.GetChild(i).transform.gameObject;
                newSlot.slotObject.GetComponent<Image>().sprite = stock[i].itemSprite;
                newSlot.slotObject.GetComponent<TraderSlot>().setButtonData(stock[i]);
                newSlot.price = stock[i].buyPrice;
                slots.Add(newSlot);
            }

            spawnedInventory = true;
        }

        traderInventory.SetActive(false);

    }

    public void startTrading()
    {
        Player.instance.setMovement(true);
        Player.instance.inventory.setTrader(this);
        traderInventory.SetActive(true);
    }

    public void stopTrading()
    {
        Player.instance.setMovement(false);
        Player.instance.inventory.setTrader(null);
        Player.instance.inventory.setInventory(false);
        Player.instance.setInventoryToggle(false);
        traderInventory.SetActive(false);
    }

    public void addMoney(int amount)
    {
        totalMoney += amount;
    }

    public void takeMoney(int amount)
    {
        totalMoney -= amount;
    }

    public bool checkTraderFunds(int amount)
    {
        if(totalMoney - amount >= 0)
        {
            return true;
        }

        return false;
    }

    public void buyItem()
    {
        if (activeItem != null)
        {
            if (Player.instance.inventory.checkFunds(activeItem.buyPrice) && Player.instance.inventory.checkInventorySpace())
            {
                Player.instance.inventory.setTrader(this);

                if (activeItem.GetType() == typeof(Weapon))
                {
                    Player.instance.inventory.addWeapon(activeItem);
                    Player.instance.inventory.adjustFunds(-activeItem.buyPrice);
                    emptyInfoBox();
                }
            }
        }
    }

    public void showWeaponInfoBox(Sprite s, string name, string damage, string range, string price, TraderSlot slot, Item active)
    {
        infoBox.transform.GetChild(0).gameObject.SetActive(true);
        infoBox.transform.GetChild(0).GetComponent<Image>().sprite = s;
        infoBox.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = name;
        infoBox.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "Damage: " + damage;
        infoBox.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "Range: " + range;
        infoBox.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "Price: " + price;

        activeSlot = slot;
        activeItem = active;
    }

    public void emptyInfoBox()
    {
        infoBox.transform.GetChild(0).gameObject.SetActive(false);
        infoBox.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "";
        infoBox.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "";
        infoBox.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "";
        infoBox.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "";

        activeSlot = null;
        activeItem = null;
    }

}
