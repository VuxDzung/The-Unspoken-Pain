using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class Dialogue : ScriptableObject
{
    public List<Dialogue> subDialogues;
    public string nodeID;
    public string characterName;
    public Image characterImage;
    public string dialouge;
}
