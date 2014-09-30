using UnityEngine;
using System.Collections;

public class GameBoardScene : MonoBehaviour {

	public void ChangeToScene(int sceneToChangeTo){
		Application.LoadLevel (sceneToChangeTo);
	}
}
