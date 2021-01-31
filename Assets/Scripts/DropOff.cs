using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropOff : Slot
{
    public void MoveToDesk() {
        Item tmp = item;
        RemoveItemFromSlot();
        tmp.transform.localPosition = new Vector3(0, 0, -0.1f);
    }
}
