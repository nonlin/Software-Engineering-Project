using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonBehavior : MonoBehaviour,IPointerDownHandler {

	private GameObject SinglePlayer;
	private GameObject MultiPlayer;
	private GameObject ScoreBoard;
	private GameObject aiSelectE, aiSelectM, aiSelectH;
	public bool isSingle, isMulti,isScore;
	private bool isEasy, isMed, isHard;

	// Use this for initialization
	void Start () {
	
		isSingle = false;
		isMulti = false;
		isEasy = false;
		isHard = false;
		isMed = false;
		isScore = false;
		//Get GameObject via its tag
		SinglePlayer = GameObject.FindGameObjectWithTag ("SinglePlayer");
		MultiPlayer = GameObject.FindGameObjectWithTag ("MultiPlayer");
		ScoreBoard = GameObject.FindGameObjectWithTag("ScoreBoard");
		aiSelectE = GameObject.FindGameObjectWithTag ("AIDiffE");
		aiSelectM = GameObject.FindGameObjectWithTag ("AIDiffM");
		aiSelectH = GameObject.FindGameObjectWithTag ("AIDiffH");

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnPointerDown(PointerEventData data){
		//Find the gameobject user is selecting on click and make it true and others false
		if (gameObject ==  SinglePlayer) {
			isSingle = true;
			isMulti = false;	
			isScore = false;
		}
		else if (gameObject ==  MultiPlayer){
			isMulti = true;
			isSingle = false; 
			isScore = false;
		}
		else if (gameObject == ScoreBoard){
			isMulti = false;
			isSingle = false;
			isScore = true;
		}
		//Once we know if its true we can toggle the colors of the button so that they look selected or not.
		if(isSingle){
			gameObject.GetComponent<Image>().color = Color.green;
			MultiPlayer.GetComponent<Image>().color = Color.white;
			ScoreBoard.GetComponent<Image>().color = Color.white;
		}
		if(isMulti){
			gameObject.GetComponent<Image>().color = Color.green;
			SinglePlayer.GetComponent<Image>().color = Color.white;
			ScoreBoard.GetComponent<Image>().color = Color.white;
		}
		if (isScore) {
			gameObject.GetComponent<Image>().color = Color.green;
			SinglePlayer.GetComponent<Image>().color = Color.white;	
			MultiPlayer.GetComponent<Image>().color = Color.white;
		}
		//Debug.Log (isSingle + "-" + isMulti);
		//Debug.Log (gameObject.tag);
		//AI buttons
		if (gameObject == aiSelectE) {
		
			isEasy = true;
			isMed = false;
			isHard = false;
		}
		if (gameObject == aiSelectM) {
			
			isEasy = false;
			isMed = true;
			isHard = false;
		}
		if (gameObject == aiSelectH) {
			
			isEasy = false;
			isMed = false;
			isHard = true;
		}
		if (isEasy) {
			gameObject.GetComponent<Image>().color = Color.green;
			aiSelectM.GetComponent<Image>().color = Color.white;
			aiSelectH.GetComponent<Image>().color = Color.white;
		}
		if (isMed) {
			gameObject.GetComponent<Image>().color = Color.blue;
			aiSelectE.GetComponent<Image>().color = Color.white;
			aiSelectH.GetComponent<Image>().color = Color.white;
		}
		if (isHard) {
			gameObject.GetComponent<Image>().color = Color.red;
			aiSelectM.GetComponent<Image>().color = Color.white;
			aiSelectE.GetComponent<Image>().color = Color.white;
		}
	}
}
