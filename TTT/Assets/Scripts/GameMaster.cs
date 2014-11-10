using UnityEngine;
using System.Collections;

public class GameMaster : MonoBehaviour {

	public AudioSource audio1;
	public AudioSource audio2;
	public AudioClip ButtonSelect;
	public float effectVol = 1.0f;
	public int aiDiffID = 0;
	public int First = 1;
	public bool AIFirst = false;
	// Use this for initialization
	void Awake() {
		DontDestroyOnLoad(this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey("escape"))
			Application.Quit();
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

	public void setFirst(int first){
		First = first;
	}
	public void setAIFirst(bool NPCFirst){
		AIFirst = NPCFirst;
	}
}
