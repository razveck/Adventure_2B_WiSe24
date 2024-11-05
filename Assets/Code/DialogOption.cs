using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable] //C# Feature, dass die Daten der Klasse gespeichert werden d�rfen
public class DialogOption {
	public string text;
	public DialogLine nextLine;
	public UnityEvent onSelected;
}
