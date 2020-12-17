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


    GameObject traderInventory;

    GameObject slotParent;


    GameObject infoBox;

    Item activeItem;

    TraderSlot activeSlot;


    // Start is called before the first frame update
    void Start()
    {
        slotParent = SceneLoader.instance.traderSlotParent;
        infoBox = SceneLoader.instance.traderInfoBox;
        
    }

    public void setTraderCanvas(GameObject c)
    {
        traderInventory = c;

        if (!spawnedInventory)
        {
            for (int i = 0; i < stock.Count; i++)
            {
                Slot newSlot = new Slot();
                newSlot.slotObject = slotParent.transform.GetChild(i).transform.gameObject;
                newSlot.slotObject.transform.GetChild(0).GetComponent<Image>().sprite = stock[i].itemSprite;
                newSlot.slotObject.GetComponent<TraderSlot>().setTrader(this);
                newSlot.slotObject.GetComponent<TraderSlot>().setButtonData(stock[i]);
                newSlot.price = stock[i].buyPrice;
                slots.Add(newSlot);
            }

            spawnedInventory = true;
        }

        traderInventory.SetActive(false);
        emptyInfoBox();

        startTrading();
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
        Player.instance.uiHandler.changeMenu(UIHandler.Menus.PLAYERUI);
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
                }
                else if(activeItem.GetType() == typeof(Item))
                {
                    Player.instance.inventory.addItem(activeItem);
                    Player.instance.inventory.adjustFunds(-activeItem.buyPrice);
                }
                else if (activeItem.GetType() == typeof(Healing))
                {
                    Player.instance.inventory.addItem(activeItem);
                    Player.instance.inventory.adjustFunds(-activeItem.buyPrice);
                }
            }
        }
    }


    public void showItemInfoBox(Sprite s, string name, string damage, string range, string price, TraderSlot slot, Item active)
    {
        infoBox.transform.GetChild(0).gameObject.SetActive(true);
        infoBox.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = name;
        infoBox.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "Damage: " + damage;
        infoBox.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "Range: " + range;
        infoBox.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "Price: " + price;
        infoBox.transform.GetChild(5).transform.gameObject.SetActive(true);

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
        infoBox.transform.GetChild(5).transform.gameObject.SetActive(false);

        activeSlot = null;
        activeItem = null;
    }

}
