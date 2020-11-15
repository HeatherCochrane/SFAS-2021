using System;
using UnityEngine;

[Serializable]
public class ChoiceData
{
    [SerializeField] private string _text;
    [SerializeField] private int _beatId;
    [SerializeField] public Quest quest;
    public string DisplayText { get { return _text; } }
    public int NextID { get { return _beatId; } }
}
