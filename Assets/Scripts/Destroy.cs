using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="TextAdventure/InputActions/Destroy")]
public class Destroy : InputAction
{
    public override void RespondToInput(GameController controller, string[] separatedInputWords)
    {
        controller.itemManager.DestroyNounInInventory(separatedInputWords);
    }
}
