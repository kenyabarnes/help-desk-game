using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class mainGameController : MonoBehaviour
{
    public List<Character> line;

    public DragController dragControll;
    
    public Character characterPrefab;

    public GameObject characterList;

    public Item communicatorPrefab;
    public Item keysPrefab;
    public Item raygunPrefab;
    public Item petspiderPrefab;
    public Item diaryPrefab;
    public Item ticketPrefab;
    public Item briefcasePrefab;
    public Item toyPrefab;

    public int numberOfCharacters = 0;
    public float itemLayer = 0f;

    public bool characterActive = false;

    public GameObject activeCharacter;
    public bool inArea = false;

    public TextAsset chatJsonFile;
    public TextAsset itemJsonFile;

    public ChatModel chatmodel;
    public ItemModel itemmodel;

    public Button itemNotFound;

    public GameObject karen;
    public GameObject businessman;
    public GameObject valleygirl;
    public GameObject dejectedguy;
    public GameObject casualthief;

    // Start is called before the first frame update
    void Start()
    {
        chatmodel = JsonUtility.FromJson<ChatModel>(chatJsonFile.text);
        itemmodel = JsonUtility.FromJson<ItemModel>(itemJsonFile.text);
        populateCharacters(numberOfCharacters);

        Button btn = itemNotFound.GetComponent<Button>();
        btn.onClick.AddListener(notFoundClick);
        dragControll.LetGo += dragControll_OnLetGo;
        characterActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (line.Count == 0) {
            Debug.Log("Game Over!");
            this.GetComponent<EndGameSceneScript>().nextScene("game_over");
        }

        if (!characterActive && (line.Count != 0)) {
            int nextId = line[0].id;
            activeCharacter = GameObject.Find("Queue").transform.GetChild(nextId).gameObject;
            characterActive = true;
            activeCharacter.SetActive(true);
            line.RemoveAt(0);
            
            this.GetComponent<AudioSource>().clip = activeCharacter.GetComponent<Character>().audioClip;
            this.GetComponent<AudioSource>().Play();
        }

        if(characterActive && activeCharacter.GetComponent<Character>().type.Equals("lost") && !itemNotFound.gameObject.activeSelf) {
            itemNotFound.gameObject.SetActive(true);
        }

        if(characterActive && activeCharacter.GetComponent<Character>().type.Equals("found") && itemNotFound.gameObject.activeSelf) {
            itemNotFound.gameObject.SetActive(false);
        }
    }

    void populateCharacters(int numberOfCharacters) {
        for (int i = 0; i < numberOfCharacters; i++) {
           line.Add(createCharacter(i));
        }
    }

    void notFoundClick() {
        activeCharacter.GetComponent<Character>().notFoundClick();
    }

    Character createCharacter(int id) {
        Character newCharacter = Instantiate(characterPrefab, characterList.transform);
        newCharacter.id = id;

        // randomly choose a character class
        CharacterType[] charactertypes = chatmodel.charactertypes;
        int randomType = UnityEngine.Random.Range(0, charactertypes.Length);
        CharacterType chosenType = charactertypes[randomType];

        newCharacter.characterName = chosenType.name;

        GameObject animator;
        switch(chosenType.type){
            
            case "karen":
                animator = Instantiate(karen, newCharacter.transform);
                newCharacter.audioClip = this.GetComponent<AudioStorage>().karen_audio;
                break;
            case "businessman":
                animator = Instantiate(businessman, newCharacter.transform);
                newCharacter.audioClip = this.GetComponent<AudioStorage>().businessman_audio;
                break;
            case "valleygirl":
                animator = Instantiate(valleygirl, newCharacter.transform);
                newCharacter.audioClip = this.GetComponent<AudioStorage>().valleygirl_audio;
                break;
            case "dejectedguy":
                animator = Instantiate(dejectedguy, newCharacter.transform);
                newCharacter.audioClip = this.GetComponent<AudioStorage>().dejectedguy_audio;
                break;
            case "casualthief":
                animator = Instantiate(casualthief, newCharacter.transform);
                newCharacter.audioClip = this.GetComponent<AudioStorage>().casualthief_audio;
                break;
        }
        
        //randomly choose between found type and lost type character
        string[] lostOrFound = new string[] {"lost", "found"}; 
        int randomChoice = UnityEngine.Random.Range(0, lostOrFound.Length);
        string chosenChoice = lostOrFound[randomChoice]; 

        newCharacter.type = chosenChoice;

        //randomly choose an item
        ItemJSON[] itemtypes = itemmodel.items;
        int randomItemType = UnityEngine.Random.Range(0, itemtypes.Length);
        ItemJSON chosenItem = itemtypes[randomItemType]; 

        Item newItem;

        switch (chosenItem.itemname)
        {
            case "communicator":
                newItem = Instantiate(communicatorPrefab, GameObject.Find("ItemSpawnPoint").transform);
                break;
            case "keys":
                newItem = Instantiate(keysPrefab, GameObject.Find("ItemSpawnPoint").transform);
                break;
            case "raygun":
                newItem = Instantiate(raygunPrefab, GameObject.Find("ItemSpawnPoint").transform);
                break;
            case "petspider":
                newItem = Instantiate(petspiderPrefab, GameObject.Find("ItemSpawnPoint").transform);
                break;
            case "diary":
                newItem = Instantiate(diaryPrefab, GameObject.Find("ItemSpawnPoint").transform);
                break;                
            case "ticket":
                newItem = Instantiate(ticketPrefab, GameObject.Find("ItemSpawnPoint").transform);
                break;                
            case "briefcase":
                newItem = Instantiate(briefcasePrefab, GameObject.Find("ItemSpawnPoint").transform);
                break;                
            case "toy":
                newItem = Instantiate(toyPrefab, GameObject.Find("ItemSpawnPoint").transform);                                                                            
                break;      
            default:
                newItem = Instantiate(keysPrefab, GameObject.Find("ItemSpawnPoint").transform);
                break;
        }
         
        newItem.id = id;
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
            newCharacter.dialouge.Add(chosenType.generic.not_available);
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
            newCharacter.dialouge.Add(chosenType.generic.correct);
            newCharacter.dialouge.Add(chosenType.generic.wrong);
            newCharacter.dialouge.Add(chosenType.generic.not_available);
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
                area.GetComponent<DropOff>().PutItemInSlot(item, dragControll);

                if (!activeCharacter.GetComponent<Character>().checkItem(item)) {
                    area.GetComponent<DropOff>().MoveToDesk();
                }
                
            }
            if(area.tag == "InventorySlot") {
                area.GetComponent<InventorySlot>().PutItemInSlot(item, dragControll);
            }
        }
    }
}
