
using UnityEngine;

public class ModalsController : MonoBehaviour {

    public GameObject[] modals;

    //public static GameObject currentModal;

    public void ShowModal(GameObject go) {
        PauseGame();
        foreach (GameObject modal in modals) {
            if (modal.GetInstanceID() != go.GetInstanceID()) {
                modal.SetActive(false);
            }
        }

        go.SetActive(true);
        //currentModal = go;
    }

    public void CloseModal(GameObject modal) {
        ResumeGame();
        modal.SetActive(false);
    }

    public void CloseModals() {
        ResumeGame();
        foreach (GameObject modal in modals) {
            if(modal.activeSelf)
                modal.SetActive(false);
        }
    }

    public void CloseOrExitModal(GameObject go) {
        bool anyOpen = false;
        foreach (GameObject modal in modals) {
            if (modal.activeSelf) {
                anyOpen = true;
                break;
            }
        }
        if (anyOpen) {
            CloseModals();
        } else {
            ShowModal(go);
        }
    }

    private void PauseGame() {
        if (!GameParameters.paused) {
            GameParameters.paused = true;
            Time.timeScale = 0;
        }
    }

    private void ResumeGame() {
        if (GameParameters.paused) {
            GameParameters.paused = false;
            Time.timeScale = 1;
        }
    }
}
