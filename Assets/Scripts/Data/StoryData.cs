using System;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[Serializable]
[CreateAssetMenu(fileName = "StoryData", menuName = "New story data/StoryData", order = 1)]
public class StoryData : ScriptableObject
{
    [SerializeField] private List<BeatData> _beats;
    public BeatData GetBeatById( int id )
    {
        return _beats.Find(b => b.ID == id);
    }

#if UNITY_EDITOR

    public static StoryData LoadData(string d)
    {
        StoryData data = AssetDatabase.LoadAssetAtPath<StoryData>(d);
        if (data == null)
        {
            data = CreateInstance<StoryData>();
            AssetDatabase.CreateAsset(data, d);
        }

        return data;
    }

    public string getFilePath()
    {
        return AssetDatabase.GetAssetPath(this);
    }


#endif
}

