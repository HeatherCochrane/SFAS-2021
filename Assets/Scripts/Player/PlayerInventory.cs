using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerInventory : MonoBehaviour
{
    public struct Slot
    {
        public Sprite image;
        public bool isTaken;
        public GameObject slotObject;
        public ScriptableObject objectData;
        public int amount;
    }

    [SerializeField]
    List<Slot> slots = new List<Slot>();

    [SerializeField]
    List<Item> inventoryItems = new List<Item>();

    [SerializeField]
    GameObject inventory;

    [SerializeField]
    GameObject slotParent;

    [SerializeField]
    GameObject infoBox;

    InventorySlot activeSlot;

    [SerializeField]
    TextMeshProUGUI funds;

    bool spawnedInventory = false;

    public int playerFunds = 100;

    Trader currentTrader;
    private void Start()
    {
        if(!spawnedInventory)
        {
            for(int i =0; i < 9; i++)
            {
                Slot newSlot = new Slot();
                newSlot.slotObject = slotParent.transform.GetChild(i).transform.gameObject;
                newSlot.isTaken = false;
                newSlot.amount = 0;
                slots.Add(newSlot);
            }

            spawnedInventory = true;
        }

        inventory.SetActive(false);
    }

    private void FixedUpdate()
    {
        funds.text = playerFunds.ToString();
    }

    public void adjustFunds(int amount)
    {
        playerFunds += amount;
    }

    public bool checkFunds(int amount)
    {
        if(playerFunds - amount >= 0)
        {
            return true;
        }

        return false;
    }
    public bool showInventory()
    {
        if (inventory.activeSelf)
        {
            inventory.SetActive(false);
            return false;
        }
        else
        {
            inventory.SetActive(true);
            emptyInfoBox();
            return true;
        }
    }

    public void setInventory(bool set)
    {
        inventory.SetActive(set);

        if(set)
        {
            emptyInfoBox();
        }
    }

    void updateUI(Item item)
    {
        for(int i =0; i < slots.Count; i++)
        {
            if(!slots[i].isTaken)
            {
                Slot newSlot;
                newSlot = slots[i];
                newSlot.slotObject.GetComponent<Image>().sprite = item.itemSprite;
                newSlot.isTaken = true;
                newSlot.slotObject.GetComponent<InventorySlot>().setButtonData(item);
                newSlot.slotObject.GetComponent<InventorySlot>().setSlotPos(i);
                slots[i] = newSlot;

                break;
            }
        }
    }

    void refreshUI()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            Slot newSlot;
            newSlot = slots[i];
            newSlot.slotObject.GetComponent<Image>().sprite = null;
            newSlot.isTaken = false;
            newSlot.objectData = null;
            newSlot.amount = 0;
            newSlot.slotObject.GetComponent<InventorySlot>().setSlotPos(0);
            slots[i] = newSlot;
        }

        for(int i =0; i < inventoryItems.Count; i++)
        {
            Slot newSlot;
            newSlot = slots[i];
            newSlot.slotObject.GetComponent<Image>().sprite = inventoryItems[i].itemSprite;
            newSlot.isTaken = true;
            newSlot.slotObject.GetComponent<InventorySlot>().setButtonData(inventoryItems[i]);
            newSlot.slotObject.GetComponent<InventorySlot>().setSlotPos(i);
            newSlot.amount = slots[i].amount;
            slots[i] = newSlot;
        }
    }

    public void addWeapon(Item t)
    {   
        inventoryItems.Add(t);
        updateUI(t);
    }

    public void removeItem(Item t)
    {
        inventoryItems.Remove(t);
        refreshUI();
    }
    
    public bool checkInventorySpace()
    {
        if(!slots[slots.Count - 1].isTaken)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool checkIfStackable(Item i)
    {
        foreach(Item item in inventoryItems)
        {
            if(item == i)
            {
                return true;
            }
        }

        return false;
    }

    public void addStackable(Item i)
    {
        foreach (Item item in inventoryItems)
        {
            if (item == i)
            {
                Debug.Log(item + " " + i);

                Slot newSlot = slots[item.getSlotPos()];
                newSlot.amount += 1;
                slots[item.getSlotPos()] = newSlot;

                Debug.Log("Position: " + item.getSlotPos());
            }
        }
    }

    public void showWeaponInfoBox(Sprite s, string name, string damage, string range, string price, InventorySlot slot)
    {
        infoBox.transform.GetChild(0).gameObject.SetActive(true);
        infoBox.transform.GetChild(0).GetComponent<Image>().sprite = s;
        infoBox.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = name;
        infoBox.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = damage;
        infoBox.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = range;
        infoBox.transform.GetChild(4).gameObject.SetActive(true);
        infoBox.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "Sell Price: " + price;

        if (currentTrader != null)
        {
            infoBox.transform.GetChild(5).gameObject.SetActive(false);
            infoBox.transform.GetChild(6).gameObject.SetActive(false);
            infoBox.transform.GetChild(7).gameObject.SetActive(true);
        }
        else
        {
            infoBox.transform.GetChild(5).gameObject.SetActive(true);
            infoBox.transform.GetChild(6).gameObject.SetActive(true);
            infoBox.transform.GetChild(7).gameObject.SetActive(false);
        }

        activeSlot = slot;
    }

    public void emptyInfoBox()
    {
        infoBox.transform.GetChild(0).gameObject.SetActive(false);
        infoBox.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "";
        infoBox.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "";
        infoBox.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "";
        infoBox.transform.GetChild(4).gameObject.SetActive(false);
        infoBox.transform.GetChild(5).gameObject.SetActive(false);
        infoBox.transform.GetChild(6).gameObject.SetActive(false);
        activeSlot = null;
    }

    public void dropItem()
    {
        if(activeSlot != null)
        {
            
            activeSlot.dropItem();
        }
    }

    public void equipItem()
    {
        if(activeSlot != null)
        {
            activeSlot.equipItem();
            emptyInfoBox();
        }
    }

    public void setTrader(Trader t)
    {
        currentTrader = t;
    }

    public void sellItem()
    {
        if(currentTrader != null && activeSlot != null)
        {
            playerFunds += activeSlot.getItem().sellPrice;
            removeItem(activeSlot.getItem());
            Debug.Log(playerFunds);
            emptyInfoBox();

            //Check to see if the player has sold their equipped item
            Player.instance.weapons.checkWeapon(inventoryItems);
        }
    }
}
