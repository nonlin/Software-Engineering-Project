using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Typer : MonoBehaviour {
	
	public Text textComp;
	private CreateGameBoard gameBoard;
	public AudioClip keyEnter;
	// Use this for initialization
	void Start () {
	
		//StartCoroutine ("TypeIn",);
		gameBoard = GameObject.FindGameObjectWithTag ("GameBoard").GetComponent<CreateGameBoard>();

	}
	
	// Update is called once per frame
	void Awake () {
	
	
		textComp = GetComponent<Text> ();
	}

	public IEnumerator TypeIn(string msg){
		Debug.Log ("test"+msg);
		yield return new WaitForSeconds(.5f);
		for(int i = 0; i <msg.Length; i++){

			textComp.text = msg.Substring(0,i);
			this.GetComponent<AudioSource>().PlayOneShot(keyEnter);
			yield return new WaitForSeconds(0.1f);
		}

	}
	public void startCR(string msg){

		StartCoroutine ("TypeIn",msg);
	}
}
