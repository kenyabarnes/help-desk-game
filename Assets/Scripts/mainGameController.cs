using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class mainGameController : MonoBehaviour
{

    public List<Item> inventory;
    public List<Character> line;
    
    public Character characterPrefab;

    public GameObject characterList;
    public Item itemPrefab;
    public int numberOfCharacters = 0;

    public bool characterActive = false;

    // Start is called before the first frame update
    void Start()
    {
        populateCharacters(numberOfCharacters);

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

        //Item newItem = Instantiate(itemPrefab, newCharacter.gameObject.transform);
        Item newItem = Instantiate(itemPrefab, GameObject.Find("ItemSpawnPoint").transform);
        newItem.itemName = "Keys";
        newItem.description = "someone probably lost these";

        newItem.DroppedOff += newItem_OnDroppedOff;

        newCharacter.item = newItem;

        return newCharacter;
    }

    void newItem_OnDroppedOff(object sender, string tag){
        Debug.Log("Item Name: " + (sender as Item).itemName + " Tag: " + tag);
    }
}
