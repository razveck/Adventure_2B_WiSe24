using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using UnityEngine;

public class MusicSystem : MonoBehaviour {

	//Singleton pattern - es darf nur 1 Objekt/Instanz geben, und es muss immer verfügbar sein
	public static MusicSystem Instance;

	public EventInstance music;

	//wird vor Start ausgeführt
	private void Awake() {
		//sorgt dafür, dass es nur 1 Instanz gibt
		if(Instance == null){
			Instance = this;
			DontDestroyOnLoad(this);
		}else{
			Destroy(this);
		}
	}
}
