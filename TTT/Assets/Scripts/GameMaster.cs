using UnityEngine;
using System.Collections;

public class GameMaster : MonoBehaviour {

	public AudioSource audio1;
	public AudioSource audio2;
	public AudioClip ButtonSelect;
	public float effectVol = 1.0f;
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
}
