using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="TextAdventure/InputActions/Combine")]
public class Combine : InputAction
{
    public override void RespondToInput(GameController controller, string[] separatedInputWords)
    {
        controller.itemManager.CombineItems(separatedInputWords);
    }
}
