using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Character : MonoBehaviour 
{
    public int id;
    public string characterName;
    public string type;
    public List<string> dialouge;

    public Item item;

    void OnEnable()
    {
        

        if (type.Equals("found")) {
            toggleItem();
            TextMeshProUGUI dialougeBox = FindObjectOfType<TextMeshProUGUI>();
            dialougeBox.SetText(dialouge[0]);

            Debug.Log("FOUND A CHARACTER");
        }

        if (type.Equals("lost")) {
            TextMeshProUGUI dialougeBox = FindObjectOfType<TextMeshProUGUI>();
            dialougeBox.SetText(dialouge[0]);
            Debug.Log("LOST A CHARACTER. GIMMIE ITEM");
        }
    }

    void Update() {
        if (type.Equals("found")) {
            if (item.transform.parent.gameObject.CompareTag("InventorySlot")) {
                done();
            }
        }
        
    }

    public bool checkItem(Item givenItem) {
        if (type.Equals("lost")) {
            if (item.transform.parent.gameObject.CompareTag("DropOff")) {
                if(givenItem.itemname.Equals(item.itemname)) {
                    TextMeshProUGUI dialougeBox = FindObjectOfType<TextMeshProUGUI>();
                    dialougeBox.SetText(dialouge[1]);
                    givenItem.transform.parent.gameObject.GetComponent<DropOff>().deactivateItem();
                    done();
                } else {
                    TextMeshProUGUI dialougeBox = FindObjectOfType<TextMeshProUGUI>();
                    dialougeBox.SetText(dialouge[2]);
                    return false;
                }
            }
        } else {
            Debug.Log("Type is not defined");
        }
        return true;
    }

    void done() {
        GameObject gameController = GameObject.Find("Game Controllers");
        mainGameController script = gameController.GetComponent<mainGameController>();
        script.characterActive = false;
        this.gameObject.SetActive(false);
    }

    void toggleItem() {
        if (item.gameObject.activeSelf) {
            item.gameObject.SetActive(false);
            item.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            item.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        } else {
            item.gameObject.SetActive(true);
            item.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            item.gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }
    }

}