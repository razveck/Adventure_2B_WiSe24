using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using UnityEngine;

public class MusicSystem : MonoBehaviour {

	//Singleton pattern - es darf nur 1 Objekt/Instanz geben, und es muss immer verf�gbar sein
	public static MusicSystem Instance;

	public EventInstance music;

	//wird vor Start ausgef�hrt
	private void Awake() {
		//sorgt daf�r, dass es nur 1 Instanz gibt
		if(Instance == null){
			Instance = this;
			DontDestroyOnLoad(this);
		}else{
			Destroy(this);
		}
	}
}
