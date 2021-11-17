using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName =("TextAdventure/ActionResponses/CombineSkullWithGemstones"))]
public class CombineSkullWithGemstonesActionResponse : ActionResponse
{
    public InteractableObjects object1;
    public InteractableObjects object2;
    public InteractableObjects combinationResult;
    public override bool DoActionResponse(GameController controller)
    {
        controller.LogStringWithReturn("You combined the item");
        controller.itemManager.nounsInInventory.Remove(object1.noun);
        controller.itemManager.nounsInInventory.Remove(object2.noun);
        controller.itemManager.nounsInInventory.Add(combinationResult.noun);
        controller.itemManager.AddActionResponsesToDictionaries();
        return true;
    }
}
