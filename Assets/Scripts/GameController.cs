using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [HideInInspector] public RoomNavigation roomNavigation;
    [HideInInspector] public List<string> actionLog = new List<string>();
    [HideInInspector] public List<string> interactionDescriptionsInRoom = new List<string>();
    [HideInInspector] public ItemManager itemManager;
    public Text displayText;
    public InputAction[] inputActions;


    private void Awake()
    {
        roomNavigation = GetComponent<RoomNavigation>();
        itemManager = GetComponent<ItemManager>();
    }

    private void Start()
    {
        DisplayRoomText();
        DisplayLoggedText();
    }

    public void LogStringWithReturn (string stringToAdd)
    {
        actionLog.Add(stringToAdd);
    }

    public void DisplayRoomText()
    {
        ClearCollectionsForNewRoom();
        UnpackRoom();
        PrepareObjectsToTakeOrExamine(roomNavigation.currentRoom);
        string joinedInteractionDescriptions = string.Join("\n", interactionDescriptionsInRoom.ToArray());
        string combinedText = roomNavigation.currentRoom.description + "\n" + joinedInteractionDescriptions;
        LogStringWithReturn(combinedText);
        for (int i = 0; i < roomNavigation.currentRoom.interactableObjects.Length; i++)
        {
            Debug.Log(roomNavigation.currentRoom.interactableObjects[i]);
        }
        
    }

    public void DisplayLoggedText()
    {
        string logAsText = string.Join("\n", actionLog.ToArray());
        displayText.text = logAsText;
    }

    public void UnpackRoom()
    {
        roomNavigation.UnpackExits();
    }

    public void PrepareObjectsToTakeOrExamine(Room currentRoom)
    {
        for (int i = 0; i < currentRoom.interactableObjects.Length; i++)
        {
            InteractableObjects interactableObjectInRoom = currentRoom.interactableObjects[i];
            string descriptionInRoom = itemManager.GetObjectsNotInInventory(currentRoom, i);
            if (descriptionInRoom != null)
            {
                interactionDescriptionsInRoom.Add(descriptionInRoom);
            }

            for (int j = 0; j < interactableObjectInRoom.interactions.Length; j++)
            {
                Interaction interaction = interactableObjectInRoom.interactions[j];
                
                if (interaction.inputAction.keyString == "examine")
                {
                    itemManager.examineDictionary.Add(interactableObjectInRoom.noun, interaction.textResponse);
                }

                if (interaction.inputAction.keyString == "take")
                {
                    itemManager.takeDictionary.Add(interactableObjectInRoom.noun, interaction.textResponse);
                }
            }
        }
    }

    public string TestVerbDictionaryWithNoun (Dictionary<string,string> verbDictionary, string verb, string noun)
    {
        if (verbDictionary.ContainsKey(noun))
        {
            return verbDictionary[noun];
        }
        else
        {
            return $"You can't {verb} {noun}";
        }
    }

    public void ClearCollectionsForNewRoom()
    {
        roomNavigation.ClearExits();
        interactionDescriptionsInRoom.Clear();
        itemManager.ClearCollections();
    }
}
