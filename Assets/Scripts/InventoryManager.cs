using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerUpHandler
{
	//The list of items in our inventory
	public List<Item> inventory = new List<Item>();
	//Array of the UI Image boxes on screen for this inventory
	public Image[] slots = new Image[12];
	//Blank item
	public Item empty;
	//Reference to the Current Selected Item
	public Item selectedItem;
	public bool canSwap;
	
	public void UpdateDisplay()
	{
		for(int i = 0; i < slots.Length; i++)
		{
			slots[i].sprite = inventory[i].icon;
		}
	}
	public void DragDropEvent()
	{
		selectedItem = DragAndDrop.currentSlot.parentInventory.inventory[DragAndDrop.selectedSlotIndex];
		if(selectedItem.itemName != "Empty")
		{
			DragAndDrop.currentDrag = selectedItem;
			DragAndDrop.currentDragIndex = DragAndDrop.selectedSlotIndex;
			inventory[DragAndDrop.selectedSlotIndex] = empty;
			UpdateDisplay();
			DragAndDrop.UpdateDrag();
		}
	}	
	public void PlaceItem()
	{
		selectedItem = DragAndDrop.currentSlot.parentInventory.inventory[DragAndDrop.selectedSlotIndex];
		//if we have an item
		if(DragAndDrop.currentDrag != null)
		{
			if(selectedItem.itemName == "Empty")
			{
				Debug.Log("Can Place Woop");
				DragAndDrop.currentSlot.parentInventory.inventory[DragAndDrop.selectedSlotIndex] = DragAndDrop.currentDrag;
			}
			else
			{
				if(!canSwap)
				{
					Debug.Log("Bounce Back");
					DragAndDrop.homeSlot.parentInventory.inventory[DragAndDrop.currentDragIndex] = DragAndDrop.currentDrag;
				}
				else
				{
					Debug.Log("Swap");
					
					DragAndDrop.homeSlot.parentInventory.inventory[DragAndDrop.currentDragIndex] = selectedItem;
					
					DragAndDrop.currentSlot.parentInventory.inventory[DragAndDrop.selectedSlotIndex] = DragAndDrop.currentDrag;
				}
			}
			DragAndDrop.ResetDrag();
			UpdateDisplay();
		}
	
	}
	public void OnPointerEnter( PointerEventData eventData)
	{
		SlotHandler currentHandler = eventData.pointerCurrentRaycast.gameObject.GetComponent<SlotHandler>();
		currentHandler.SlotEvent();
	}
	public void OnPointerDown( PointerEventData eventData)
	{
		SlotHandler currentHandler = eventData.pointerCurrentRaycast.gameObject.GetComponent<SlotHandler>();
		currentHandler.SlotEvent();
		currentHandler.SlotEventOrigin();
		DragDropEvent();
	}
	public void OnPointerUp( PointerEventData eventData)
	{
		//if we are not over a slot
		if(eventData.pointerCurrentRaycast.gameObject == null)
		{
			//bitch you done did fucked that aint no slot!!! Go home your drunk
			DragAndDrop.homeSlot.parentInventory.inventory[DragAndDrop.currentDragIndex] = DragAndDrop.currentDrag;
			//Reset Visuals
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
	void Start()
	{
		for(int i = 0; i < slots.Length; i++)
		{
			slots[i].GetComponent<SlotHandler>().slotValue = i;
			slots[i].GetComponent<SlotHandler>().parentInventory = this;			
		}
		UpdateDisplay();
	}
}
