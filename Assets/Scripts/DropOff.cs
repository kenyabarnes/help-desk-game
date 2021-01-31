using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropOff : Slot
{
    public void MoveToDesk() {
        if(item != null){
            Item tmp = item;
            RemoveItemFromSlot();
            tmp.transform.localPosition = new Vector3(0, 0, -0.1f);
        } else {
            Debug.LogError("ERROR: No Item to MoveToDesk");
        }
        
    }
}
