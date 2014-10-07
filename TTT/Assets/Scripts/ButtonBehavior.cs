using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonBehavior : MonoBehaviour,IPointerDownHandler {

	private GameObject SinglePlayer;
	private GameObject MultiPlayer;
	public bool isSingle;
	public bool isMulti;
	// Use this for initialization
	void Start () {
	
		isSingle = false;
		isMulti = false;
		SinglePlayer = GameObject.FindGameObjectWithTag ("SinglePlayer");
		MultiPlayer = GameObject.FindGameObjectWithTag ("MultiPlayer");
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
	}
}
