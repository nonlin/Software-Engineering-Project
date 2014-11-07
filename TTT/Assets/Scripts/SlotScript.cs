using UnityEngine;
using System.Collections;
using UnityEngine.UI;//allows to reach the UI image 
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class SlotScript : MonoBehaviour, IPointerDownHandler {

	public GamePiece gamePiece;
	private Image gamePieceImage;
	public int slotNumber;
	public CreateGameBoard gameBoard;
	private ScoreInput scoreInput;
	private GameMaster GMO;
	private int slotNum;
	private string[] slotNumString;
	public int x;
	public int y;
	public int player;
	 
	// Use this for initialization
	void Start () {

		player = 0;
		GMO = GameObject.FindGameObjectWithTag ("GameMaster").GetComponent<GameMaster>();
		gameBoard = GameObject.FindGameObjectWithTag ("GameBoard").GetComponent<CreateGameBoard>();
		scoreInput = GameObject.FindGameObjectWithTag ("ScorePrompt").GetComponent<ScoreInput> ();
		gamePieceImage = gameObject.transform.GetChild (0).GetComponent<Image> ();
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
		//User can't click when game is over.
		if(!gameBoard.gameOver){

			//Prevent Click Spamming in the case of "Thinking NPC"
			if(gameBoard.gameOver || gameBoard.currentPlayer == 2) return;
			//Prevents double clicking in the same slot. 
			if (gameBoard.GamePieceList [slotNumber].gamePieceName == null) {
				//User's Place Piece is called with AI off. 
				PlacePiece(false,this.gameObject);
				//Don't have AI play if Player Wins
				if(!gameBoard.gameOver){

					EasyAI();
				}
			}	
		}
	}

	//Game Logic
	public bool CheckForWin(GameObject slot){
		Debug.Log ("Entered Check Win");
		SlotScript slotCS = slot.GetComponent<SlotScript> ();
		//Row/Col Check
		if (( GetPlayerSlotID(slotCS.x,1) == (gameBoard.currentPlayer)) &&
		    GetPlayerSlotID(slotCS.x,2) == (gameBoard.currentPlayer) && 
		    GetPlayerSlotID(slotCS.x,3) == (gameBoard.currentPlayer) && 
		    (GetPlayerSlotID(slotCS.x,4) == (gameBoard.currentPlayer) || 
		 GetPlayerSlotID(slotCS.x,0) == (gameBoard.currentPlayer))){
			Debug.Log ("Col Sat" + gameBoard.currentPlayer);
			HighlightWin(1,slot);
				
			return true; 
		}
		if ((GetPlayerSlotID(1,slotCS.y) == (gameBoard.currentPlayer)) &&
		    GetPlayerSlotID(2,slotCS.y) == (gameBoard.currentPlayer) && 
		    GetPlayerSlotID(3,slotCS.y) == (gameBoard.currentPlayer)&& 
		    (GetPlayerSlotID(4,slotCS.y) == (gameBoard.currentPlayer) || 
		 	GetPlayerSlotID(0,slotCS.y) == (gameBoard.currentPlayer))){
			Debug.Log ("Row Sat");
			HighlightWin(2,slot);
			return true; 
		}
		//Diag Cross 1
		if((GetPlayerSlotID(0,0) == (gameBoard.currentPlayer) || 
		   	GetPlayerSlotID(4,4) == (gameBoard.currentPlayer)) &&
		   GetPlayerSlotID(1,1) == (gameBoard.currentPlayer) &&
		   GetPlayerSlotID(2,2) == (gameBoard.currentPlayer) &&
		   GetPlayerSlotID(3,3) == (gameBoard.currentPlayer)){
			Debug.Log ("Mid Diag Sat");
			HighlightWin(3,slot);
			return true;
		}
		if((GetPlayerSlotID(1,0) == (gameBoard.currentPlayer) && 
		    GetPlayerSlotID(2,1) == (gameBoard.currentPlayer)) &&
		   GetPlayerSlotID(3,2) == (gameBoard.currentPlayer) &&
		   GetPlayerSlotID(4,3) == (gameBoard.currentPlayer)){
			Debug.Log ("Top Diag Sat");
			HighlightWin(4,slot);
			return true;
		}
		if((GetPlayerSlotID(0,1) == (gameBoard.currentPlayer) && 
		    GetPlayerSlotID(1,2) == (gameBoard.currentPlayer)) &&
		   GetPlayerSlotID(2,3) == (gameBoard.currentPlayer) &&
		   GetPlayerSlotID(3,4) == (gameBoard.currentPlayer)){
			Debug.Log ("Bot Diag Sat");
			HighlightWin(5,slot);
			return true;
		}
		//Diag Cross 2
		if((GetPlayerSlotID(4,0) == (gameBoard.currentPlayer) || 
		    GetPlayerSlotID(0,4) == (gameBoard.currentPlayer)) &&
		   GetPlayerSlotID(3,1) == (gameBoard.currentPlayer) &&
		   GetPlayerSlotID(2,2) == (gameBoard.currentPlayer) &&
		   GetPlayerSlotID(1,3) == (gameBoard.currentPlayer)){
			Debug.Log ("Mid Diag 2 Sat");
			HighlightWin(6,slot);
			return true;
		}
		if((GetPlayerSlotID(3,0) == (gameBoard.currentPlayer) && 
		    GetPlayerSlotID(2,1) == (gameBoard.currentPlayer)) &&
		   GetPlayerSlotID(1,2) == (gameBoard.currentPlayer) &&
		   GetPlayerSlotID(0,3) == (gameBoard.currentPlayer)){
			Debug.Log ("Top Diag 2 Sat");
			HighlightWin(7,slot);
			return true;
		}
		if((GetPlayerSlotID(4,1) == (gameBoard.currentPlayer) && 
		    GetPlayerSlotID(3,2) == (gameBoard.currentPlayer)) &&
		   GetPlayerSlotID(2,3) == (gameBoard.currentPlayer) &&
		   GetPlayerSlotID(1,4) == (gameBoard.currentPlayer)){
			Debug.Log ("Bot Diag 2 Sat");
			HighlightWin(8,slot);
			
			return true;
		}
		return false; 
	}

	public int GetPlayerSlotID(int x, int y){

		//Debug.Log ("aGrid Player:"+gameBoard.aGrid [x, y].GetComponent<SlotScript> ().player+" at "+x+"-"+y);
		return gameBoard.aGrid [x, y].GetComponent<SlotScript> ().player;
	}

	void HighlightWin(int winID, GameObject slot){

		SlotScript slotCS = slot.GetComponent<SlotScript> ();
		for(int i = 0; i < 5; i++)
			for(int j = 0; j < 5; j++)
				switch(winID){
				case 1:
				if(GetPlayerSlotID(slotCS.x,j) == gameBoard.currentPlayer){
					gameBoard.aGrid [slotCS.x, j].GetComponent<Image>().color = Color.green;
					}
					break;
				case 2:
				if(GetPlayerSlotID(i,slotCS.y) == gameBoard.currentPlayer){
					gameBoard.aGrid [i, slotCS.y].GetComponent<Image>().color = Color.green;
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
				case 8://Bot Diag 2
				if(GetPlayerSlotID(i,j) == gameBoard.currentPlayer && ((i==4 && j==1) || (i==3 && j==2) || (i==2 && j==3) || (i==1 && j==4))){
					
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
	public void EasyAI(){

		gameBoard.difficultyX = 5;
		List<GameObject> emptySlots = new List<GameObject> ();
		GameObject slot;
		for(int i = 0; i < 5; i++)
			for(int j = 0; j < 5; j++){
			//slot can be any slot from the grid
			slot = gameBoard.aGrid[i,j];
			//if slot is empty aka no player assigned
			if(slot.GetComponent<SlotScript> ().player == 0){
				//add the empty slot to a list of empty slots
				emptySlots.Add (slot);
			}
		}
		//then set the slot equal to some empty slot from the list of empty slots available
		slot = emptySlots [Random.Range (0, emptySlots.Count)];
		//get the slot number via the name and store it in npcPiece
		gameBoard.npcPiece = int.Parse(slot.GetComponent<SlotScript>().name);
		PlacePiece(true,slot);
	}

	void PlacePiece(bool turnAIOn, GameObject slot){

		//For AI Logic
		if (GMO.aiDiffID == 1 && turnAIOn) {
			Debug.Log ("NPC SLOTNUM"+gameBoard.npcPiece);
			gameBoard.AddPiece (1, gameBoard.npcPiece);
			TurnSwitch(slot);
		} 
		//For Two Player Logic
		if (gameBoard.GamePieceList [slotNumber].gamePieceName == null) {
			slotNum = int.Parse (transform.name);
			Debug.Log ("Current Player"+gameBoard.currentPlayer);


			if (gameBoard.currentPlayer == 1) {
					
				gameBoard.AddPiece (0, slotNum);
			} else if (GMO.aiDiffID == 0) {

				gameBoard.AddPiece (1, slotNum);
			} 

			gameBoard.moves++;
			ScoreSystem();
			TurnSwitch(slot);
		}
	}

	void TurnSwitch(GameObject slot){
		//Update Slot Player Info With the Current Player
		slot.GetComponent<SlotScript>().player = gameBoard.currentPlayer;
		//Check for Win Every Turn
		if (CheckForWin (slot)) {

			gameBoard.gameOver = true;
			Debug.Log ("Player " + slot.GetComponent<SlotScript>().player + " Won!");
			gameBoard.ShowWinnerPrompt ();
			
			return;
		} else if (gameBoard.moves >= 25) {
			gameBoard.gameOver = true;
			Debug.Log ("TIE!");
			gameBoard.ShowStaleMatePrompt ();
			//This is how to call IEnumerators to have delayed functions
			StartCoroutine ("TieHighLight");
			
			return;
		}
		
		gameBoard.currentPlayer++;
		if (gameBoard.currentPlayer > 2)
			gameBoard.currentPlayer = 1;
	}

	void ScoreSystem(){

		//Debug.Log ("<color = red>Enter Score System</color>");

		if (gameBoard.moves >= 4 && gameBoard.totalScore != 0) {
		
			gameBoard.totalScore = gameBoard.totalScore-(gameBoard.difficultyX * 225);
			Debug.Log ("<color=red>Your Score is </color>" + gameBoard.totalScore);
			//scoreInput.SetGameScore(gameBoard.totalScore);
		}
	}
}
