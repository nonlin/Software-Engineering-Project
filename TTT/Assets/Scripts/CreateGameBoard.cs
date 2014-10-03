using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreateGameBoard : MonoBehaviour {

	public List<GamePiece> slotsO = new List<GamePiece>();
	public List<GamePiece> GamePieceList = new List<GamePiece> ();
	public GameObject slots;
	private PieceDB pieceDB;
	public int currentPlayer;
	int x = 180;
	int y = 250;
	// Use this for initialization
	void Start () {
	
		int slotAmount = 0;
		pieceDB = GameObject.FindGameObjectWithTag ("PieceDB").GetComponent<PieceDB> ();
		for (int i = 0; i < 5; i++) {
		
			for(int j = 0; j <5; j++){

				GameObject slot = (GameObject)Instantiate(slots);
				slot.GetComponent<SlotScript>().slotNumber = slotAmount;
				slot.name = slotAmount.ToString();// + ":Slot" + i + "-" + j;
				Debug.Log (slot.name);
				slotsO.Add(new GamePiece());
				GamePieceList.Add(new GamePiece());
				slot.transform.parent = this.gameObject.transform;//Make it a child of this game object
				slot.GetComponent<RectTransform>().localPosition = new Vector3(x - (i*105),y - (j*105),0);
				slotAmount++;
			}

		}
		//AddPiece (0);
		//AddPiece (1);
		//Debug.Log (GamePieceList [0].gamePieceName);
		//Debug.Log (GamePieceList [1].gamePieceName);
		//Debug.Log (GamePieceList.Count);
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void AddPiece(int id,int slotNum){

		for (int i = 0; i < GamePieceList.Count; i++) {
			//if it is null(empty), we can put an item there
			if(GamePieceList[slotNum].gamePieceName == null){

				for (int j= 0; j < pieceDB.gamePiece.Count; j++) {
					//Checks to make sure ID matches
					if (pieceDB.gamePiece[j].gamePieceID == id) {
						//sets empty slot 
						GamePieceList [slotNum] = pieceDB.gamePiece[id];
					}
				}
				
				break;
			}
		}
	}	
}
