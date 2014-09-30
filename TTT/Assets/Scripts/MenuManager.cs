using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {
	
	private Animator menuAnim;
	private bool showDifficulty = false; 

	// Use this for initialization
	void Start () {
		
		menuAnim = GetComponent<Animator> ();
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

}
