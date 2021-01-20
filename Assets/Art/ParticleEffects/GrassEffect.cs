using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassEffect : MonoBehaviour
{
    [SerializeField]
    GameObject grassParticles;

    [SerializeField]
    GameObject snowParticles;

    [SerializeField]
    GameObject jungleParticles;

    GameObject newGrass;

    public void spawnGrass()
    {
        if (Player.instance.sceneLoader.getCurrentScene().particleEffect != SceneLoader.Particle.NONE)
        {
            if (Player.instance.sceneLoader.getCurrentScene().particleEffect == SceneLoader.Particle.GRASS)
            {
                newGrass = Instantiate(grassParticles);
                Player.instance.audioHandler.playGrassStep();
            }
            else if (Player.instance.sceneLoader.getCurrentScene().particleEffect == SceneLoader.Particle.SNOW)
            {
                newGrass = Instantiate(snowParticles);
                Player.instance.audioHandler.playSnowStep();
            }
            else if(Player.instance.sceneLoader.getCurrentScene().particleEffect == SceneLoader.Particle.JUNGLE)
            {
                newGrass = Instantiate(jungleParticles);
                Player.instance.audioHandler.playGrassStep();
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
