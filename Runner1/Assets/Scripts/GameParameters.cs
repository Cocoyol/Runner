using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameParameters : MonoBehaviour {

    public static GameParameters parameters;

    public static float fadeSpeed = 0.7f;
    public static bool isFirstScene = true;

    public static int level = 1;
    //public static float speed = 4f;

    public static bool alive = true;

    public static bool sounds = true;
    public static bool music = true;
    public static bool paused = false;

    public static bool updatedScores = false;
    public static bool newMaxLevel = false;
    public static bool newMaxItems = false;
    public static bool newMaxPoints = false;

    public string dataFilename = "datos.dat";

    public int startMode = 1;                   // 0 = Nuevo , 1 = Cargar    *No implementado

    private void Awake() {
        
        if (parameters == null) {
            DontDestroyOnLoad(gameObject);
            parameters = this;
        } else if (parameters != this) {
            Debug.Log("Nueva escena!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            RestartVarsOnChange();
            //ResetSpeed();

            isFirstScene = false;
            Destroy(gameObject);
        }
    }

    /*public static void ResetSpeed() {
        speed = 4.0f + (level - 1) * 0.75f;
        Debug.Log("PRIMARY SPEED: " + speed);
    }*/

    private void RestartVarsOnChange() {
        level = 1;
        paused = updatedScores = newMaxLevel = newMaxItems = newMaxPoints = false;
    }

}
