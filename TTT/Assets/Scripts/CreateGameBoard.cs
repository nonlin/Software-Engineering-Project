using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;//allows to reach the UI image 

public class CreateGameBoard : MonoBehaviour {

	public List<GamePiece> slotsO = new List<GamePiece>();
	public List<GamePiece> GamePieceList = new List<GamePiece> ();
	public GameObject slots;
	public GameObject slot;
	private PieceDB pieceDB;
	public int currentPlayer;
	public int moves;
	public int npcPiece; 
	public int slotAmount;
	public GameObject[,] aGrid = new GameObject[5,5];
	private int xPos = 180;
	private int yPos = 250;
	//public string prompt;
	private Typer typerCS;
	// Use this for initialization
	void Start () {
		currentPlayer = 1;
		slotAmount = 0;
		pieceDB = GameObject.FindGameObjectWithTag ("PieceDB").GetComponent<PieceDB> ();
		typerCS = GameObject.FindGameObjectWithTag ("Prompt").GetComponent<Typer> ();
		for (int x = 0; x < 5; x++) {
		
			for(int y = 0; y <5; y++){

				slot = (GameObject)Instantiate(slots);

				slot.GetComponent<SlotScript>().slotNumber = slotAmount;
				slot.GetComponent<SlotScript>().x = x;
				slot.GetComponent<SlotScript>().y = y;

				slot.name = slotAmount.ToString();// + ":Slot" + i + "-" + j;
				//Debug.Log (slot.name);
				//slotsO.Add(new GamePiece());//Might not need
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
	public void ShowWinnerPrompt(){
		
		if(currentPlayer == 1)
		{
			//prompt = "X gets 4 in a row. Player 1 wins!";
			typerCS.startCR("X gets 4 in a row. Player 1 wins!");
		} else {
			typerCS.startCR("O gets 4 in a row. Player 2 wins!");
			//prompt = "O gets 4 in a row. Player 2 wins!";
		}
	}

	public void ShowStaleMatePrompt(){

		typerCS.startCR("Its A DRAW!!");
	}

}
