using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour {

	public DialogLine dialog;
	public DialogScreen dialogScreen;

	// Start is called before the first frame update
	void Start() {

	}

	public void StartDialog(){
		dialogScreen.StartDialog(dialog);
	}

	public void ChangeDialog(DialogLine newDialog){
		dialog = newDialog;
	}
}
