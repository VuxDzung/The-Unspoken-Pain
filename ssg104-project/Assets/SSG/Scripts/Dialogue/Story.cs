using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Story : ScriptableObject
{
    public Dialogue[] dialogues;
    internal List<Branch> decisionBranches = new List<Branch>();
    internal bool isBuilt = false;
}
