using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DialogScreen : MonoBehaviour {

	private DialogLine currentLine;
	private PlayerInput playerInput;

	public TMP_Text dialogText;

	public List<Button> buttons;

	// Start is called the first time the object is activated
	// Awake is called when the object is loaded
	void Awake() {
		//Beispiel OnClick event
		//Button button = buttons[0];
		//fügt ClickBeispiel hinzu, sodass onClick ClickBeispiel ausführt
		//button.onClick.AddListener(ClickBeispiel);

		for (int i = 0; i < buttons.Count; i++){
			//i wird in die Funktion weitergegeben
			// () => heisst Lambda
			//i wird gecaptured, dewegen müssen wir den Wert kopieren
			int index = i;
			buttons[i].onClick.AddListener(() => OnButtonClick(index));
		}

		playerInput = FindAnyObjectByType<PlayerInput>();
	}

	void OnButtonClick(int index){
		if(currentLine.options.Count == 0){
			EndDialog();
			return; //abbrechen
		}

		if(currentLine.options[index].nextLine == null){
			EndDialog();
			return; //abbrechen
		}

		StartDialog(currentLine.options[index].nextLine);
	}


	public void StartDialog(DialogLine line){
		currentLine = line;
		dialogText.text = line.text;

		for(int i = 0; i < buttons.Count; i++){
			if(i < line.options.Count){ //es gibt eine Option für diesen Button
				buttons[i].gameObject.SetActive(true);
				buttons[i].GetComponentInChildren<TMP_Text>().text = line.options[i].text;
			}else
				buttons[i].gameObject.SetActive(false);
		}

		//wenn keine Options
		if(line.options.Count == 0){
			buttons[0].gameObject.SetActive(true);
			buttons[0].GetComponentInChildren<TMP_Text>().text = "Continue";
		}

		gameObject.SetActive(true);
		playerInput.enabled = false;
	}

	public void EndDialog(){
		gameObject.SetActive(false);
		playerInput.enabled = true;
	}
}
