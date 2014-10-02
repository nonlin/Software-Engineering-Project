using UnityEngine;
using System.Collections;

public class CreateGameBoard : MonoBehaviour {

	public GameObject slot;
	int x = 180;
	int y = 250;
	// Use this for initialization
	void Start () {
	
		for (int i = 0; i < 5; i++) {
		
			for(int j = 0; j <5; j++){

				GameObject slots = (GameObject)Instantiate(slot);
				slots.transform.parent = this.gameObject.transform;//Make it a child of this game object
				slots.GetComponent<RectTransform>().localPosition = new Vector3(x - (i*105),y- (j*105),0);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
