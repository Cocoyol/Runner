using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SceneController))]
public class InitScene : MonoBehaviour {

    private SceneController sceneController;

    private void Awake() {
        sceneController = GetComponent<SceneController>();

        GameParameters gp = GameObject.Find("GameParams").GetComponent<GameParameters>();
        int startMode = gp.startMode;

        if (startMode == 0) {            // Modo "iniciar Nuevo juego"
            Debug.Log("Iniciar Nueva partida...");
            gp.startMode = 1;
        } else if(startMode == 1) {     // Modo "cargar partida"
            Debug.Log("Cargando Datos...");
            if (LoadGame())
                ApplyGameData();
            else
                Debug.Log("No se pudo cargar el archivo.");
        }
    }

    // * Cargar los datos desde archivo
    public bool LoadGame() {
        bool result = false;
        int fst = sceneController.fm.LoadGameData(ref sceneController.gData);

        // Checar posibles problemas
        switch(fst) {
            case 1:
                Debug.Log("Ocurrió un problema: Archivo no existe.");
                break;
            case 2:
                Debug.Log("Ocurrió un problema: Archivo corrupto.");
                break;
            default:
                result = true;
                break;
        }

        return result;
    }

    // * Inicializa todos los valores CARGADOS en las variables del juego
    public void ApplyGameData() {
        //Iniciar puntuaciones máximas
        InitScores();

        // ... Iniciar todo lo demás...
    }

    public void InitScores() {
        Scores scores = GetComponent<Scores>();
        if (scores != null) {
            scores.InitScores(sceneController.gData.maxLevel, sceneController.gData.maxPoints, sceneController.gData.maxAlivePoints, sceneController.gData.maxItemPoints, sceneController.gData.maxItemsTaken);
            //Debug.Log("Puntos máximos: "+gData.maxPoints);
        }
    }

}
