
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour {

    private bool isMusicMuted = false;
    private bool isSoundsMuted = false;

    [SerializeField]
    private ImagesSwitcher affectedMusicButton;
    [SerializeField]
    private ImagesSwitcher affectedSoundsButton;

    AudioSource mainMusic;
    AudioSource[] sounds;

    private void Awake() {
        // Obtener el SoundSource de la cámara principal
        mainMusic = Camera.main.GetComponent<AudioSource>();
        if (!GameParameters.music)
            MuteMainMusic();

        // Obtener todos los sonidos
        sounds = FindObjectsOfType<AudioSource>();
        if (!GameParameters.sounds)
            MuteSounds();
    }

    // Música
    private void MuteMainMusic() {
        mainMusic.mute = true;
        isMusicMuted = true;
        GameParameters.music = false;

        if(affectedMusicButton != null)
            affectedMusicButton.SetInactiveImage();
    }

    private void RestoreMainMusic() {
        mainMusic.mute = false;
        isMusicMuted = false;
        GameParameters.music = true;

        if (affectedMusicButton != null)
            affectedMusicButton.SetActiveImage();
    }

    public void ToogleMusicVolume() {
        if (mainMusic != null) {
            if (!isMusicMuted) {
                MuteMainMusic();
            } else {
                RestoreMainMusic();
            }
        }
    }


    // Sonidos

    public void MuteSounds() {
        foreach (AudioSource a in sounds) {
            if (a.GetInstanceID() != mainMusic.GetInstanceID()) {
                a.mute = true;
            }
        }
        isSoundsMuted = true;
        GameParameters.sounds = false;

        if (affectedSoundsButton != null)
            affectedSoundsButton.SetInactiveImage();
    }

    private void RestoreSounds() {
        foreach (AudioSource a in sounds) {
            if (a.GetInstanceID() != mainMusic.GetInstanceID()) {
                a.mute = false;
            }
        }
        isSoundsMuted = false;
        GameParameters.sounds = true;

        if (affectedSoundsButton != null)
            affectedSoundsButton.SetActiveImage();
    }

    public void ToogleSoundsVolume() {
        if (!isSoundsMuted) {
            MuteSounds();
        } else {
            RestoreSounds();
        }
    }


}
