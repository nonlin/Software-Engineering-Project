using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreInput : MonoBehaviour {

	public string Name;
	public string Score;

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

		Name = name;
		PlayerPrefs.SetString("Name", name); // The first is the string name that refers to the saved score, the second is your score variable (int)
		PlayerPrefs.Save ();
		Debug.Log ("<color=green>Inputed Value = </color>" + Name);
	}
	


	/*public void SetGameScore(int score){

		Score = score.ToString ();
		PlayerPrefs.SetString ("Score", Score);
		PlayerPrefs.Save ();

	}*/

}
