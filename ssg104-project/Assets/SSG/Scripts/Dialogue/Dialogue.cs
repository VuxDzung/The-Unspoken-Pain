using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class Dialogue : ScriptableObject
{
    public List<Dialogue> subDialogues;
    public string[] nameOfTriggedItems;
    public string characterName;
    public Sprite characterImage;
    public string dialogue;
    public bool isPlayerDialogue;
}
