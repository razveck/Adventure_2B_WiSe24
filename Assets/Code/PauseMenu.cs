using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

	public PlayerInput playerInputActions;
	public PlayerInput menuInputActions;
	public GameObject pauseMenuObject;

	public GameObject resumeButton;

	private void Awake() {
		InputSystem.onAnyButtonPress.Call(OnAnyButtonPress);
	}
	private void OnAnyButtonPress(InputControl control) {

		if(control.device.name.Contains("Mouse"))
			return;

		if(pauseMenuObject.activeSelf) {
			GameObject selected = EventSystem.current.currentSelectedGameObject;
			if(selected == null) {
				EventSystem.current.SetSelectedGameObject(resumeButton);
			}
		}
	}

	void OnEnable() {
		menuInputActions.currentActionMap.FindAction("Pause").performed += OnPaused;
	}

	private void OnDisable() {
		menuInputActions.currentActionMap.FindAction("Pause").performed -= OnPaused;
	}

	public void OnPaused(InputAction.CallbackContext context) {
		TogglePause();
	}

	public void TogglePause() {
		bool paused = pauseMenuObject.activeSelf;

		pauseMenuObject.SetActive(!paused); //gegenteil von paused

		if(paused) {
			Time.timeScale = 1f;
			playerInputActions.currentActionMap.Enable();
		} else {
			Time.timeScale = 0f;
			playerInputActions.currentActionMap.Disable();
			EventSystem.current.SetSelectedGameObject(resumeButton);
		}
	}

	public void MuteMusic() {
		RuntimeManager.GetBus("bus:/").getVolume(out float volume); //'out float' heisst, die variable wird erstellt, in die Funktion gegeben und das Ergebnis wird da gespeichert

		if(volume > 0)
			volume = 0;
		else
			volume = 1;

		RuntimeManager.GetBus("bus:/").setVolume(volume);
	}

	public void ToMenu() {
		TogglePause();
		SceneManager.LoadScene(0);
	}

}
