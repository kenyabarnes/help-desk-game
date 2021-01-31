using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Item : MonoBehaviour
{
    public string itemName;
    public string description;

    public event EventHandler<string> DroppedOff;

    void OnTriggerEnter2D(Collider2D collider) {
    	Debug.Log("Entered drop off area");
        OnDroppedOff(collider.gameObject.tag);
    }

    public void OnDroppedOff(string tag) {
        DroppedOff?.Invoke(this, tag);
    }
}