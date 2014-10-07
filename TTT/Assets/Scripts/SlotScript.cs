﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;//allows to reach the UI image 
using UnityEngine.EventSystems;

public class SlotScript : MonoBehaviour, IPointerDownHandler {

	public GamePiece gamePiece;
	private Image gamePieceImage;
	public int slotNumber;
	public CreateGameBoard gameBoard;
	private int slotNum;
	private string[] slotNumString;
	public int x;
	public int y;
	public int player;
	// Use this for initialization
	void Start () {
	
		gameBoard = GameObject.FindGameObjectWithTag ("GameBoard").GetComponent<CreateGameBoard>();
		gamePieceImage = gameObject.transform.GetChild (0).GetComponent<Image> ();
		gameBoard.currentPlayer = 1;
	}
	
	// Update is called once per frame
	void Update () {
		//Make sure only non empty slots display an icon. 
		if (gameBoard.GamePieceList[slotNumber].gamePieceName != null) {

			gamePiece = gameBoard.GamePieceList[slotNumber];
			gamePieceImage.enabled = true;
			gamePieceImage.sprite = gameBoard.GamePieceList[slotNumber].gamePieceIcon;
		}
		else{
		
			gamePieceImage.enabled = false;
		}
	}

	public void OnPointerDown(PointerEventData data){

		if (gameBoard.GamePieceList[slotNumber].gamePieceName == null) {
			slotNum = int.Parse(transform.name);
			Debug.Log (gameBoard.currentPlayer);

			if(gameBoard.currentPlayer == 1){
		
				gameBoard.AddPiece (0,slotNum);


			}
			else{
				gameBoard.AddPiece (1,slotNum);

			}
			gameBoard.moves++;
			//Update player to currentplayer to make CheckWin Check correct player set
			player = gameBoard.currentPlayer;
			if(CheckForWin ()){

				Debug.Log ("Player " + player + " Won!");
				gameBoard.ShowWinnerPrompt();

				return;
			}else if(gameBoard.moves >= 25){

				Debug.Log ("TIE!");
				gameBoard.ShowStaleMatePrompt();
				//This is how to call IEnumerators to have delayed functions
				StartCoroutine ("TieHighLight");

				return;
			}

			gameBoard.currentPlayer++;
			if(gameBoard.currentPlayer > 2)
				gameBoard.currentPlayer = 1;
		}
	}

	//Game Logic
	public bool CheckForWin(){

		//Row/Col Check
		if (( GetPlayerSlotID(x,1) == (gameBoard.currentPlayer)) &&
		    GetPlayerSlotID(x,2) == (gameBoard.currentPlayer) && 
		    GetPlayerSlotID(x,3) == (gameBoard.currentPlayer) && 
		    (GetPlayerSlotID(x,4) == (gameBoard.currentPlayer) || 
		 	GetPlayerSlotID(x,0) == (gameBoard.currentPlayer))){
			Debug.Log ("Col Sat" + gameBoard.currentPlayer);
			HighlightWin(1);
				
			return true; 
		}
		if ((GetPlayerSlotID(1,y) == (gameBoard.currentPlayer)) &&
		    GetPlayerSlotID(2,y) == (gameBoard.currentPlayer) && 
		    GetPlayerSlotID(3,y) == (gameBoard.currentPlayer)&& 
		    (GetPlayerSlotID(4,y) == (gameBoard.currentPlayer) || 
		 	GetPlayerSlotID(0,y) == (gameBoard.currentPlayer))){
			Debug.Log ("Row Sat");
			HighlightWin(2);
			return true; 
		}
		//Diag Cross 1
		if((GetPlayerSlotID(0,0) == (gameBoard.currentPlayer) || 
		   	GetPlayerSlotID(4,4) == (gameBoard.currentPlayer)) &&
		   GetPlayerSlotID(1,1) == (gameBoard.currentPlayer) &&
		   GetPlayerSlotID(2,2) == (gameBoard.currentPlayer) &&
		   GetPlayerSlotID(3,3) == (gameBoard.currentPlayer)){
			Debug.Log ("Mid Diag Sat");
			HighlightWin(3);
			return true;
		}
		if((GetPlayerSlotID(1,0) == (gameBoard.currentPlayer) && 
		    GetPlayerSlotID(2,1) == (gameBoard.currentPlayer)) &&
		   GetPlayerSlotID(3,2) == (gameBoard.currentPlayer) &&
		   GetPlayerSlotID(4,3) == (gameBoard.currentPlayer)){
			Debug.Log ("Top Diag Sat");
			HighlightWin(4);
			return true;
		}
		if((GetPlayerSlotID(0,1) == (gameBoard.currentPlayer) && 
		    GetPlayerSlotID(1,2) == (gameBoard.currentPlayer)) &&
		   GetPlayerSlotID(2,3) == (gameBoard.currentPlayer) &&
		   GetPlayerSlotID(3,4) == (gameBoard.currentPlayer)){
			Debug.Log ("Bot Diag Sat");
			HighlightWin(5);
			return true;
		}
		//Diag Cross 2
		if((GetPlayerSlotID(4,0) == (gameBoard.currentPlayer) || 
		    GetPlayerSlotID(0,4) == (gameBoard.currentPlayer)) &&
		   GetPlayerSlotID(3,1) == (gameBoard.currentPlayer) &&
		   GetPlayerSlotID(2,2) == (gameBoard.currentPlayer) &&
		   GetPlayerSlotID(1,3) == (gameBoard.currentPlayer)){
			Debug.Log ("Mid Diag 2 Sat");
			HighlightWin(6);
			return true;
		}
		if((GetPlayerSlotID(3,0) == (gameBoard.currentPlayer) && 
		    GetPlayerSlotID(2,1) == (gameBoard.currentPlayer)) &&
		   GetPlayerSlotID(1,2) == (gameBoard.currentPlayer) &&
		   GetPlayerSlotID(0,3) == (gameBoard.currentPlayer)){
			Debug.Log ("Top Diag 2 Sat");
			HighlightWin(7);
			return true;
		}
		if((GetPlayerSlotID(4,1) == (gameBoard.currentPlayer) && 
		    GetPlayerSlotID(3,2) == (gameBoard.currentPlayer)) &&
		   GetPlayerSlotID(2,3) == (gameBoard.currentPlayer) &&
		   GetPlayerSlotID(1,4) == (gameBoard.currentPlayer)){
			Debug.Log ("Bot Diag 2 Sat");
			HighlightWin(8);
			
			return true;
		}
		return false; 
	}

