using System.Collections;
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
        public Vector3 spawnPoint;
        public float left;
        public float right;
    }

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

    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.FindObjectOfType<SceneLoader>() != this)
        {
            Destroy(GameObject.FindObjectOfType<SceneLoader>().gameObject);
        }

        instance = this;
        anim = this.GetComponent<Animator>();

    }


    void loadSetScene()
    {
        SceneManager.LoadScene(sceneToLoad);
        Debug.Log(sceneToLoad);

        foreach(SceneData scene in AllSceneData)
        {
            if(scene.scenePath == sceneToLoad)
            {
                Player.instance.transform.position = scene.spawnPoint;
                Player.instance.setCamBounds(scene.left, scene.right);
                break;
            }
        }

        UnityEngine.EventSystems.EventSystem.current = Player.instance.system;

        anim.SetTrigger("FadeIn");
        Player.instance.setInput(true);
    }

    public void switchSceneToLoad(GameObject g)
    {
        sceneSelected += 1;
        if(sceneSelected > AllSceneData.Count - 1)
        {
            sceneSelected = 0;
        }
        if (sceneSelected < gateSprites.Count)
        {
            g.GetComponent<SpriteRenderer>().sprite = gateSprites[sceneSelected];
        }

        sceneToLoad = AllSceneData[sceneSelected].scenePath;
    }

    public void switchScene(string scene)
    {
        sceneToLoad = scene;
        loadScene();
    }

    public void loadScene()
    {
        Player.instance.setInput(false);
        anim.SetTrigger("FadeOut");
    }

}
