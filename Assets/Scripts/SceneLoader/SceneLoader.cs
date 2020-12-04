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
        SceneData d = AllSceneData.Find(x => x.scenePath == sceneToLoad);

        SceneManager.LoadScene(sceneToLoad);
        Debug.Log(sceneToLoad);

        if (sceneToLoad != "PlayerHome")
        {
            player.transform.position = d.spawnPoint;
            Player.instance.setCamBounds(d.left, d.right);
        }
        else
        {
            player.transform.position = new Vector2(15, 0);
            Player.instance.setCamBounds(-10f, 10);

            sceneSelected = 1;
            sceneToLoad = AllSceneData[1].scenePath;
        }


        UnityEngine.EventSystems.EventSystem.current = Player.instance.system;

        anim.SetTrigger("FadeIn");
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
        anim.SetTrigger("FadeOut");
    }

}
