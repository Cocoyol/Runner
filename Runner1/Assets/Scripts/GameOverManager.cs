
using UnityEngine;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour {

    [SerializeField]
    private GameObject gameOverScreen;

    [SerializeField]
    private Text puntuacion;
    [SerializeField]
    private Text items;
    [SerializeField]
    private Text maxPuntuacion;
    [SerializeField]
    private Text maxItems;

    [SerializeField]
    private GameObject newPoints;
    [SerializeField]
    private GameObject newItems;

    [SerializeField]
    private AudioSource loseSound;
    [SerializeField]
    private AudioClip loseMusic;

    private AudioSource mainAudio;

    private void Awake() {
        mainAudio = Camera.main.GetComponent<AudioSource>();
    }

    // <-- No es obligatorio, pero es una BUENA PRÁCTICA poner el EventHandler en "OnEnable"
    private void OnEnable() {
        Character.OnDead += ActivateGameOverScreen;
    }

    private void OnDisable() {
        Character.OnDead -= ActivateGameOverScreen;
    }
    // -->

    public void ActivateGameOverScreen() {
        //StartCoroutine(TriggerScreen());
        ShowGameOverScreen();
    }

    /*IEnumerator TriggerScreen() {
        AudioSource mainAudio = Camera.main.GetComponent<AudioSource>();
        if (mainAudio != null)
            mainAudio.Stop();
        loseSound.Play();
        yield return new WaitForSeconds(loseSound.clip.length);
        ShowGameOverScreen();
    }*/

    public void PlayLoseSound() {
        if (mainAudio != null)
            mainAudio.Stop();
        loseSound.Play();
    }

    public void ShowGameOverScreen() {
        PlayLoseSound();

        gameOverScreen.SetActive(true);
        GameObject.Find("ScoreText").SetActive(false);
        GameObject.Find("LevelText").SetActive(false);

        GetComponent<GameSaver>().SaveGame();

        puntuacion.GetComponent<Text>().text = Mathf.Round(GetComponent<Scores>().points).ToString();
        items.GetComponent<Text>().text = GetComponent<Scores>().itemsTaken.ToString();
        maxPuntuacion.GetComponent<Text>().text = Mathf.Round(GetComponent<Scores>().maxPoints).ToString();
        maxItems.GetComponent<Text>().text = GetComponent<Scores>().maxItemsTaken.ToString();
        if (!GameParameters.newMaxPoints) {
            newPoints.SetActive(false);
        }
        if(!GameParameters.newMaxItems) {
            newItems.SetActive(false);
        }

        mainAudio.clip = loseMusic;
        mainAudio.Play();
    }
}
