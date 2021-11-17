using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="TextAdventure/ScriptableObject")]
public class InteractableObjects : ScriptableObject
{
    public string noun;
    public string objectDescription = "description displayed in room";

    public Interaction[] interactions;
}
