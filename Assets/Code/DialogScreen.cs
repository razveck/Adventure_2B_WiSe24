using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogScreen : MonoBehaviour {

	public TMP_Text dialogText;

	public List<Button> buttons;

	// Start is called before the first frame update
	void Start() {

	}

	public void StartDialog(DialogLine line){
		dialogText.text = line.text;

		for(int i = 0; i < buttons.Count; i++){
			if(i < line.options.Count){ //es gibt eine Option für diesen Button
				buttons[i].gameObject.SetActive(true);
				buttons[i].GetComponentInChildren<TMP_Text>().text = line.options[i].text;
			}else
				buttons[i].gameObject.SetActive(false);
		}
	}
}
