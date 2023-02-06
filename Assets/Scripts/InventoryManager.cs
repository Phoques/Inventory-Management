using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerUpHandler
{

    //List of items in our inventory.
    public List<Item> inventory = new List<Item>();
    //Array of the UI Image Boxes on screen
    public Image[] slots = new Image[12];
    //Blank item    
    public Item empty;
    //Reference to the currnt selected item
    public Item selectedItem;

    //If it cant swap it will bounce back (We can then choose behaviour of our inventory)
    public bool canSwap;


    public void UpdateDisplay()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].sprite = inventory[i].icon;
        }
    }

    public void DragDropEvent()
    {
        //The selected item is the thing we are currently holiding with our mouse.
        selectedItem = DragAndDrop.currentSlot.parentInventory.inventory[DragAndDrop.selectedSlotIndex];

        //Empty meaning the scriptable item
        if(selectedItem.name != "Empty")
        {
            //This uses the mouse to grab the selected item.
            DragAndDrop.currentDrag = selectedItem;
            //This tells us what it is and where it came from (the slot it came from and the type of item it is, weapon / armour
            DragAndDrop.currentDragIndex = DragAndDrop.selectedSlotIndex;
            //This replaces the sprite in the slot with the 'empty' sprite as soon as you pick it up.
            inventory[DragAndDrop.selectedSlotIndex] = empty;
            //This rus the for loop to ensure all slots in the inventory are updated.
            UpdateDisplay();

            //This updates the sprite that follows the mouse? (Double check function)
            DragAndDrop.UpdateDrag();
        }
    }

    public void PlaceItem()
    {
        //What is the current slot and item we are interacting with.
        selectedItem = DragAndDrop.currentSlot.parentInventory.inventory[DragAndDrop.selectedSlotIndex];

        //If we have an item / it isnt empty
        if(DragAndDrop.currentDrag != null)
        {
            if (selectedItem.itemName == "Empty")
            {
                Debug.Log("Can Place this item woohoo");
                DragAndDrop.currentSlot.parentInventory.inventory[DragAndDrop.selectedSlotIndex] = DragAndDrop.currentDrag;
            }
            else 
            {
                if (!canSwap)
                {
                    Debug.Log("Bounce Back");
                    DragAndDrop.homeSlot.parentInventory.inventory[DragAndDrop.currentDragIndex] = DragAndDrop.currentDrag;
                }
                else
                {
                    Debug.Log("Swap");

                    //This takes the item in the slot were an item already exists (E.g I have click dragged a helmet, and am hovering it over the armour, this is saying the armour
                    // is going to move to the helmets original ' homeslot'
                    DragAndDrop.homeSlot.parentInventory.inventory[DragAndDrop.currentDragIndex] = selectedItem;

                    //This puts the 'Helmet' in the place of the 'Armour' that we have hovered over.
                    DragAndDrop.currentSlot.parentInventory.inventory[DragAndDrop.selectedSlotIndex] = DragAndDrop.currentDrag;
                }
            }
        }


    }




    //click on the Interface (above like IPointerEnterHandler) then press ALT + ENTER and it will propose the below solutions.
    //Otherwise they will be red angry bois.
    //Pointer EventData allows you to get information from where the pointer is navigating over.
    public void OnPointerEnter(PointerEventData eventData)
    {
        //throw new System.NotImplementedException(); This will come up so it doesnt get cranky but we will put our own logic in here.


        SlotHandler currentHandler = eventData.pointerCurrentRaycast.gameObject.GetComponent<SlotHandler>();
        currentHandler.SlotEvent();
    }

    //OnPointer down is when the mouse is clicked
    public void OnPointerDown(PointerEventData eventData)
    {

        SlotHandler currentHandler = eventData.pointerCurrentRaycast.gameObject.GetComponent<SlotHandler>();
        currentHandler.SlotEvent();
        currentHandler.SlotEventOrigin();
        DragDropEvent();

    }

    //OnpointerUp is when the mouse click is let up.
    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject == null)
        {
            //This isnt a slot and so it will bounce back to its homeslot.
            DragAndDrop.homeSlot.parentInventory.inventory[DragAndDrop.currentDragIndex] = DragAndDrop.currentDrag;

            //reset visuals of the sprites.
            DragAndDrop.ResetDrag();
            UpdateDisplay();
        }
        else
        {

            SlotHandler currentHandler = eventData.pointerCurrentRaycast.gameObject.GetComponent<SlotHandler>();
            currentHandler.SlotEvent();
            PlaceItem();
            currentHandler.parentInventory.UpdateDisplay();
        }
    }

    private void Start()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].GetComponent<SlotHandler>().slotValue = i;
            slots[i].GetComponent<SlotHandler>().parentInventory = this;
        }
            UpdateDisplay();
    }


}
