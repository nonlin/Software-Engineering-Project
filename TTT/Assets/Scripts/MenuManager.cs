using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
	
	private Animator menuAnim;
	//private bool showDifficulty = false; 
	//private bool showOptions = false;
	//private int volLvl = 1.0f;
	// Use this for initialization
	void Start () {

		menuAnim = this.GetComponent<Animator> ();
		menuAnim.SetBool ("showOp", false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void showAI(){

		menuAnim.SetBool("show",true);
	}

	public void hideAI(){

		menuAnim.SetBool("show",false);
	}

	public void OnPointerEnter(PointerEventData data){

		menuAnim.SetBool ("showOp", true);
	}

	public void OnPointerExit(PointerEventData data){

		menuAnim.SetBool ("showOp", false);
	}

	public void showScoreBoard(){menuAnim.SetBool("show",true);}

	public void hideScoreBoard(){menuAnim.SetBool("show",false);}

}
