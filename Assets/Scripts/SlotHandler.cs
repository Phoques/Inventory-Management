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
     // As in where the item came from originally.
    public void SlotEventOrigin()
    {
        DragAndDrop.homeSlot = this;
    }
  
}
