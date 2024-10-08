using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour {

	public UnityEvent OnInteracted;

	// Start is called before the first frame update
	void Start() {

	}

	// Update is called once per frame
	void Update() {

	}

	public void OnInteract(){
		Debug.Log("Interact YAY!");
		OnInteracted.Invoke(); //event auslösen
	}
}
