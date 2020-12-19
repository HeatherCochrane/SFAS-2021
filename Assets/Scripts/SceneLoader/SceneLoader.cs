﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader instance;

    [System.Serializable]
    public struct SceneData
    {
        public string scenePath;
        public Vector2 horizontal;
        public Vector2 vertical;
        public Particle particleEffect;

    }

    public enum Particle { GRASS, SNOW};
    [SerializeField]
    List<SceneData> AllSceneData = new List<SceneData>();

    string sceneToLoad;

    int sceneSelected = 0;

    [SerializeField]
    GameObject player;

    Animator anim;

    [SerializeField]
    TextMeshProUGUI currentScene;

    //Trader Canvas
    public GameObject traderSlotParent;

    //Trader Info Box
    public GameObject traderInfoBox;

    public GameObject traderButton;

    public GameObject leaveButton;

    [SerializeField]
    List<Sprite> gateSprites = new List<Sprite>();

    SceneData current;

    Vector2 newSpawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.FindObjectOfType<SceneLoader>() != this)
        {
            Destroy(GameObject.FindObjectOfType<SceneLoader>().gameObject);
        }

        instance = this;
        anim = this.GetComponent<Animator>();

        current = AllSceneData[1];
    }


    void loadSetScene()
    {
        SceneManager.LoadScene(sceneToLoad);
        Debug.Log(sceneToLoad);

        foreach(SceneData scene in AllSceneData)
        {
            if(scene.scenePath == sceneToLoad)
            {
                Player.instance.transform.position = newSpawnPoint;
                Player.instance.setCamBounds(scene.horizontal, scene.vertical);
                current = scene;
                break;
            }
        }

        UnityEngine.EventSystems.EventSystem.current = Player.instance.system;

        anim.SetTrigger("FadeIn");
        Player.instance.setInput(true);
        
    }

    public void switchScene(string scene, Vector2 newSpawn)
    {
        sceneToLoad = scene;
        newSpawnPoint = newSpawn;
        loadScene();
    }

    public void startGame(string scene)
    {
        sceneToLoad = scene;
        newSpawnPoint = new Vector2(-6, -2);
        loadScene();
    }

    public void loadScene()
    {
        Player.instance.setInput(false);
        anim.SetTrigger("FadeOut");
    }

    public SceneData getCurrentScene()
    {
        return current;
    }
}
