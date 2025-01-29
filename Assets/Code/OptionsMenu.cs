using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour {

	[System.Serializable]
	public class Options {
		public float mouseSensitivity = 1f;
		public bool invertMouseX;
		public bool invertMouseY;
	}

	public static Options myOptions;

	public Slider masterSlider;
	public Slider mouseSensSlider;

	public Toggle invertXToggle;
	public Toggle invertYToggle;


	// Start is called before the first frame update
	void Start() {
		if(PlayerPrefs.HasKey("Options")) {
			string options = PlayerPrefs.GetString("Options");
			myOptions = JsonUtility.FromJson<Options>(options);
		}else{
			myOptions = new();
		}


		masterSlider.onValueChanged.AddListener(MasterSlider);

		mouseSensSlider.onValueChanged.AddListener(MouseSensSlider);

		invertXToggle.onValueChanged.AddListener(InvertX);
		invertYToggle.onValueChanged.AddListener(InvertY);

		float masterVolume = PlayerPrefs.GetFloat("MasterVolume");
		masterSlider.value = masterVolume;
		RuntimeManager.GetVCA("vca:/Master").setVolume(masterVolume);

		//float mouseSens = PlayerPrefs.GetFloat("MouseSens");
		//mouseSensitivity = mouseSens;
		mouseSensSlider.value = myOptions.mouseSensitivity;

		
	}

	public void MasterSlider(float volume) {
		PlayerPrefs.SetFloat("MasterVolume", volume);

		//VCA
		RuntimeManager.GetVCA("vca:/Master").setVolume(volume);
	}

	public void MusicSlider(float volume) {


		//VCA
		RuntimeManager.GetVCA("vca:/Music").setVolume(volume);
	}

	public void SfxSlider(float volume) {


		//VCA
		RuntimeManager.GetVCA("vca:/SFX").setVolume(volume);
	}

	public void MouseSensSlider(float value) {
		//mouseSensitivity = value;
		//PlayerPrefs.SetFloat("MouseSens", value);
		myOptions.mouseSensitivity = value;

		string json = JsonUtility.ToJson(myOptions);
		PlayerPrefs.SetString("Options", json);
		Debug.Log(json);
	}

	public void InvertX(bool value) {
		//invertMouseX = value;
	}

	public void InvertY(bool value) {
		//invertMouseY = value;
	}
}





[System.Serializable]
public class Savegame {
	public Vector3 PlayerPosition;
	public List<Vector3> EnemyPositions;
}

