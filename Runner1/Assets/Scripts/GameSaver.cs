using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SceneController))]
public class GameSaver : MonoBehaviour {

    private SceneController sceneController;

    private void Awake() {
        sceneController = GetComponent<SceneController>();
    }

    // Guardar los datos en archivo
    public void SaveGame() {
        if (FillGameData()) {                                   // Si pudo recoger todos los datos necesarrios?
            sceneController.fm.SaveGameData(ref sceneController.gData);
        }
    }

    // Llena la clase que se va a GUARDAR
    private bool FillGameData() {
        bool result = false;

        Scores scores = GetComponent<Scores>();
        if (scores != null) {
            //fm.LoadGameData(ref gData);                       // <-- Es necesario???

            sceneController.gData.lastLevel = scores.level;
            sceneController.gData.lastAlivePoints = scores.alivePoints;
            sceneController.gData.lastItemPoints = scores.itemPoints;
            sceneController.gData.lastItemsTaken = scores.itemsTaken;
            sceneController.gData.lastPoints = scores.points;

            if (scores.level > scores.maxLevel) {
                scores.maxLevel = scores.level;
                GameParameters.newMaxLevel = true;
            }
            sceneController.gData.maxLevel = scores.maxLevel;

            if (scores.alivePoints > scores.maxAlivePoints) {
                scores.maxAlivePoints = scores.alivePoints;
            }
            sceneController.gData.maxAlivePoints = scores.maxAlivePoints;

            if (scores.itemPoints > scores.maxItemPoints) {
                scores.maxItemPoints = scores.itemPoints;
            }
            sceneController.gData.maxItemPoints = scores.maxItemPoints;

            if (scores.itemsTaken > scores.maxItemsTaken) {
                scores.maxItemsTaken = scores.itemsTaken;
                GameParameters.newMaxItems = true;
            }
            sceneController.gData.maxItemsTaken = scores.maxItemsTaken;

            if (scores.points > scores.maxPoints) {
                scores.maxPoints = scores.points;
                GameParameters.newMaxPoints = true;
            }
            sceneController.gData.maxPoints = scores.maxPoints;

            GameParameters.updatedScores = true;
            result = true;
        }

        return result;
    }
}
