using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreateGameBoard : MonoBehaviour {

	public List<GamePiece> slotsO = new List<GamePiece>();
	public List<GamePiece> GamePieceList = new List<GamePiece> ();
	public GameObject slots;
	private PieceDB pieceDB;
	public int currentPlayer;
	//public GamePiece GamePieceCS;
	public GameObject[,] aGrid = new GameObject[5,5];
	int xPos = 180;
	int yPos = 250;
	// Use this for initialization
	void Start () {

		int slotAmount = 0;
		pieceDB = GameObject.FindGameObjectWithTag ("PieceDB").GetComponent<PieceDB> ();

		for (int x = 0; x < 5; x++) {
		
			for(int y = 0; y <5; y++){

				GameObject slot = (GameObject)Instantiate(slots);

				slot.GetComponent<SlotScript>().slotNumber = slotAmount;
				slot.GetComponent<SlotScript>().x = x;
				slot.GetComponent<SlotScript>().y = y;

				slot.name = slotAmount.ToString();// + ":Slot" + i + "-" + j;
				//Debug.Log (slot.name);
				slotsO.Add(new GamePiece());//Might not need
				GamePieceList.Add(new GamePiece());
				aGrid[x,y] = slot;
				//Debug.Log (aGrid[x,y].name);
				slot.transform.parent = this.gameObject.transform;//Make it a child of this game object
				slot.GetComponent<RectTransform>().localPosition = new Vector3(xPos - (x*105),yPos - (y*105),0);
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

		//CheckForWin ();
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
