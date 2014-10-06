using UnityEngine;
using System.Collections;

public class GameBoardScene : MonoBehaviour {

	private MenuManager mmScript;
	private int sceneInt;
	void Start(){

		mmScript = GameObject.FindGameObjectWithTag ("Canvas").GetComponent<MenuManager> ();

	}
	public void ChangeToScene(){

		Application.LoadLevel (1);
	
	}
}