	public int GetPlayerSlotID(int x, int y){

		return gameBoard.aGrid [x, y].GetComponent<SlotScript> ().player;
	}

	void HighlightWin(int winID){

		for(int i = 0; i < 5; i++)
			for(int j = 0; j < 5; j++)
				switch(winID){
				case 1:
					if(GetPlayerSlotID(x,j) == gameBoard.currentPlayer){
						gameBoard.aGrid [x, j].GetComponent<Image>().color = Color.green;
					}
					break;
				case 2:
					if(GetPlayerSlotID(i,y) == gameBoard.currentPlayer){
						gameBoard.aGrid [i, y].GetComponent<Image>().color = Color.green;
					}
					break;
				case 3:
				if(GetPlayerSlotID(i,j) == gameBoard.currentPlayer && i == j){

						gameBoard.aGrid [i, j].GetComponent<Image>().color = Color.green;
						
					}
					break;
				case 4://Top Diag 
				if(GetPlayerSlotID(i,j) == gameBoard.currentPlayer && i == (j+1)){
					
					gameBoard.aGrid [i, j].GetComponent<Image>().color = Color.green;
					
				}
				break;
				case 5://Bot Diag
				if(GetPlayerSlotID(i,j) == gameBoard.currentPlayer && (i+1) == j){
					
					gameBoard.aGrid [i, j].GetComponent<Image>().color = Color.green;
					
				}
				break;
				case 6://Mid Diag 2
				if(GetPlayerSlotID(i,j) == gameBoard.currentPlayer &&  (((i==4 && j==0) || (i==0 && j==4)) || (i==3 && j==1) || (i==2 && j==2)|| (i==1 && j==3))){
					
					gameBoard.aGrid [i, j].GetComponent<Image>().color = Color.green;
					
				}
				break;
				case 7://Top Diag 2
				if(GetPlayerSlotID(i,j) == gameBoard.currentPlayer && ((i==3 && j==0) || (i==2 && j==1) || (i==1 && j==2) || (i==0 && j==3))){
					//Only Slots with matching ID to Player
					gameBoard.aGrid [i, j].GetComponent<Image>().color = Color.green;
					
				}
				break;
				case 8:
				if(GetPlayerSlotID(i,j) == gameBoard.currentPlayer && i == (j-1)){
					
					gameBoard.aGrid [i, j].GetComponent<Image>().color = Color.green;
					
				}
				break;
				default:
				Debug.Log ("Error: Convential Win Not Found");
				break;
				}
		
	}
	public IEnumerator TieHighLight(){
		for(int i = 0; i < 5; i++)
			for(int j = 0; j < 5; j++){
				yield return new WaitForSeconds(.5f);
				gameBoard.aGrid[i,j].GetComponent<Image>().color = Color.blue;
		}
		//gameBoard.GetComponent<SlotScript>().enabled = false;
	}
}
