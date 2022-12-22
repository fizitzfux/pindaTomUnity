using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    // VARIABLES
    private bool inventoryOpen = false;
    private List<ItemSlot> itemSlotList = new List<ItemSlot>();
    private float normalWalkSpeed;
    private float normalRunSpeed;
    private float normalMouseSensitivity;


    // REFERENCES
    public bool InventoryOpen => inventoryOpen;
    public GameObject Player;
    public GameObject Camera;
    public GameObject inventoryParent;
    public GameObject inventoryTab;
    public GameObject craftingTab;
    public GameObject itemSlotPrefab;
    public Transform inventoryItemTransform;

    private void Start()
    {
        // Roept de Walkspeed, Runspeed en MouseSensitivity aan van de player om deze te beinvloeden in dit script
        normalWalkSpeed = this.Player.GetComponent<playerController>().walkSpeed;
        normalRunSpeed = this.Player.GetComponent<playerController>().runSpeed;
        normalMouseSensitivity = this.Camera.GetComponent<CameraController>().mouseSensitivity;

        Inventory.instance.onItemChange += UpdateInventoryUI;
        UpdateInventoryUI();
    }

    void Update()
    {
        // Code om de inventory te openen en te sluiten
        if(Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Inventory toggled");
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
        else if(inventoryOpen && Input.GetKeyDown(KeyCode.Escape))
        {
            CloseInventory();
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

        this.Player.GetComponent<playerController>().walkSpeed = 0;
        this.Player.GetComponent<playerController>().runSpeed = 0;
        this.Camera.GetComponent<CameraController>().mouseSensitivity = 0;
    }

    private void CloseInventory()
    {
        ChangeCursorState(true);
        inventoryOpen = false;
        inventoryParent.SetActive(false);

        this.Player.GetComponent<playerController>().walkSpeed = normalWalkSpeed;
        this.Player.GetComponent<playerController>().runSpeed = normalRunSpeed;
        this.Camera.GetComponent<CameraController>().mouseSensitivity = normalMouseSensitivity;
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
