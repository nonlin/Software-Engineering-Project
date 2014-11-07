using UnityEngine;
using System.Collections;

public class GameMaster : MonoBehaviour {

	public AudioSource audio1;
	public AudioSource audio2;
	public AudioClip ButtonSelect;
	public float effectVol = 1.0f;
	public int aiDiffID = 0;
	// Use this for initialization
	void Awake() {
		DontDestroyOnLoad(this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void menuButtonSound(){

		audio2.PlayOneShot(ButtonSelect,effectVol);
	}

	public void seteffectVol(float newEffectVol){

		effectVol = newEffectVol;
	}

	public void setAIDiffID(int currentAIDiffID){

		aiDiffID = currentAIDiffID;
	}

	public void deletePlayerPrefs(){
		PlayerPrefs.DeleteAll ();
		//Reload 

	}
}
