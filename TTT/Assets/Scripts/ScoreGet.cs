using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreGet : MonoBehaviour {

	private string Entry;
	private string Score;
	public Text SetName;
	public Text SetScore;
	public string EntryNum;
	// Use this for initialization
	void Start () {
		EntryNum = gameObject.name.Substring (10);
		Debug.Log ("<color=green> ENTRY NUM </color>" + EntryNum);
		//gameObject.tag = "ScoreEntry" + EntryNum;
		GetInput ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void GetInput(){
		
		Entry = PlayerPrefs.GetString("Entry"+EntryNum);
		
		string[] split = Entry.Split (' ');
		if(split.Length == 2){
		//if(int.Parse(split[1]) > 0){
		SetName.text = split[0];
		Debug.Log ("<color=blue> SCORE NAME </color>" + split[0] + ": " + split[1]);
		//Score = split [1];
		SetScore.text = split[1];
		}
		
	}

}
