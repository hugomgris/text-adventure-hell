using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Exit 
{
    public string directionNoun;
    [TextArea] public string exitDescription;
    public Room valueRoom;
}
