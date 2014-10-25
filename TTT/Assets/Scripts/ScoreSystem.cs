using UnityEngine;
using System.Collections;

public class ScoreSystem : MonoBehaviour {

	private Animator thisAnim;
	private GameObject ScorePrompt;
	// Use this for initialization
	void Start () {
	
		ScorePrompt = this.gameObject;
		thisAnim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ShowScorePrompt(){thisAnim.SetBool ("showSprompt",true);}

	public void HideScorePrompt(){thisAnim.SetBool ("showSprompt",false);}
}
