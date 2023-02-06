using UnityEngine;
using UnityEngine.UI;


public class DragAndDrop : MonoBehaviour
{

    //static reference to the current item that we are dragging
    public static Item currentDrag;
    public static SlotHandler currentSlot;
    public static SlotHandler homeSlot;
    public static int currentDragIndex;

    //Hacky shit way to get component to take the Image currentDragImage, then make it equal dragImage lol (This is just like doing get component)
    public Image getComponentImage;
    public static Image dragImage;
    

    public static int selectedSlotIndex;

    public static void UpdateDrag()
    {
        //Sets the sprite following the mouse to the icon sprite of the item.
        dragImage.sprite = currentDrag.icon;
    }

    public static void ResetDrag()
    {
        //clear the current item
        currentDrag = null;
        //turn off the image following the mouse;
        dragImage.gameObject.SetActive(false);
        //Clear current slot
        currentSlot = null;
    }

    private void Start()
    {
        //this is the hacky crap lol
        dragImage = getComponentImage;

    }

    private void Update()
    {
        //If we have clicked and are currently dragging an item. / if something is connected.
        if (currentDrag != null)
        {   //ActiveSelf is checking the bool of 'SetActive' this is how to check this thing.
            if(dragImage.gameObject.activeSelf == false)
            {
                dragImage.gameObject.SetActive(true);
                dragImage.transform.position = Input.mousePosition;
            }
        }
    }


}
