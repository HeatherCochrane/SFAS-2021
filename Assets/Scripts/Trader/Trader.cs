using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Trader : MonoBehaviour
{
    //Slot variables needed
    public struct Slot
    {
        public Sprite image;
        public GameObject slotObject;
        public ScriptableObject objectData;
        public int price;

    }

    //Keep track of all the avaliable clots
    [SerializeField]
    List<Slot> slots = new List<Slot>();

    //keep track of whether to update the slots
    bool spawnedInventory = false;

    //Trader variables
    int totalMoney = 0;

    //The list of stock for each trader
    [SerializeField]
    List<Item> stock = new List<Item>();

    //Trader object within the scene
    GameObject traderInventory;

    //Where the slots are parented to for layouts
    GameObject slotParent;

    //The ui that contains the info needed for the player
    GameObject infoBox;

    //The active item
    Item activeItem;

    //Which slot is currently selected
    TraderSlot activeSlot;


    // Start is called before the first frame update
    void Start()
    {
        //Set up the gameobjects using the player instance
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
                //Set up the slot with the appropriate details given from the current trader 
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

    //Make sure the player cant move and the inventory is set up using the correct trader stock
    public void startTrading()
    {
        Player.instance.setMovement(true);
        Player.instance.inventory.setTrader(this);
        traderInventory.SetActive(true);
    }

    //Give the player back control and reset the trader and game objects
    public void stopTrading()
    {
        Player.instance.setMovement(false);
        Player.instance.inventory.setTrader(null);
        Player.instance.inventory.setInventory(false);
        Player.instance.setInventoryToggle(false);
        Player.instance.uiHandler.changeMenu(UIHandler.Menus.PLAYERUI);
    }

    public void buyItem()
    {
        if (activeItem != null)
        {
            //Chck that the player can afford the item and if so add this to their inventory depending on their type
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
                    //Healing items can be stacked
                    Player.instance.inventory.addItem(activeItem);
                    Player.instance.inventory.adjustFunds(-activeItem.buyPrice);
                }
            }
        }
    }

    //Set up the info box with the appropriate info given by the button selected
    public void showItemInfoBox(Sprite s, string name, string damage, string range, string price, TraderSlot slot, Item active)
    {
        infoBox.transform.GetChild(0).gameObject.SetActive(true);
        infoBox.transform.GetChild(0).GetComponent<Image>().sprite = s;
        infoBox.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = name;
        infoBox.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "Damage: " + damage;
        infoBox.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "Range: " + range;
        infoBox.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "Price: " + price;
        infoBox.transform.GetChild(5).transform.gameObject.SetActive(true);

        activeSlot = slot;
        activeItem = active;
    }

    //Clear the info box
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
