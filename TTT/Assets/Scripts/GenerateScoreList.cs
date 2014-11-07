using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GenerateScoreList : MonoBehaviour {

	// Use this for initialization
	public List<GameObject> ScoreEntry = new List<GameObject>();
	public GameObject GO;
	public GameObject ScoreEntryObject;
	public int entryNumber;
	void Start () {
	
		entryNumber = 0;
		for (int i = 0; i < 10; i++) {
		
			GO = Instantiate(ScoreEntryObject) as GameObject;
			GO.transform.parent=transform;
			GO.name = "ScoreEntry"+entryNumber;
			GO.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
			GO.GetComponent<RectTransform>().localPosition = new Vector3(-19.5f,-200 + (50 * i),0);
			entryNumber++;
			ScoreEntry.Add (GO);
		}

		Debug.Log ("<color=red>SCore Entry Count</color>"+ScoreEntry.Count);
	}
	
	// Update is called once per frame
	void Update () {
	

	}
}
