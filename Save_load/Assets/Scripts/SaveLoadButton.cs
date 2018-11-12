using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadButton : MonoBehaviour {

	// Use this for initialization
	public void Save()
    {
        GameController.control.SaveGame();
        SceneController.sceneControl.SaveSceneData();
    }
	
	// Update is called once per frame
	public void Load() {
        GameController.control.LoadGame();
        SceneController.sceneControl.LoadSceneData();
	}
}
