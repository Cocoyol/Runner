using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour {

    public FileManager fm;
    public GameData gData;

    private void Awake() {

        fm = GetComponent<FileManager>();

        string dataFilename = GameObject.Find("GameParams").GetComponent<GameParameters>().dataFilename;
        string filePath = Application.persistentDataPath + "/" + dataFilename;
        fm.filePath = filePath;

        gData = new GameData();
    }
}
