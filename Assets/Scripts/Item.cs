using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Item : MonoBehaviour
{
    public string itemName;
    public string description;

    public void remove(){
        Destroy(gameObject);
    }

    public event EventHandler DroppedOff;

    void OnTriggerEnter2D(Collider2D collider) {
    	Debug.Log("Entered drop off area");
        OnDroppedOff(EventArgs.Empty);
    }

    public void OnDroppedOff(EventArgs e) {
        DroppedOff?.Invoke(this, e);
    }
}