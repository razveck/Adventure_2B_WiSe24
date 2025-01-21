using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DialogScreen : MonoBehaviour {

	private DialogLine currentLine;

	public PlayerInput playerInput;
	public TMP_Text dialogText;
	public Image portraitImage;

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
	}

	void OnButtonClick(int index){
		if(currentLine.options.Count == 0){
			EndDialog();
			return; //abbrechen
		}

		currentLine.options[index].onSelected.Invoke();

		if(currentLine.options[index].nextLine == null){
			EndDialog();
			return; //abbrechen
		}

		StartDialog(currentLine.options[index].nextLine);
	}


	public void StartDialog(DialogLine line){
		currentLine = line;
		dialogText.text = line.text;
		if(line.portrait != null)
			portraitImage.sprite = line.portrait;

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
		playerInput.DeactivateInput();

		StopAllCoroutines();
		//Führt die Funktion als Coroutine aus statt "normale" Funktion
		StartCoroutine(TypewriterCoroutine());
		//Wenn wir mehrere Coroutines haben, können wir auch nur eine stoppen, mit:
		//StopCoroutine(HIER DIE COROUTINE VARIABEL);

		EventSystem.current.SetSelectedGameObject(buttons[0].gameObject);

	}

	public void EndDialog(){
		gameObject.SetActive(false);
		playerInput.ActivateInput();

	}

	//muss nicht Coroutine heissen (aber ich mach das gerne :D)
	private IEnumerator TypewriterCoroutine(){

		int length = dialogText.text.Length;
		//string finalText = dialogText.text;
		//dialogText.text = "";

		WaitForSeconds delay = new WaitForSeconds(0.1f);

		for(int i = 0; i < length; i++) {
			dialogText.maxVisibleCharacters = i + 1;
			//dialogText.text = finalText.Substring(0, i + 1);
			yield return delay;
		}


	}
}
