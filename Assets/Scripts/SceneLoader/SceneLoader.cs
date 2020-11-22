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

    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.FindObjectOfType<SceneLoader>() != this)
        {
            Destroy(GameObject.FindObjectOfType<SceneLoader>().gameObject);
        }

        instance = this;
        anim = this.GetComponent<Animator>();
        currentScene.text = "Player Home";
    }


    void loadSetScene()
    {
        SceneData d = AllSceneData.Find(x => x.scenePath == sceneToLoad);

        SceneManager.LoadScene(sceneToLoad);
        player.transform.position = d.spawnPoint;

        anim.SetTrigger("FadeIn");
    }

    public void switchSceneToLoad()
    {
        sceneSelected += 1;

        if(sceneSelected > AllSceneData.Count - 1)
        {
            sceneSelected = 0;
        }

        sceneToLoad = AllSceneData[sceneSelected].scenePath;
        currentScene.text = sceneToLoad;
    }

    public void returnHome()
    {
        sceneToLoad = "PlayerHome";
        anim.SetTrigger("FadeOut");
    }
    public void loadScene()
    {
        anim.SetTrigger("FadeOut");
    }

}
