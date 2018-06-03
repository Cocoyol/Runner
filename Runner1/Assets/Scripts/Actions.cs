
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Actions : MonoBehaviour {

    [SerializeField]
    private AudioSource startButtonSound;
    [SerializeField]
    private GameObject modalSalir;

    private bool keyMusic = true;
    private bool keySounds = true;

    private void Update() {
    #if UNITY_STANDALONE || UNITY_WEBPLAYER

        // Tecla Salir (Fijado en ESC)
        if (Input.GetKeyUp(KeyCode.Escape)) {
            GetComponent<ModalsController>().CloseOrExitModal(modalSalir);
        }

        // Teclas de Sonidos
        if (Input.GetButton("Music")) {
            if(keyMusic)
                GetComponent<AudioManager>().ToogleMusicVolume();
            keyMusic = false;
        } else {
            keyMusic = true;
        }
        if (Input.GetButton("Sounds")) {
            if(keySounds)
                GetComponent<AudioManager>().ToogleSoundsVolume();
            keySounds = false;
        } else {
            keySounds = true;
        }

    #endif
    }

    public void StartGame() {
        StartCoroutine(ChangeScene(1));
    }

    public void QuitGame() {
        Application.Quit();
    }

    public void ReturnToMenu() {
        StartCoroutine(ChangeScene(0));
    }

    public void OpenModal(GameObject go) {
        GetComponent<ModalsController>().ShowModal(go);
    }

    IEnumerator ChangeScene(int scene) {
        startButtonSound.Play();
        GameParameters.fadeSpeed = startButtonSound.clip.length * 1.15f;

        Button[] buttons = FindObjectsOfType<Button>();
        foreach(Button button in buttons) {
            button.interactable = false;
        }

        FadingEffect fading = GetComponent<FadingEffect>();
        fading.fadeSpeed = GameParameters.fadeSpeed;
        fading.BeginFade(1);
        
        yield return new WaitForSeconds(GameParameters.fadeSpeed);

        SceneManager.LoadScene(scene);
    }
}
