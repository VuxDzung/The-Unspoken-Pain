using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class Dialogue : ScriptableObject
{
    public List<Dialogue> subDialogues;
    public string characterName;
    public Sprite characterImage;
    public Image backGroundImage;
    public string dialogue;
}
