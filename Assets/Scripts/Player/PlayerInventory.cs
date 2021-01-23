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

    //Info Box Children
    [SerializeField]
    GameObject infoBox;

    [SerializeField]
    TextMeshProUGUI itemName;
    [SerializeField]
    TextMeshProUGUI itemEffect;
    [SerializeField]
    TextMeshProUGUI itemRange;
    [SerializeField]
    TextMeshProUGUI itemPrice;
    [SerializeField]
    GameObject itemEquip;
    [SerializeField]
    GameObject itemDrop;
    [SerializeField]
    GameObject itemSell;
    [SerializeField]
    GameObject itemSellAll;


    [SerializeField]
    GameObject infoBoxButtons;

    InventorySlot activeSlot;

    [SerializeField]
    TextMeshProUGUI funds;

    bool spawnedInventory = false;

    public int playerFunds = 50;

    Trader currentTrader;

    [SerializeField]
    Sprite emptySlotImage;

    [SerializeField]
    Color weaponColour;

    [SerializeField]
    Color itemColour;
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
    }

    private void OnEnable()
    {
        emptyInfoBox();
    }

    private void FixedUpdate()
    {
        funds.text = playerFunds.ToString();
    }

    public void adjustFunds(int amount)
    {
        playerFunds += amount;
        Player.instance.audioHandler.playCoins();
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

    public void setInventory(bool set)
    {
        inventory.SetActive(set);

        foreach(Slot obj in slots)
        {
            obj.slotObject.GetComponent<RectTransform>().localScale = new Vector3(0.7f, 0.7f, 1);
        }

        emptyInfoBox();
    }

    void updateUI(Item item)
    {
        for(int i =0; i < slots.Count; i++)
        {
            if(!slots[i].isTaken)
            {
                Slot newSlot;
                newSlot = slots[i];
                newSlot.slotObject.transform.GetChild(1).GetComponent<Image>().sprite = item.itemSprite;
                newSlot.isTaken = true;
                newSlot.slotObject.GetComponent<InventorySlot>().setButtonData(item);
                newSlot.slotObject.GetComponent<InventorySlot>().setSlotPos(i);
                newSlot.slotObject.GetComponent<InventorySlot>().amount = inventoryItems[i].amount;
                newSlot.slotObject.GetComponent<InventorySlot>().updateStackedUI();

                if(item.GetType() == typeof(Weapon))
                {
                    newSlot.slotObject.GetComponent<Image>().color = weaponColour;
                }
                else
                {
                    newSlot.slotObject.GetComponent<Image>().color = itemColour;
                }

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
                newSlot.slotObject.transform.GetChild(1).GetComponent<Image>().sprite = inventoryItems[i].item.itemSprite;
                newSlot.isTaken = true;
                newSlot.slotObject.GetComponent<InventorySlot>().setButtonData(inventoryItems[i].item);
                newSlot.slotObject.GetComponent<InventorySlot>().setSlotPos(i);
                newSlot.slotObject.GetComponent<InventorySlot>().amount = inventoryItems[i].amount;
                newSlot.slotObject.GetComponent<InventorySlot>().updateStackedUI();

                if (newSlot.slotObject.GetComponent<InventorySlot>().getItem().GetType() == typeof(Weapon))
                {
                    newSlot.slotObject.GetComponent<Image>().color = weaponColour;
                }
                else
                {
                    newSlot.slotObject.GetComponent<Image>().color = itemColour;
                }

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
            newSlot.slotObject.transform.GetChild(1).GetComponent<Image>().sprite = emptySlotImage;
            newSlot.slotObject.GetComponent<InventorySlot>().amount = 0;
            newSlot.slotObject.GetComponent<InventorySlot>().updateStackedUI();
            newSlot.slotObject.GetComponent<InventorySlot>().setButtonData(null);
            newSlot.slotObject.GetComponent<Image>().color = Color.white;
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
        if (checkIfStackable(t))
        {
            addStackable(t);
        }
        else
        {
            InventoryItem newInventory = new InventoryItem();
            newInventory.item = t;
            newInventory.amount = 1;
            inventoryItems.Add(newInventory);
            updateUI(t);
        }
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

    public void showItemInfoBox(Sprite s, string name, string damage, string range, string price, InventorySlot slot, bool equipable, bool droppable)
    {
        itemName.gameObject.SetActive(true);
        itemName.text = name;
        itemEffect.gameObject.SetActive(true);
        itemEffect.text = damage;
        itemRange.gameObject.SetActive(true);
        itemRange.text = range;
        itemPrice.gameObject.SetActive(true);
        itemPrice.text = "Sell Price: " + price;

        if (currentTrader != null)
        {
            itemEquip.gameObject.SetActive(false);
            itemDrop.gameObject.SetActive(false);
            itemSell.gameObject.SetActive(true);
            
            if(slot.amount > 1)
            {
               itemSellAll.gameObject.SetActive(true);
            }
            else
            {
                itemSellAll.gameObject.SetActive(false);
            }
        }
        else
        {
            if (equipable)
            {
                itemEquip.gameObject.SetActive(true);
            }
            else
            {
               itemEquip.gameObject.SetActive(false);
            }

            if (droppable)
            {
                itemDrop.gameObject.SetActive(true);
            }
            else
            {
                itemDrop.gameObject.SetActive(false);
            }

            itemSell.gameObject.SetActive(false);
            itemSellAll.gameObject.SetActive(false);
        }

        activeSlot = slot;
    }

    public void emptyInfoBox()
    {
        itemName.gameObject.SetActive(false);
        itemEffect.gameObject.SetActive(false);
        itemRange.gameObject.SetActive(false);
        itemPrice.gameObject.SetActive(false);
        itemEquip.SetActive(false);
        itemDrop.SetActive(false);
        itemSell.SetActive(false);
        itemSellAll.SetActive(false);

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

            if(interaction == "Use")
            {
                InventoryItem o = inventoryItems[pos];
                o.amount -= 1;
                inventoryItems[pos] = o;

                if (inventoryItems[pos].amount == 0)
                {
                    removeItem(inventoryItems[pos].item);
                }

                Player.instance.audioHandler.playEat();
            }
            else if (interaction == "Drop")
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

                    Player.instance.audioHandler.playDrop();
                }
            }
            else if (interaction == "Sell")
            {
                if (activeSlot.removeInventory(interaction))
                {
                    adjustFunds(activeSlot.getItem().sellPrice);

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

                }
            }
            else if (interaction == "SellAll")
            {
                adjustFunds((activeSlot.getItem().sellPrice * activeSlot.amount));

                activeSlot.removeInventory(interaction);

                activeSlot.amount = 0;

                InventoryItem i = inventoryItems[pos];
                i.amount = activeSlot.amount;
                inventoryItems[pos] = i;


                removeItem(inventoryItems[pos].item);
            }
        }

        //Check to see if the player has sold their equipped item
        Player.instance.weapons.checkWeapon(inventoryItems);

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
