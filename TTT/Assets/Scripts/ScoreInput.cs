using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class ScoreInput : MonoBehaviour {
	
	public string Score;
	public List<int> Scores = new List<int>();
	[SerializeField]
	private InputField nameInputField;
	
	//[SerializeField]
	//private Button submitButton = null;

	// Use this for initialization
	void Start () {
	
		//SetGameScore ();
		// Add listener to catch the submit (when set Submit button is pressed)
		nameInputField.onSubmit.AddListener((value) => SetInput(value));
		// Add validation
		nameInputField.validation = InputField.Validation.Alphanumeric;
		
		// This is a setup for a button that grabs the field value when pressed
		//submitButton.onClick.AddListener(() => SetInput(nameInputField.value));
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetInput(string name){

		int oldScore;
		string oldName;
		Score = PlayerPrefs.GetString ("Score", "0");
		
		for (int i = 9; i > 0; i--) {
			//CHeck to see if we have a key at EntryLocation:i
			if(PlayerPrefs.HasKey ("Entry"+i)){
				//if we do split that key's entry to get its 0:name, 1:Score
				string[] split = PlayerPrefs.GetString ("Entry"+i).Split (' ');
				Debug.Log ("<color=white> ENTRY: </color>" + PlayerPrefs.GetString("Entry"+i));
				//check that entries score with the new score we have
				if(int.Parse(split[1]) < int.Parse (Score) ){
					//if the score we have is greater than the score already stored swap and save?
					//OlsScore and name = to score and name already stored.
					oldScore = int.Parse (split[1]);
					oldName = split[0];
					PlayerPrefs.SetString("Entry"+i, name + " " + Score);
					PlayerPrefs.Save ();
					Debug.Log ("<color=blue> Saving IF: </color>" + name + " " + Score + ":" + i);
					Score = oldScore.ToString();
					name = oldName;
				}
			}
			//else if we don't have an entry at EntryLocation:i, insert it? 
			else{

				Debug.Log ("<color=blue> Saving Else: </color>" + name + " " + Score + ":" + i);
				PlayerPrefs.SetString("Entry"+i, name + " " + Score);
				PlayerPrefs.Save ();
				Score = "0";
				name = "-";
			}

		}
	
}



	/*public void SetGameScore(int score){

		Score = score.ToString ();
		PlayerPrefs.SetString ("Score", Score);
		PlayerPrefs.Save ();

	}*/

}
