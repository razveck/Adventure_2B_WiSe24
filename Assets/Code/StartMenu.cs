using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour {

	public EventReference musicEvent;

	private void Start() {
		if(!MusicSystem.Instance.music.hasHandle()) {
			MusicSystem.Instance.music = RuntimeManager.CreateInstance(musicEvent);
			//MusicSystem.Instance.music = RuntimeManager.CreateInstance("event:/Music/OST_CozyGame");
			MusicSystem.Instance.music.start();
		}

		MusicSystem.Instance.music.setParameterByNameWithLabel("Scene", "Menu");
	}

	public void StartGame() {
		MusicSystem.Instance.music.setParameterByNameWithLabel("Scene", "Level");
		SceneManager.LoadScene(1);
	}

	public void MuteMusic(){
		RuntimeManager.GetBus("bus:/").getVolume(out float volume); //'out float' heisst, die variable wird erstellt, in die Funktion gegeben und das Ergebnis wird da gespeichert

		if(volume > 0 )
			volume = 0;
		else
			volume = 1;

		RuntimeManager.GetBus("bus:/").setVolume(volume);
	}

}
