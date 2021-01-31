using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class mainGameController : MonoBehaviour
{
    public List<Character> line;

    public DragController dragControll;
    
    public Character characterPrefab;

    public GameObject characterList;
    public Item itemPrefab;
    public int numberOfCharacters = 0;
    public float itemLayer = 0f;

    public bool characterActive = false;
    public bool inArea = false;

    public TextAsset chatJsonFile;
    public TextAsset itemJsonFile;

    public ChatModel chatmodel;
    public ItemModel itemmodel;

    // Start is called before the first frame update
    void Start()
    {
        chatmodel = JsonUtility.FromJson<ChatModel>(chatJsonFile.text);
        itemmodel = JsonUtility.FromJson<ItemModel>(itemJsonFile.text);
        populateCharacters(numberOfCharacters);
        dragControll.LetGo += dragControll_OnLetGo;
        characterActive = false;
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

        // randomly choose a character class
        CharacterType[] charactertypes = chatmodel.charactertypes;
        int randomType = UnityEngine.Random.Range(0, charactertypes.Length);
        CharacterType chosenType = charactertypes[randomType];

        newCharacter.characterName = chosenType.name;
        
        //randomly choose between found type and lost type character
        string[] lostOrFound = new string[] {"lost", "found"}; 
        int randomChoice = UnityEngine.Random.Range(0, lostOrFound.Length);
        string chosenChoice = lostOrFound[randomChoice]; 

        newCharacter.type = chosenChoice;

        //randomly choose an item
        ItemJSON[] itemtypes = itemmodel.items;
        int randomItemType = UnityEngine.Random.Range(0, itemtypes.Length);
        ItemJSON chosenItem = itemtypes[randomItemType]; 

        Item newItem = Instantiate(itemPrefab, GameObject.Find("ItemSpawnPoint").transform);
        newItem.itemname = chosenItem.itemname;
        newItem.description = chosenItem.description;
        newItem.Enter += newItem_OnEnter;
        newItem.Exit += newItem_OnExit;
        newCharacter.item = newItem;

        //branching logic for adding dialouge
        newCharacter.dialouge = new List<string>();

        if (newCharacter.type.Equals("found")) {
            newCharacter.dialouge.Add(chosenType.generic.found);
            newCharacter.dialouge.Add(chosenType.generic.correct);
            newCharacter.dialouge.Add(chosenType.generic.wrong);
        } else if (newCharacter.type.Equals("lost")) {
            switch (newCharacter.item.itemname)
            {
                case "communicator":
                    newCharacter.dialouge.Add(chosenType.lost.communicator);
                    break;
                case "keys":
                    newCharacter.dialouge.Add(chosenType.lost.keys);
                    break;
                case "raygun":
                    newCharacter.dialouge.Add(chosenType.lost.raygun);
                    break;
                case "petspider":
                    newCharacter.dialouge.Add(chosenType.lost.petspider);
                    break;
                case "diary":
                    newCharacter.dialouge.Add(chosenType.lost.diary);
                    break;                
                case "ticket":
                    newCharacter.dialouge.Add(chosenType.lost.ticket);
                    break;                
                case "briefcase":
                    newCharacter.dialouge.Add(chosenType.lost.briefcase);
                    break;                
                case "toy":
                    newCharacter.dialouge.Add(chosenType.lost.toy);                                                                            
                    break;                
                default:
                    newCharacter.dialouge.Add("look man i just work here");
                    break;            
            }
            
        } else {
            Debug.Log("Character doesn't have a type assigned");
        }



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
                area.GetComponent<DropOff>().itemSetUp(item, dragControll);
            }
            if(area.tag == "InventorySlot") {
                area.GetComponent<InventorySlot>().itemSetUp(item);
            }
        }
    }
}
