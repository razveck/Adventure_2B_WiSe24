using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestScreen : MonoBehaviour {

	public TMP_Text questObjective;

	// Start is called before the first frame update
	void Start() {

	}

	public void StartQuest(QuestStep quest){
		if(quest.isCompleted)
			return;

		gameObject.SetActive(true);

		questObjective.text = quest.objective;
	}

	public void EndQuest(QuestStep quest){
		if(quest.isCompleted)
			return;

		gameObject.SetActive(false);

		quest.isCompleted = true;
		quest.onCompleted.Invoke();
	}
}
