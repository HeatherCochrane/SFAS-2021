using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionGate : MonoBehaviour
{
    [SerializeField]
    string scene;

    [SerializeField]
    Vector2 spawnPoint;

    public string getScene()
    {
        return scene;
    }

    public Vector2 getSpawnPoint()
    {
        return spawnPoint;
    }
}
