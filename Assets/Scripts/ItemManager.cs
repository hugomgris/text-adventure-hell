using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [HideInInspector] public GameController controller;
    public List<InteractableObjects> usableItemList = new List<InteractableObjects>();
    public List<Room> totalGameRooms = new List<Room>();
    [HideInInspector] public List<string> nounsInRoom = new List<string>();
    [HideInInspector] public List<string> nounsInInventory = new List<string>();
    public Dictionary<string, string> examineDictionary = new Dictionary<string, string>();
    public Dictionary<string, string> takeDictionary = new Dictionary<string, string>();
    public Dictionary<string, ActionResponse> useDictionary = new Dictionary<string, ActionResponse>();
    public Dictionary<string, ActionResponse> combineDictionary = new Dictionary<string, ActionResponse>();


    private void Awake()
    {
        controller = GetComponent<GameController>();
        for (int i = 0; i < totalGameRooms.Count; i++)
        {
            for (int j = 0; j < totalGameRooms[i].interactableObjects.Length; j++)
            {
                totalGameRooms[i].interactableObjects[j].isTaken = false;
            }
        }
    }

    public string GetObjectsNotInInventory(Room currentRoom, int i)
    {
        InteractableObjects interactableObjectInRoom = currentRoom.interactableObjects[i];
        if (!nounsInInventory.Contains(interactableObjectInRoom.noun))
        {
            if (interactableObjectInRoom.isTaken == false)
            { 
            nounsInRoom.Add(interactableObjectInRoom.noun);
            return interactableObjectInRoom.objectDescription;
            }
        }
        return null;
    }

    public Dictionary<string,string> Take(string[] separatedInputWords)
    {
        string noun = separatedInputWords[1];
        if (nounsInRoom.Contains(noun))
        {
            nounsInRoom.Remove(noun);
            nounsInInventory.Add(noun);
            for (int i = 0; i < controller.roomNavigation.currentRoom.interactableObjects.Length; i++)
            {
                if(controller.roomNavigation.currentRoom.interactableObjects[i].noun==noun)
                {
                    controller.roomNavigation.currentRoom.interactableObjects[i].isTaken = true;
                }
            }
            AddActionResponsesToDictionaries();
            return takeDictionary;
        }
        else
        {
            controller.LogStringWithReturn($"There is no {noun} here to take, bro.");
            return null;
        }
    }

    public void DestroyNounInInventory(string[] separatedInputWords)
    {
        string noun = separatedInputWords[1];
        if (nounsInInventory.Contains(noun))
        {
            nounsInInventory.Remove(noun);
        }
        else
            controller.LogStringWithReturn($"There is no {noun} in your inventory to destroy");
    }


    public void DisplayInventory()
    {
        if (nounsInInventory.Count > 0)
        {
            controller.LogStringWithReturn("You open your backpack. Inside you find: ");
            for (int i = 0; i < nounsInInventory.Count; i++)
            {
                controller.LogStringWithReturn(nounsInInventory[i]);
            }
        }
        else
            controller.LogStringWithReturn("Your backpack is empty, bro.");
    }

    public void AddActionResponsesToDictionaries()
    {
        for (int i = 0; i < nounsInInventory.Count; i++)
        {
            string noun = nounsInInventory[i];
            InteractableObjects interactableObjectInInventory = GetInteractableObjectFromusableList(noun);
            if (interactableObjectInInventory == null)
            {
                continue;
            }
            else

            for (int j = 0; j < interactableObjectInInventory.interactions.Length; j++)
            {
                Interaction interaction = interactableObjectInInventory.interactions[j];

                if (interaction.actionResponse==null)
                {
                    continue;
                }

                if (interaction.actionResponse.actionKey == "use") 
                    { 
                    
                    if (!useDictionary.ContainsKey(noun))
                    {
                        useDictionary.Add(noun, interaction.actionResponse);
                    }
                    }

                if (interaction.actionResponse.actionKey == "combine")
                    {
                        if (!combineDictionary.ContainsKey(noun))
                        {
                            combineDictionary.Add(noun, interaction.actionResponse);
                        }
                    }
                }
        }
    }

    InteractableObjects GetInteractableObjectFromusableList (string noun)
    {
        for (int i = 0; i < usableItemList.Count; i++)
        {
            if (usableItemList[i].noun == noun)
            {
                return usableItemList[i];
            }
        }
        return null;
    }

    public void ClearCollections()
    {
        nounsInRoom.Clear();
        examineDictionary.Clear();
        takeDictionary.Clear();
        combineDictionary.Clear();
    }

    public void UseItem(string[] separatedInputWords)
    {
        string nounToUse = separatedInputWords[1];

        if (nounsInInventory.Contains(nounToUse))
        {
            if (useDictionary.ContainsKey(nounToUse))
            {
                bool actionResult = useDictionary[nounToUse].DoActionResponse(controller);
                if (!actionResult)
                {
                    controller.LogStringWithReturn("Hmm... Nothing happened...");
                }
            }else
                controller.LogStringWithReturn($"You can't use the {nounToUse}");
        }
        else
            controller.LogStringWithReturn($"You don't have a {nounToUse} to use.");
    }

    public void CombineItems(string[] separatedInputWords)
    {
        string noun1 = separatedInputWords[1];
        string noun2 = separatedInputWords[3];

        if (nounsInInventory.Contains(noun1) && nounsInInventory.Contains(noun2))
        {
            if (combineDictionary.ContainsKey(noun1) && combineDictionary.ContainsKey(noun2))
            {
                bool actionResult = combineDictionary[noun1].DoActionResponse(controller);
                if (!actionResult)
                {
                    controller.LogStringWithReturn("fallo tipo 1");
                }
            }else
            controller.LogStringWithReturn("fallo tipo 2");
        }else
            controller.LogStringWithReturn("fallo tipo 3");
    }
}

