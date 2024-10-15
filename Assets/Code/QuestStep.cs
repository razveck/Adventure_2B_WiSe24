using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class QuestStep : MonoBehaviour {

	public string objective;
	public bool isCompleted;

	public UnityEvent onCompleted;

	// Start is called before the first frame update
	void Start() {

	}

}
