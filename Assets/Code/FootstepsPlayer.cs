using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public class FootstepsPlayer : MonoBehaviour {

	private EventInstance footstepInstance;

	public EventReference footstepEvent;

	// Start is called before the first frame update
	void Start() {
		footstepInstance = RuntimeManager.CreateInstance(footstepEvent);
		footstepInstance.setParameterByNameWithLabel("Untergründe", "Leaves");
	}

	//wird vom Animation Event aufgerufen
	public void PlayFootsteps(){
		footstepInstance.set3DAttributes(RuntimeUtils.To3DAttributes(transform));

		footstepInstance.start();
	}

	public void ChangeUntergrund(string parameter){
		footstepInstance.setParameterByNameWithLabel("Untergründe", parameter);
	}
}
