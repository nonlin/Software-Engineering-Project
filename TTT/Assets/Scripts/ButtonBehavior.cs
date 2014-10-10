using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonBehavior : MonoBehaviour,IPointerDownHandler {

	private GameObject SinglePlayer;
	private GameObject MultiPlayer;
	private GameObject aiSelectE, aiSelectM, aiSelectH;
	public bool isSingle, isMulti;
	private bool isEasy, isMed, isHard;

	// Use this for initialization
	void Start () {
	
		isSingle = false;
		isMulti = false;
		isEasy = false;
		isHard = false;
		isMed = false;
		SinglePlayer = GameObject.FindGameObjectWithTag ("SinglePlayer");
		MultiPlayer = GameObject.FindGameObjectWithTag ("MultiPlayer");
		aiSelectE = GameObject.FindGameObjectWithTag ("AIDiffE");
		aiSelectM = GameObject.FindGameObjectWithTag ("AIDiffM");
		aiSelectH = GameObject.FindGameObjectWithTag ("AIDiffH");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnPointerDown(PointerEventData data){

		if (gameObject ==  SinglePlayer) {
			isSingle = true;
			isMulti = false;		
		}
		else if (gameObject ==  MultiPlayer){
			isMulti = true;
			isSingle = false; 
		}
		if(isSingle){
			gameObject.GetComponent<Image>().color = Color.green;
			MultiPlayer.GetComponent<Image>().color = Color.white;
		}
		if(isMulti){
			gameObject.GetComponent<Image>().color = Color.green;
			SinglePlayer.GetComponent<Image>().color = Color.white;
		}
		Debug.Log (isSingle + "-" + isMulti);
		Debug.Log (gameObject.tag);
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
