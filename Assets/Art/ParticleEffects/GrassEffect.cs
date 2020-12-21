using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassEffect : MonoBehaviour
{
    [SerializeField]
    GameObject grassParticles;

    [SerializeField]
    GameObject snowParticles;

    GameObject newGrass;

    public void spawnGrass()
    {
        if (Player.instance.sceneLoader.getCurrentScene().particleEffect != SceneLoader.Particle.NONE)
        {
            if (Player.instance.sceneLoader.getCurrentScene().particleEffect == SceneLoader.Particle.GRASS)
            {
                newGrass = Instantiate(grassParticles);
            }
            else if (Player.instance.sceneLoader.getCurrentScene().particleEffect == SceneLoader.Particle.SNOW)
            {
                newGrass = Instantiate(snowParticles);
            }


            newGrass.transform.SetParent(Player.instance.transform);
            newGrass.transform.position = Player.instance.transform.position - new Vector3(0, 1);
        }
    }

    public void attack()
    {
        Player.instance.longRanged();
    }

    public void spawnArrow()
    {
        Player.instance.spawnArrow();
    }
}
