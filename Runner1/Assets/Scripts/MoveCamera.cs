
using UnityEngine;

public class MoveCamera : MonoBehaviour {
    
    public float baseSpeed;
    private float speed;

    private bool _gameStarted = false;

    public bool gameStarted {
        get {
            return _gameStarted;
        }

        set {
            _gameStarted = value;
        }
    }

    private void Awake() {
#if UNITY_EDITOR
        QualitySettings.vSyncCount = 0;  // VSync must be disabled
        Application.targetFrameRate = 60;
#endif

        UpdateLevelParameters();

        AnimateBackground();
    }

    private void FixedUpdate () {
        if (_gameStarted) {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
            
    }

    public void UpdateLevelParameters() {
        speed = baseSpeed + (GameParameters.level - 1.0f) * 0.75f;
        Debug.Log("Camera speed: " + speed);
    }

    public void StartGame() {
        _gameStarted = true;
        GameObject.FindGameObjectWithTag("bgMontanas").GetComponent<BackgroundAnimation>().speed = 0.001f;
    }

    private void AnimateBackground() {
        GameObject.FindGameObjectWithTag("bgNiebla2").GetComponent<BackgroundAnimation>().speed = 0.022f;
        GameObject.FindGameObjectWithTag("bgNiebla1").GetComponent<BackgroundAnimation>().speed = 0.012f;
        GameObject.FindGameObjectWithTag("bgNubes").GetComponent<BackgroundAnimation>().speed = 0.008f;
    }

}
