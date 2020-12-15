using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassEffect : MonoBehaviour
{
    [SerializeField]
    GameObject grassParticles;

    GameObject newGrass;

    public void spawnGrass()
    {
        newGrass = Instantiate(grassParticles);
        newGrass.transform.SetParent(Player.instance.transform);
        newGrass.transform.position = Player.instance.transform.position - new Vector3(0, 1);
    }
}
