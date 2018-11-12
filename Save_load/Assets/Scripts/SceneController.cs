using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

public class SceneController : MonoBehaviour {

    public static SceneController sceneControl;
    
    public int IndexScene;

    private void Awake() {
        if(sceneControl == null) {
            DontDestroyOnLoad(gameObject);
            sceneControl = this;
            try
            {
                LoadSceneData();
            }
            catch
            {
                SetDefault();
            }
        } else if(sceneControl != this) {
            Destroy(gameObject);
        }
    }

    public void SetDefault()
    {
        print("set defaut");
        IndexScene = 0;
    }

    public void NextScene() {
        if ((SceneManager.sceneCountInBuildSettings-1) > SceneManager.GetActiveScene().buildIndex) {
            print("loading " + (SceneManager.GetActiveScene().buildIndex + 1));
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        } else {
            print("This is the last scene");
        }
        
    }

    public void PreviousScene() {
        if (SceneManager.GetActiveScene().buildIndex != 0) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
            print("loading " + (SceneManager.GetActiveScene().buildIndex -1 ));
            
        } else {
            print("This is the first scene");
        }
        
    }
    private void OnGUI() {
        GUIStyle style = new GUIStyle();
        style.fontSize = 56;
        GUI.Label(new Rect(10, 10, 180, 80), "Active scene index : "  + SceneManager.GetActiveScene().buildIndex, style);
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(IndexScene);
    }


    public void SaveSceneData()
    {
        FileStream file = File.Open(Application.persistentDataPath + "/sceneInfo.dat", FileMode.Create);
        SceneData data = new SceneData();
        data.indexScene = SceneManager.GetActiveScene().buildIndex;
        BinaryFormatter bf = new BinaryFormatter();
        print(data.indexScene);
        bf.Serialize(file, data);
        file.Close();
    }

    public void LoadSceneData()
    {
        BinaryFormatter bf = new BinaryFormatter();
        if(!File.Exists(Application.persistentDataPath + "/sceneInfo.dat"))
        {
            throw new Exception("Scene file does not exist");
        }
        FileStream file = File.Open(Application.persistentDataPath + "/sceneInfo.dat", FileMode.Open);
        SceneData data = (SceneData)bf.Deserialize(file);

        IndexScene = data.indexScene;
        file.Close();
        LoadScene();
    }

   

}

[Serializable]
class SceneData
{
    public int indexScene;
}
