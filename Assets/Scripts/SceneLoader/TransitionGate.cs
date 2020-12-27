using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionGate : MonoBehaviour
{
    [SerializeField]
    string scene;

    [SerializeField]
    string spawn;

    public string getScene()
    {
        return scene;
    }

    public string getSpawn()
    {
        return spawn;
    }
}
