using UnityEngine;
using UnityEngine.UI;

public class DragAndDrop : MonoBehaviour
{
  //static reference to the current item that we are dragging
  public static Item currentDrag;
  public static SlotHandler currentSlot;
  public static SlotHandler homeSlot;
  public static int currentDragIndex;
  //HACKY SHIT WAY TO GET COMPONENT
  public Image getComponentImage;
  public static Image dragImage;
  public static int selectedSlotIndex;
  
  
  public static void UpdateDrag()
  {
	  //sets the sprite following the mouse to the icon sprite of the item
	dragImage.sprite = currentDrag.icon;  
  }
   public static void ResetDrag()
  {
	  //Clear the current item
	  currentDrag = null;
	  //Turn off the Image following the Mouse
	  dragImage.gameObject.SetActive(false);
	  //Clear Current Slot
	  currentSlot = null;
  }
  void Start()
  {
	  dragImage = getComponentImage;
  }
  void Update()
  {
	  //if we are dragging an item...if something is connected
	  if(currentDrag != null)
	  {
		  if(dragImage.gameObject.activeSelf == false)
		  {
			  dragImage.gameObject.SetActive(true);
		  }
		   dragImage.transform.position = Input.mousePosition;
		   }
  }
}
