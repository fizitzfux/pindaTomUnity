using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region singleton
    public static Inventory instance;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    #endregion

    public delegate void OnItemChange();
    public OnItemChange onItemChange = delegate {};

    // Maakt een lijst om de hoeveelheid items bij te houden en om items toetevoegen en te verwijderen
    public List<Item> inventoryItemList = new List<Item>();

    // Voegt een item toe aan je inventory
    public void AddItem(Item item)
    {
        inventoryItemList.Add(item);
        onItemChange.Invoke();
    }

    // Verwijderdt een item uit je inventory
    public void RemoveItem(Item item)
    {
        inventoryItemList.Remove(item);
        onItemChange.Invoke();
    }
    
}
