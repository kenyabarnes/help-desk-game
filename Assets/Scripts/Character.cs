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
        item.gameObject.SetActive(true);

        if (type.Equals("found")) {

            TextMeshProUGUI dialougeBox = FindObjectOfType<TextMeshProUGUI>();
            dialougeBox.SetText(dialouge[0]);

            List<Item> inventory = GameObject.Find("Game Controllers").transform.GetComponent<mainGameController>().inventory;
            inventory.Add(item);
            Debug.Log("FOUND A CHARACTER");
        }

        if (type.Equals("lost")) {
            Debug.Log("LOST A CHARACTER. GIMMIE ITEM");
        }
    }

}