using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldItem : MonoBehaviour
{
    [SerializeField]
    Item data;

    // Start is called before the first frame update
    void Start()
    {

        this.GetComponent<SpriteRenderer>().sprite = data.itemSprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Item getItemData()
    {
        return data;
    }
}
