using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour {

	public PlayerInput playerInputActions;
	public PlayerInput menuInputActions;
	public GameObject pauseMenuObject;

	public GameObject resumeButton;

	void Awake() {
		menuInputActions.currentActionMap.FindAction("Pause").performed += OnPaused;
	}

	public void OnPaused(InputAction.CallbackContext context) {
		TogglePause();
	}

	public void TogglePause(){
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

}
