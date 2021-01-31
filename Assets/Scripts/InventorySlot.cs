using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    public  Item item;

    public void itemSetUp(Item i) {
        if(item == null){
            item = i;
            item.transform.SetParent(this.transform);
            item.Exit += item_OnExit;

            setPos();            
        } else {
            Debug.LogError("ERROR: Slot already has Item");
        }
    }

    public void setPos() {

    }

    public void item_OnExit(object sender, string tag) {
        GameObject isp = GameObject.Find("ItemSpawnPoint");
        item.transform.SetParent(isp.transform);
        item.Exit -= item_OnExit; 
        item = null;
    }

}
