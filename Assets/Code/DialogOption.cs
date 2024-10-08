using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] //C# Feature, dass die Daten der Klasse gespeichert werden dürfen
public class DialogOption {
	public string text;
	public DialogLine nextLine;
}
