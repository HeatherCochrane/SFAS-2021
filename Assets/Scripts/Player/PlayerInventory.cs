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

    [System.Serializable]
    public struct InventoryItem
    {
        public Item item;
        public int amount;
    }

    [SerializeField]
    List<Slot> slots = new List<Slot>();

    [SerializeField]
    List<InventoryItem> inventoryItems = new List<InventoryItem>();

    [SerializeField]
    GameObject inventory;

    [SerializeField]
    GameObject slotParent;

    [SerializeField]
    GameObject infoBox;

    [SerializeField]
    GameObject infoBoxButtons;

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

    public void setActiveSlot(InventorySlot s)
    {
        activeSlot = s;
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
                newSlot.slotObject.GetComponent<InventorySlot>().amount = inventoryItems[i].amount;
                newSlot.slotObject.GetComponent<InventorySlot>().updateStackedUI();
                slots[i] = newSlot;

                break;
            }
        }
    }

    void refreshUI()
    {
        resetSlots();

        if (inventoryItems.Count > 0)
        {
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                Slot newSlot;
                newSlot = slots[i];
                newSlot.slotObject.GetComponent<Image>().sprite = inventoryItems[i].item.itemSprite;
                newSlot.isTaken = true;
                newSlot.slotObject.GetComponent<InventorySlot>().setButtonData(inventoryItems[i].item);
                newSlot.slotObject.GetComponent<InventorySlot>().setSlotPos(i);
                newSlot.slotObject.GetComponent<InventorySlot>().amount = inventoryItems[i].amount;
                newSlot.slotObject.GetComponent<InventorySlot>().updateStackedUI();
                slots[i] = newSlot;
            }
        }
    }

    void resetSlots()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            Slot newSlot;
            newSlot = slots[i];
            newSlot.image = null;
            newSlot.isTaken = false;
            newSlot.objectData = null;
            newSlot.slotObject = slots[i].slotObject;
            newSlot.slotObject.GetComponent<Image>().sprite = null;
            newSlot.slotObject.GetComponent<InventorySlot>().amount = 0;
            newSlot.slotObject.GetComponent<InventorySlot>().updateStackedUI();
            newSlot.slotObject.GetComponent<InventorySlot>().setButtonData(null);
            slots[i] = newSlot;
        }

    }

    public void addWeapon(Item t)
    {
        InventoryItem newInventory = new InventoryItem();
        newInventory.item = t;
        newInventory.amount = 1;
        inventoryItems.Add(newInventory);
        updateUI(t);
    }

    public void removeItem(Item t)
    {
        int blah = inventoryItems.FindIndex(x => x.item == t);
        inventoryItems.RemoveAt(blah);
    }

    public void addItem(Item t)
    {
        InventoryItem newInventory = new InventoryItem();
        newInventory.item = t;
        newInventory.amount = 1;
        inventoryItems.Add(newInventory);
        updateUI(t);
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
        foreach(InventoryItem item in inventoryItems)
        {
            if(item.item == i)
            {
                return true;
            }
        }

        return false;
    }

    public void addStackable(Item j)
    {
      
        for(int i = 0; i < inventoryItems.Count; i++)
        {
            if (inventoryItems[i].item == j)
            {
                Slot newSlot = slots[inventoryItems[i].item.getSlotPos()];
                InventoryItem t = inventoryItems[i];
                t.amount += 1;
                inventoryItems[i] = t;
                newSlot.slotObject.GetComponent<InventorySlot>().amount = inventoryItems[i].amount;
                newSlot.slotObject.GetComponent<InventorySlot>().updateStackedUI();
                slots[inventoryItems[i].item.getSlotPos()] = newSlot;

                break;
            }
        }
    }

    public void showItemInfoBox(Sprite s, string name, string damage, string range, string price, InventorySlot slot)
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
            infoBoxButtons.transform.GetChild(0).gameObject.SetActive(false);
            infoBoxButtons.transform.GetChild(1).gameObject.SetActive(false);
            infoBoxButtons.transform.GetChild(2).gameObject.SetActive(true);

            if(slot.amount > 1)
            {
                infoBoxButtons.transform.GetChild(3).gameObject.SetActive(true);
            }
            else
            {
                infoBoxButtons.transform.GetChild(3).gameObject.SetActive(false);
            }
        }
        else
        {
            infoBoxButtons.transform.GetChild(0).gameObject.SetActive(false);
            infoBoxButtons.transform.GetChild(1).gameObject.SetActive(true);
            infoBoxButtons.transform.GetChild(2).gameObject.SetActive(false);
            infoBoxButtons.transform.GetChild(3).gameObject.SetActive(false);
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
        infoBoxButtons.transform.GetChild(0).gameObject.SetActive(false);
        infoBoxButtons.transform.GetChild(1).gameObject.SetActive(false);
        infoBoxButtons.transform.GetChild(2).gameObject.SetActive(false);
        infoBoxButtons.transform.GetChild(3).gameObject.SetActive(false);

        activeSlot = null;
    }

    public void RemoveItemFromInventory(string interaction)
    {
        int pos = 0;

        if (activeSlot != null)
        {
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i].item == activeSlot.getItem())
                {
                    pos = i;
                    break;
                }
            }


            if (interaction == "Drop")
            {
                if (activeSlot.removeInventory(interaction))
                {
                    Debug.Log("DROPPED");

                    InventoryItem o = inventoryItems[pos];
                    o.amount -= 1;
                    inventoryItems[pos] = o;

                    if (inventoryItems[pos].amount == 0)
                    {
                        removeItem(inventoryItems[pos].item);
                    }
                }
            }
            else if (interaction == "Sell")
            {
                if (activeSlot.removeInventory(interaction))
                {
                    playerFunds += activeSlot.getItem().sellPrice;

                    if (activeSlot.amount > 0)
                    {
                        InventoryItem i = inventoryItems[pos];
                        i.amount = activeSlot.amount;
                        inventoryItems[pos] = i;
                    }
                    else
                    {
                        removeItem(inventoryItems[pos].item);
                    }

                    //Check to see if the player has sold their equipped item
                    Player.instance.weapons.checkWeapon(inventoryItems);
                }
            }
            else if (interaction == "SellAll")
            {
                activeSlot.removeInventory(interaction);

                playerFunds += (activeSlot.getItem().sellPrice * activeSlot.amount);

                activeSlot.amount = 0;

                InventoryItem i = inventoryItems[pos];
                i.amount = activeSlot.amount;
                inventoryItems[pos] = i;


                removeItem(inventoryItems[pos].item);

                //Check to see if the player has sold their equipped item
                Player.instance.weapons.checkWeapon(inventoryItems);

            }
        }

        refreshUI();
        if (activeSlot.amount <= 0)
        {
            emptyInfoBox();
        }
    }

    public void equipItem()
    {
        if(activeSlot != null)
        {
            activeSlot.equipItem();
        }

        refreshUI();
        emptyInfoBox();
    }

    public void setTrader(Trader t)
    {
        currentTrader = t;
    }

}
