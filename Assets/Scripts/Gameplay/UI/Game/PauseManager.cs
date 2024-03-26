using CT6RIGPR;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private BallController _ballController;
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private Button _resumeButton, _settingsButton, _exitButton;
    [Header("Fade Settings")]
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float _duration, _targetVolume;
    private bool _isPaused = false;

    private void Update()
    {
        if (Application.isPlaying && Input.GetKeyDown(KeyCode.P) && _isPaused == false)
        {
            _ballController.DisableInput();
            _pauseMenu.SetActive(true);
            _resumeButton.Select();
            StartCoroutine(FadeAudioSource.StartFade(_audioSource, _duration, _targetVolume));
            _isPaused = true;
        }
    }
}
