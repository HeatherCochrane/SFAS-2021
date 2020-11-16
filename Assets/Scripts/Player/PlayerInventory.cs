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
                slots.Add(newSlot);
            }

            spawnedInventory = true;
        }

        inventory.SetActive(false);
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
            slots[i] = newSlot;
        }

        for(int i =0; i < inventoryItems.Count; i++)
        {
            Slot newSlot;
            newSlot = slots[i];
            newSlot.slotObject.GetComponent<Image>().sprite = inventoryItems[i].itemSprite;
            newSlot.isTaken = true;
            newSlot.slotObject.GetComponent<InventorySlot>().setButtonData(inventoryItems[i]);
            slots[i] = newSlot;
        }
    }

    public void addWeapon(Item t)
    {   
        inventoryItems.Add(t);
        updateUI(t);

        Debug.Log("Weapon bought!");
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

    public void showWeaponInfoBox(Sprite s, string name, string damage, string range, InventorySlot slot)
    {
        infoBox.transform.GetChild(0).gameObject.SetActive(true);
        infoBox.transform.GetChild(0).GetComponent<Image>().sprite = s;
        infoBox.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = name;
        infoBox.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "Damage: " + damage;
        infoBox.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "Range: " + range;
        infoBox.transform.GetChild(4).gameObject.SetActive(true);
        infoBox.transform.GetChild(5).gameObject.SetActive(true);

        if (currentTrader != null)
        {
            infoBox.transform.GetChild(6).gameObject.SetActive(true);
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
            playerFunds += activeSlot.getItem().price;
            removeItem(activeSlot.getItem());
            Debug.Log("Sold Item!");
            Debug.Log(playerFunds);
        }
    }
}
