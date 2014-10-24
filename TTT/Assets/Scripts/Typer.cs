using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Typer : MonoBehaviour {
	
	public Text textComp;
	public AudioClip keyEnter;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Awake () {
	
	
		textComp = GetComponent<Text> ();
	}

	public IEnumerator TypeIn(string msg){

		yield return new WaitForSeconds(.5f);
		for(int i = 0; i <msg.Length; i++){

			textComp.text = msg.Substring(0,i);
			this.GetComponent<AudioSource>().PlayOneShot(keyEnter);
			yield return new WaitForSeconds(0.1f);
		}
		//yield return new WaitForSeconds(3);
		//DestroyObject(GameObject.FindGameObjectWithTag("GameMaster"));
		Destroy (GameObject.FindGameObjectWithTag("GameMaster"));
		Application.LoadLevel(0);

	}
	public void startCR(string msg){

		StartCoroutine ("TypeIn",msg);
	}
}
