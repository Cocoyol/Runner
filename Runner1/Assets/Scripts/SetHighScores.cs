using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetHighScores : MonoBehaviour {

    public Text maxPoints;
    public Text maxItems;
    public Text maxLevel;
    public Text points;
    public Text items;
    public Text level;

    public string highScoresFilename = "datos.dat";

    public GameData highScores;

    public bool loaded = false;

    private void OnEnable() {
        if (!loaded || GameParameters.updatedScores) {
            GameParameters.updatedScores = false;
            LoadHighScores();
            loaded = true;
        }
        maxPoints.text = Mathf.Round(highScores.maxPoints).ToString();
        maxItems.text = highScores.maxItemsTaken.ToString();
        maxLevel.text = highScores.maxLevel.ToString();
        points.text = Mathf.Round(highScores.lastPoints).ToString();
        items.text = highScores.lastItemsTaken.ToString();
        level.text = highScores.lastLevel.ToString();
    }

    public void LoadHighScores() {
        Debug.Log("Cargando Puntuaciones...");
        FileManager fm = GetComponent<FileManager>();
        fm.filePath = Application.persistentDataPath + "/" + highScoresFilename;

        if(highScores == null)
            highScores = new GameData();
        int fst = fm.LoadGameData(ref highScores);
        // Checar posibles problemas
        switch (fst) {
            case 1:
                Debug.Log("Ocurrió un problema: Archivo no existe.");
                break;
            case 2:
                Debug.Log("Ocurrió un problema: Archivo corrupto.");
                break;
            default:
                break;
        }
    }
}
