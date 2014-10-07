using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class GameBoardScene : MonoBehaviour, IPointerDownHandler {

//	private MenuManager mmScript;
	public int sceneInt;
	void Start(){

		//mmScript = GameObject.FindGameObjectWithTag ("Canvas").GetComponent<MenuManager> ();

	}
	public void ChangeToScene(int sceneToChangeTo){

		sceneInt = sceneToChangeTo;	
	}

	public void OnPointerDown(PointerEventData data){

		Application.LoadLevel (sceneInt);
	}
}
