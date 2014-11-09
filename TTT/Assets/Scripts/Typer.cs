using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Typer : MonoBehaviour {
	
	public Text textComp;
	public AudioClip keyEnter;
	private ScoreSystem ScoreToggle;
	// Use this for initialization
	void Start () {

		ScoreToggle = GameObject.FindGameObjectWithTag ("ScorePrompt").GetComponent<ScoreSystem>();
	}
	
	// Update is called once per frame
	void Awake () {
	
	
		textComp = GetComponent<Text> ();
	}

	public IEnumerator TypeIn(string msg){
		//Type Who Won
		yield return new WaitForSeconds(.5f);
		for(int i = 0; i <msg.Length; i++){

			textComp.text = msg.Substring(0,i);
			this.GetComponent<AudioSource>().PlayOneShot(keyEnter);
			yield return new WaitForSeconds(0.1f);
		}
		//yield return new WaitForSeconds(3);
		//DestroyObject(GameObject.FindGameObjectWithTag("GameMaster"));
		//Allow Winner to enter name for score recrod
		ScoreToggle.ShowScorePrompt ();
		//Wait for them to hit enter
		yield return StartCoroutine(WaitForKeyDown(KeyCode.Return));
		//Return to main menu
		Destroy (GameObject.FindGameObjectWithTag("GameMaster"));

		Application.LoadLevel(0);//load main menu

	}
	public void startCR(string msg){

		StartCoroutine ("TypeIn",msg);
	}

	IEnumerator WaitForKeyDown(KeyCode keyCode)
	{
		while (!Input.GetKeyDown(keyCode))
			yield return null;
	}
}
