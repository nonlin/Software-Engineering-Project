﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreGet : MonoBehaviour {

	private string Name;
	private string Score;
	public Text SetName;
	public Text SetScore;

	// Use this for initialization
	void Start () {
	
		GetInput ();
		GetGameScore ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void GetInput(){
		
		Name = PlayerPrefs.GetString("Name");
		SetName.text = Name;
		
	}

	public void GetGameScore(){
		
		Score = PlayerPrefs.GetString ("Score");
		SetScore.text = Score;
	}
}
