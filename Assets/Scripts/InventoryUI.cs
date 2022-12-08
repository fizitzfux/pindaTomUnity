using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    // VARIABLES
    private bool inventoryOpen = false;
    private List<ItemSlot> itemSlotList = new List<ItemSlot>();


    // REFERENCES
    public bool InventoryOpen => inventoryOpen;
    public Behaviour CameraController;
    public Behaviour PlayerController;
    public GameObject inventoryParent;
    public GameObject inventoryTab;
    public GameObject craftingTab;
    public GameObject itemSlotPrefab;
    public Transform inventoryItemTransform;

    private void Start()
    {
        Inventory.instance.onItemChange += UpdateInventoryUI;
        UpdateInventoryUI();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            if(inventoryOpen)
            {
                //close inventory
                CloseInventory();
            }
            else
            {
                //open inventory
                OpenInventory();
            }
        }
    }

    private void UpdateInventoryUI()
    {
        int currentItemCount = Inventory.instance.inventoryItemList.Count;

        if(currentItemCount > itemSlotList.Count)
        {
            //Add move item slots
            AddItemSlots(currentItemCount);
        }


        for(int i = 0; i < itemSlotList.Count; ++i)
        {
            if(i < currentItemCount)
            {
                //update the current item in the slot
                itemSlotList[i].AddItem(Inventory.instance.inventoryItemList[i]);
            }
            else
            {
                itemSlotList[i].DestroySlot();
                itemSlotList.RemoveAt(i);
            }
        }
    }

    private void AddItemSlots(int currentItemCount)
    {
        int amount = currentItemCount - itemSlotList.Count;

        for(int i = 0; i < amount; ++i)
        {
            GameObject GO = Instantiate(itemSlotPrefab, inventoryItemTransform);
            ItemSlot newSlot = GO.GetComponent<ItemSlot>();
            itemSlotList.Add(newSlot);
        }
    }

    private void OpenInventory()
    {
        ChangeCursorState(false);
        OnInventoryTabClicked();
        inventoryOpen = true;
        inventoryParent.SetActive(true);
        PlayerController.enabled = false;
        CameraController.enabled = false;
    }

    private void CloseInventory()
    {
        ChangeCursorState(true);
        inventoryOpen = false;
        inventoryParent.SetActive(false);
        PlayerController.enabled = true;
        CameraController.enabled = true;
    }

    public void OnCraftingTabClicked()
    {
        craftingTab.SetActive(true);
        inventoryTab.SetActive(false);
    }

        public void OnInventoryTabClicked()
    {
        craftingTab.SetActive(false);
        inventoryTab.SetActive(true);
    }

    private void ChangeCursorState(bool lockCursor)
    {
        if(lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
