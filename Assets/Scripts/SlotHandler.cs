using UnityEngine;

public class SlotHandler : MonoBehaviour
{
	public int slotValue;
	public InventoryManager parentInventory;
	
	public void SlotEvent()
	{
		DragAndDrop.selectedSlotIndex = slotValue;
		DragAndDrop.currentSlot = this;
	}
   public void SlotEventOrigin()
   {
	   DragAndDrop.homeSlot = this;
   }
}
