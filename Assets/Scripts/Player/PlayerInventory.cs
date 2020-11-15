using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    GameObject itemSlotPrefab;

    [SerializeField]
    GameObject inventoryParent;

    bool spawnedInventory = false;

    private void Start()
    {
        if(!spawnedInventory)
        {
            for(int i =0; i < 9; i++)
            {
                Slot newSlot = new Slot();
                newSlot.slotObject = Instantiate(itemSlotPrefab);
                newSlot.slotObject.transform.SetParent(inventoryParent.transform);
                newSlot.isTaken = false;
                slots.Add(newSlot);
            }

            spawnedInventory = true;
        }

    }

    public void showInventory(bool set)
    {
        inventoryParent.SetActive(set);
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
}
