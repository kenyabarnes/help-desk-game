using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Item : MonoBehaviour
{
    public int id;
    
    public string itemname;
    public string description;

    public GameObject area;

    public event EventHandler<string> Enter;
    public event EventHandler<string> Exit;

    void OnTriggerEnter2D(Collider2D collider) {
    	Debug.Log("Entered Area: " + collider.gameObject.tag);
        area = collider.gameObject;
        OnEnter(collider.gameObject.tag);
    }

    void OnTriggerExit2D(Collider2D collider) {
        Debug.Log("Exited Area: " + collider.gameObject.tag);
        area = null;
        OnExit(collider.gameObject.tag);
    }

    public void OnEnter(string tag) {
        Enter?.Invoke(this, tag);
    }

    public void OnExit(string tag) {
        Exit?.Invoke(this, tag);
    }

    public void setZ(float i){
        transform.position.Set(transform.position.x, transform.position.y, i);
    }
}