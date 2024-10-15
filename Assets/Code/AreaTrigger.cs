using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AreaTrigger : MonoBehaviour {

	public UnityEvent onTrigger;

	public void OnTriggerEnter(Collider other) {
		onTrigger.Invoke();
	}
}
