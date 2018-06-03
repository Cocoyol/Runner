using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImagesSwitcher : MonoBehaviour {

    [SerializeField]
    private Sprite active;
    [SerializeField]
    private Sprite inactive;

    public void SetActiveImage() {
        gameObject.GetComponent<Image>().sprite = active;
    }

    public void SetInactiveImage() {
        gameObject.GetComponent<Image>().sprite = inactive;
    }
}
