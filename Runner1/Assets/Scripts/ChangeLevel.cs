using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeLevel : MonoBehaviour {

    public GameObject levelTitle;

    public MoveCamera cameraC;
    public PlatformGenerator platformC1;
    public PlatformGenerator platformC2;
    public Character characterC;

    private Animator startLevelAnim;
    private Text levelTitleText;
    private Transform cameraPos;

    private float changelevelFactor = 300;
    private float startLevelPos;

    private void Awake() {
        startLevelPos = changelevelFactor;
        cameraPos = Camera.main.transform;

        startLevelAnim = levelTitle.GetComponent<Animator>();
        levelTitleText = levelTitle.GetComponent<Text>();

        startLevelAnim.Play("PresentacionNivel");
        Debug.Log(cameraPos.position.x + " proximo: " + startLevelPos);
    }

    private void Update() {
        
        if(startLevelPos < cameraPos.position.x && GameParameters.level < 10 && GameParameters.alive) {
            // Cambiar nivel
            GameParameters.level++;

            cameraC.UpdateLevelParameters();
            platformC1.UpdateLevelParameters();
            platformC2.UpdateLevelParameters();
            characterC.UpdateLevelParameters();

            levelTitleText.text = "Nivel  " + GameParameters.level.ToString().PadLeft(1,'0');
            GameObject.Find("LevelText").GetComponent<Text>().text = "Dificultad " + GameParameters.level.ToString().PadLeft(1, '0');
            startLevelAnim.Play("PresentacionNivel");

            startLevelPos += changelevelFactor * (1.0f + GameParameters.level / 9.0f);
            Debug.Log(cameraPos.position.x + " proximo: " + startLevelPos);

            GetComponent<Scores>().level = GameParameters.level;
        }
    }
}
