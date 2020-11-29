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

        if (currentScene != null)
        {
            currentScene.text = "Player Home";
        }

    }


    void loadSetScene()
    {
        SceneData d = AllSceneData.Find(x => x.scenePath == sceneToLoad);

        SceneManager.LoadScene(sceneToLoad);

        if (player != null)
        {
            if (d.spawnPoint != null)
            {
                player.transform.position = d.spawnPoint;
            }
            else
            {
                player.transform.position = new Vector2(0, 0);
            }
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
        currentScene.text = sceneToLoad;
    }

    public void returnHome()
    {
        sceneToLoad = "PlayerHome";
        currentScene.text = "Player Home";
        anim.SetTrigger("FadeOut");
    }

    public void loadScene()
    {
        anim.SetTrigger("FadeOut");
    }

}
