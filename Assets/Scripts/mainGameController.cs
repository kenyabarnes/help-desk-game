using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainGameController : MonoBehaviour
{

    public List<Item> inventory;
    public List<Character> line;
    
    public Character characterPrefab;

    public GameObject characterList;
    public Item itemPrefab;
    public int numberOfCharacters = 0;

    // Start is called before the first frame update
    void Start()
    {
        populateCharacters(numberOfCharacters);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void populateCharacters(int numberOfCharacters) {
        for (int i = 0; i < numberOfCharacters; i++) {
           line.Add(createCharacter());
        }
    }

    Character createCharacter() {
        Character newCharacter = Instantiate(characterPrefab, characterList.transform);
        newCharacter.characterName = "test";
        newCharacter.type = "found";
        newCharacter.dialouge = new List<string>();
        newCharacter.dialouge.Add("i found my cat!");

        return newCharacter;
    }
}
