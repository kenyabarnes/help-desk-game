using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
     public  Item item;
     public DragController dragController;

    public void itemSetUp(Item i, DragController dragController) {
        if(item == null){
            item = i;
            item.transform.SetParent(this.transform);
            item.Exit += item_OnExit;
            dragController.PickedUp += item_OnPickedUp;
            this.dragController = dragController;

            CenterItem();            
        } else {
            Debug.LogError("ERROR: Slot already has Item");
        }
    }

    public void CenterItem() {
        item.transform.localPosition = new Vector3(0, 0, -0.1f);
    }

    public void LeaveSlot() {
        GameObject isp = GameObject.Find("ItemSpawnPoint");
        item.transform.SetParent(isp.transform);
        item.Exit -= item_OnExit;
        dragController.PickedUp -= item_OnPickedUp;
        dragController = null;
        item = null;
    }

    public void item_OnExit(object sender, string tag) {
        if(item != null){
            LeaveSlot();
        }
    }
    public void item_OnPickedUp(object sender, GameObject o) {
        if(item != null && item.id == o.GetComponent<Item>().id){
            LeaveSlot();
        }
    }
}
