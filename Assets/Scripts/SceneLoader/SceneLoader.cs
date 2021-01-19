using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Tilemaps;

public class SceneLoader : MonoBehaviour
{
    IEnumerator checkSceneLoaded()
    {
        while (!SceneManager.GetSceneByName(sceneToLoad).isLoaded)
        {
            yield return new WaitForEndOfFrame();
        }

        GameObject m = GameObject.FindGameObjectWithTag("MiniMap");

        if (m != null)
        {
            Player.instance.uiHandler.setCurrentMap(m);
        }

        map = GameObject.FindGameObjectWithTag("ColliderMap").GetComponentInChildren<Tilemap>();
        mapSizeX = new Vector2(map.cellBounds.xMin + 10, map.cellBounds.xMax - 10);
        mapSizeY = new Vector2(map.cellBounds.yMin + 5, map.cellBounds.yMax - 5);
        Player.instance.setCamBounds(mapSizeX, mapSizeY);

        GameObject[] allSpawns = GameObject.FindGameObjectsWithTag("Spawn");

        foreach(GameObject spawn in allSpawns)
        {
            if(spawn.GetComponent<PlayerSpawns>().spawn == gate.getSpawn())
            {
                Player.instance.transform.position = spawn.transform.position;
            }
        }

        if(current.character != null)
        {
            Player.instance.uiHandler.changeMenu(UIHandler.Menus.DIALOGUE);
            Player.instance.dialogue.startNewDialogue(current.character.getData().getDialogue(0), current.character.getData().getCharacterSprite(), current.character.getData().getName(), Player.instance.uiHandler.getMenuObject(UIHandler.Menus.DIALOGUE));
            Player.instance.setInConvo();
            Player.instance.dialogue.switchSceneOnEnd(current.switchScene, current.gate);
        }
        
    }



    public static SceneLoader instance;

    [System.Serializable]
    public struct SceneData
    {
        public string scenePath;
        public Particle particleEffect;
        public Character character;
        public string switchScene;
        public TransitionGate gate;
    }

    public enum Particle { GRASS, SNOW, JUNGLE, NONE};
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


    Vector2 mapSizeX;
    Vector2 mapSizeY;
    Tilemap map;

    TransitionGate gate;
    
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

        foreach (SceneData scene in AllSceneData)
        {
            if(scene.scenePath == sceneToLoad)
            {
                Player.instance.transform.position = newSpawnPoint;
                current = scene;
                break;
            }
        }

        StartCoroutine(checkSceneLoaded());


        UnityEngine.EventSystems.EventSystem.current = Player.instance.system;

        anim.SetTrigger("FadeIn");
        Player.instance.setInput(true);
        
    }

    public void respawnPlayer()
    {
        Player.instance.setInput(true);
        anim.SetTrigger("FadeIn");

        GameObject[] allSpawns = GameObject.FindGameObjectsWithTag("Spawn");

        foreach (GameObject spawn in allSpawns)
        {
            if (spawn.GetComponent<PlayerSpawns>().spawn == "First")
            {
                Player.instance.transform.position = spawn.transform.position;
                return;
            }
        }
    }

    public void switchScene(string scene, TransitionGate g)
    {
        sceneToLoad = scene;
        gate = g;
        loadScene();
    }

    public void startGame(string scene)
    {
        sceneToLoad = scene;
        gate = GetComponent<TransitionGate>();
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
