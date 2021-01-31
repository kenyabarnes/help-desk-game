using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class mainGameController : MonoBehaviour
{
    public List<Character> line;

    public dragController dragControll;
    
    public Character characterPrefab;

    public GameObject characterList;
    public Item itemPrefab;
    public int numberOfCharacters = 0;
    public float itemLayer = 0f;

    public bool characterActive = false;
    public bool inArea = false;

    // Start is called before the first frame update
    void Start()
    {
        populateCharacters(numberOfCharacters);
        dragControll.LetGo += dragControll_OnLetGo;
    }

    // Update is called once per frame
    void Update()
    {
        if (line.Count == 0) {
            Debug.Log("Game Over!");
        }

        if (!characterActive) {
            int nextId = line[0].id;
            Transform activeCharacter = GameObject.Find("Queue").transform.GetChild(nextId);
            characterActive = true;
            activeCharacter.gameObject.SetActive(true);
            line.RemoveAt(0);
            
        }
    }

    void populateCharacters(int numberOfCharacters) {
        for (int i = 0; i < numberOfCharacters; i++) {
           line.Add(createCharacter(i));
        }
    }

    Character createCharacter(int id) {
        Character newCharacter = Instantiate(characterPrefab, characterList.transform);
        newCharacter.id = id;
        newCharacter.characterName = "test";
        newCharacter.type = "found";
        newCharacter.dialouge = new List<string>();
        newCharacter.dialouge.Add("The first line is the initial text of the character.");
        newCharacter.dialouge.Add("The second line is the text of when the player gets the item right.");
        newCharacter.dialouge.Add("The third line is the text when the player gets the item wrong.");

        Item newItem = Instantiate(itemPrefab, GameObject.Find("ItemSpawnPoint").transform);
        newItem.itemName = "Keys";
        newItem.description = "someone probably lost these";

        newItem.Enter += newItem_OnEnter;
        newItem.Exit += newItem_OnExit;

        newCharacter.item = newItem;

        return newCharacter;
    }

    void newItem_OnEnter(object sender, string tag) {
        //Debug.Log("Item Name: " + (sender as Item).itemName + " Tag: " + tag);
        inArea = true;
    }

    void newItem_OnExit(object sender, string tag) {
        //Debug.Log("Item Name: " + (sender as Item).itemName + " Tag: " + tag);
        inArea = false;
    }

    void dragControll_OnLetGo(object sender, GameObject o) {
        if(inArea){
            Item item = o.GetComponent<Item>();
            GameObject area = item.area;
        
            Debug.Log("Item Let Go At: " + area.tag);

            if(area.tag == "DropOff") {

            }
            if(area.tag == "InventorySlot") {
                area.GetComponent<InventorySlot>().itemSetUp(item);
            }
        }
    }
}
