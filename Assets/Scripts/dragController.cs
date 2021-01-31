using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class dragController : MonoBehaviour
{
    public bool smoothDrag = false;
    //public float dragLayer = 0;
    public float smoothDragPading = .05f;
    public float speed = 0.08f;

    private bool isDragging = false;
    private RaycastHit2D hit;

    public event EventHandler<GameObject> LetGo;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)) {
            Vector2 mousePos2D = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject.CompareTag("Draggable")) {
                isDragging = true;
                Debug.Log("Something was clicked: " + hit.collider.gameObject.name);
            } else {
                //Debug.Log("Something was NOT clicked!");
            }
        }

        if(Input.GetMouseButtonUp(0)) {
            isDragging = false;
            OnLetGo(hit.collider.gameObject);
        }
        
        if(isDragging) {
            Vector2 mousePos2D = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (mousePos2D - new Vector2(hit.transform.position.x, hit.transform.position.y));

            if(smoothDrag && Mathf.Abs(direction.magnitude)  > smoothDragPading) {
                direction.Normalize();
                hit.transform.Translate(direction * speed);
            } else {
                hit.transform.Translate(direction);
            }
        }
    }

    public void OnLetGo(GameObject o) {
        LetGo?.Invoke(this, o);
    }
}
