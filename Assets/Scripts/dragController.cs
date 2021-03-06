using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DragController : MonoBehaviour
{
    private bool isDragging = false;
    private RaycastHit2D hit;

    public event EventHandler<GameObject> LetGo;
    public event EventHandler<GameObject> PickedUp;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)) {
            Vector2 mousePos2D = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject.CompareTag("Draggable")) {
                isDragging = true;
                Debug.Log("Something was clicked: " + hit.collider.gameObject.name);
                OnPickedUp(hit.collider.gameObject);
            } else {
                //Debug.Log("Something was NOT clicked!");
            }
        }

        if(Input.GetMouseButtonUp(0)) {
            isDragging = false;
            if(hit.collider != null) {
                OnLetGo(hit.collider.gameObject);
            }
        }
        
        if(isDragging) {
            Vector2 mousePos2D = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (mousePos2D - new Vector2(hit.transform.position.x, hit.transform.position.y));
            hit.transform.Translate(direction);
        }
    }

    public void OnLetGo(GameObject o) {
        LetGo?.Invoke(this, o);
    }

    public void OnPickedUp(GameObject o) {
        PickedUp?.Invoke(this, o);
    }
}
