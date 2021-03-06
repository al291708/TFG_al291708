using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSelection : MonoBehaviour {

    public enum PlayerSelections { None, Ittla, Nerta };
    public enum Difficulty { Easy, Medium, Hard };
    public static PlayerSelections playerFactionChosen;
    public static Difficulty playerDifficultyChosen;
	public static PlayerSelection Instance;

	// Use this for initialization
	void Awake () {
		if (Instance) {
			DestroyImmediate (gameObject);
		} else {
			DontDestroyOnLoad (this);
			Instance = this;
		}
	}
}
