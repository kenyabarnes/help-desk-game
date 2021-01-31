using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JSONReader : MonoBehaviour
{
    public TextAsset chatJsonFile;
    public TextAsset itemJsonFile;

    void Start()
    {
        ChatModel chatmodel = JsonUtility.FromJson<ChatModel>(chatJsonFile.text);

        foreach (CharacterType charType in chatmodel.charactertypes) {
            Debug.Log("Loaded Character with the following attributes:");
            Debug.Log("name:" + charType.name + "| type:" + charType.type);
            // Debug.Log("found quote:" + charType.generic.found);
        }

        ItemModel itemmodel = JsonUtility.FromJson<ItemModel>(itemJsonFile.text);

        foreach(Item item in itemmodel.items) {
            Debug.Log("Loaded Item with the following attributes");
            Debug.Log("name:" + item.name + " and description: " + item.description);
        }
    }
}