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
		//	if(gameBoard.gameOver || gameBoard.currentPlayer == 2) return;
			//Prevents double clicking in the same slot. 
			if (gameBoard.GamePieceList [slotNumber].gamePieceName == null) {
				//User's Place Piece is called with AI off. 
				PlacePiece(false,this.gameObject);
				//Don't have AI play if Player Wins
				if(!gameBoard.gameOver){
					if(GMO.aiDiffID == 1 || GMO.aiDiffID == 0){gameBoard.diffText.text = "Easy"; EasyAI();}
						
					if(GMO.aiDiffID == 2){gameBoard.diffText.text = "Normal"; MedAI ();}

					if(GMO.aiDiffID == 3){gameBoard.diffText.text = "Hard"; HardAI ();}
						
				}
			}	
		}
	}
	
	//Game Logic
	public bool CheckForWin(GameObject slot){
		Debug.Log ("Entered Check Win");
		//Specfic to each slot is the x and the y which is why I have it dynamically assigned depending on what slot the script is on. 
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
		GameObject slot = null;
		
		//slot = WinOrBlock(); 
		if(slot == null) slot = PreventOrCreateTrap();
		if(slot == null) slot = GetCenter(); 
		if(slot == null) slot = GetEmptyCorner(); 
		if(slot == null) slot = GetEmptySide();
		if(slot == null) slot = GetEmptyInnerSide();
		if(slot == null) slot = GetRandomEmptySlot();
		//get the slot number via the name and store it in npcPiece so that we know what location to put the piece at
		if(slot != null){
			gameBoard.npcPiece = int.Parse(slot.GetComponent<SlotScript>().name);
			PlacePiece(true,slot);
		}
		else{
			GameOverTie();
		}
	}

	public void MedAI(){
		
		gameBoard.difficultyX = 5;
		GameObject slot = null;
		
		slot = WinOrBlock(); 
		//if(slot == null) slot = PreventOrCreateTrap();
		if(slot == null) slot = GetCenter(); 
		if(slot == null) slot = GetEmptyCorner(); 
		if(slot == null) slot = GetEmptySide();
		if(slot == null) slot = GetEmptyInnerSide();
		if(slot == null) slot = GetRandomEmptySlot();
		//get the slot number via the name and store it in npcPiece so that we know what location to put the piece at
		if(slot != null){
			gameBoard.npcPiece = int.Parse(slot.GetComponent<SlotScript>().name);
			PlacePiece(true,slot);
		}
		else{
			GameOverTie();
		}
	}

	public void HardAI(){
		
		gameBoard.difficultyX = 5;
		GameObject slot = null;
		
		slot = WinOrBlock(); 
		if(slot == null) slot = PreventOrCreateTrap();
		if(slot == null) slot = GetCenter(); 
		if(slot == null) slot = GetEmptyInnerCorner();
		if(slot == null) slot = GetEmptyInnerSide();
		if(slot == null) slot = GetEmptyCorner(); 
		if(slot == null) slot = GetEmptySide();

		if(slot == null) slot = GetRandomEmptySlot();
		//get the slot number via the name and store it in npcPiece so that we know what location to put the piece at
		if(slot != null){
			gameBoard.npcPiece = int.Parse(slot.GetComponent<SlotScript>().name);
			PlacePiece(true,slot);
		}
		else{
			GameOverTie();
		}
	}
	
	void PlacePiece(bool turnAIOn, GameObject slot){
		
		//For AI Logic
		if (GMO.aiDiffID >= 0 && turnAIOn) {
			Debug.Log ("NPC SLOTNUM"+gameBoard.npcPiece);
			gameBoard.AddPiece (1, gameBoard.npcPiece);
			TurnSwitch(slot);
		} 
		//For Two Player Logic
		if (gameBoard.GamePieceList [slotNumber].gamePieceName == null) {
			slotNum = int.Parse (transform.name);
			Debug.Log ("Current Player"+gameBoard.currentPlayer);
			
			
			if (gameBoard.currentPlayer == 1) {
				Debug.Log ("____X___");
				gameBoard.AddPiece (0, slotNum);
			} else if (GMO.aiDiffID == -1) {
				Debug.Log ("____O____");
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
		} else if (gameBoard.moves >= 24) {
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
	
	GameObject GetRandomEmptySlot(){
		
		GameObject slot;
		List<GameObject> emptySlots = new List<GameObject> ();
		for(int i = 0; i < 5; i++)
		for(int j = 0; j < 5; j++){
			//slot can be any slot from the grid
			slot = gameBoard.aGrid[i,j];
			//if slot is empty aka no player assigned//Gotta check that specfic slots slot script
			if(slot.GetComponent<SlotScript> ().player == 0){
				//add the empty slot to a list of empty slots
				emptySlots.Add (slot);
			}
		}
		//then set the slot equal to some empty slot from the list of empty slots available
		if(emptySlots.Count > 0){
			Debug.Log ("<color=purple>RandomEmptySlot</color>");
			slot = emptySlots [Random.Range (0, emptySlots.Count)];
			return slot;
		}
		
		return null;
	}
	
	GameObject GetEmptySide(){
		
		//	GameObject slot;
		List<GameObject> emptySides = new List<GameObject> ();
		//Note that doing it this way has some overlapping slots. Perhaps divide the inner layer into its own function
		for(int y = 1; y < 4; y++){
			//Left
			if(gameBoard.aGrid[4,y].GetComponent<SlotScript>().GetPlayerSlotID(4,y) == 0) emptySides.Add(gameBoard.aGrid[4,y]);
			
			//Right
			if(gameBoard.aGrid[0,y].GetComponent<SlotScript>().GetPlayerSlotID(0,y) == 0) emptySides.Add(gameBoard.aGrid[0,y]);
			
		}
		
		for(int x = 1; x < 4; x++){
			//Top
			if(gameBoard.aGrid[x,0].GetComponent<SlotScript>().GetPlayerSlotID(x,0) == 0) emptySides.Add(gameBoard.aGrid[x,0]);
			//Bot
			if(gameBoard.aGrid[x,4].GetComponent<SlotScript>().GetPlayerSlotID(x,4) == 0) emptySides.Add(gameBoard.aGrid[x,4]);
		}
		if (emptySides.Count > 0) { Debug.Log ("<color=purple>SidePlay</color>"); return emptySides [Random.Range (0, emptySides.Count)]; }
		
		return null; 
	}
	GameObject GetEmptyInnerSide(){
		List<GameObject> emptyInnerSides = new List<GameObject> ();
		
		if(gameBoard.aGrid[3,2].GetComponent<SlotScript>().GetPlayerSlotID(3,2) == 0) emptyInnerSides.Add(gameBoard.aGrid[3,2]);//Inner
		if(gameBoard.aGrid[2,1].GetComponent<SlotScript>().GetPlayerSlotID(2,1) == 0) emptyInnerSides.Add(gameBoard.aGrid[2,1]);//Inner
		
		if(gameBoard.aGrid[2,3].GetComponent<SlotScript>().GetPlayerSlotID(2,3) == 0) emptyInnerSides.Add(gameBoard.aGrid[2,3]);//Inner
		
		if(gameBoard.aGrid[1,2].GetComponent<SlotScript>().GetPlayerSlotID(1,2) == 0) emptyInnerSides.Add(gameBoard.aGrid[1,2]);//Inner
		
		if (emptyInnerSides.Count > 0) { Debug.Log ("<color=purple>innerSidePlay</color>"); return emptyInnerSides [Random.Range (0, emptyInnerSides.Count)]; }
		return null;
	
	}
	GameObject GetEmptyInnerCorner(){
		List<GameObject> emptyInnerCorner = new List<GameObject> ();

		if(gameBoard.aGrid[3,1].GetComponent<SlotScript>().GetPlayerSlotID(3,1) == 0) emptyInnerCorner.Add(gameBoard.aGrid[3,1]);//Inner
		if(gameBoard.aGrid[1,1].GetComponent<SlotScript>().GetPlayerSlotID(1,1) == 0) emptyInnerCorner.Add(gameBoard.aGrid[1,1]);//Inner

		if(gameBoard.aGrid[3,3].GetComponent<SlotScript>().GetPlayerSlotID(3,3) == 0) emptyInnerCorner.Add(gameBoard.aGrid[3,3]);//Inner
			
		if(gameBoard.aGrid[1,3].GetComponent<SlotScript>().GetPlayerSlotID(1,3) == 0) emptyInnerCorner.Add(gameBoard.aGrid[1,3]);//Inner

		if (emptyInnerCorner.Count > 0) { Debug.Log ("<color=purple>innerSidePlay</color>"); return emptyInnerCorner [Random.Range (0, emptyInnerCorner.Count)]; }
		return null;
	}
	
	GameObject GetEmptyCorner(){
		
		List<GameObject> emptyCorners = new List<GameObject> ();
		
		if(GetPlayerSlotID(4,0) == 0) emptyCorners.Add (gameBoard.aGrid[4,0]);
		if(GetPlayerSlotID(4,4) == 0) emptyCorners.Add (gameBoard.aGrid[4,4]);
		if(GetPlayerSlotID(0,0) == 0) emptyCorners.Add (gameBoard.aGrid[0,0]);
		if(GetPlayerSlotID(0,4) == 0) emptyCorners.Add (gameBoard.aGrid[0,4]);
		if (emptyCorners.Count > 0) {Debug.Log ("<color=purple>Corner</color>"); return emptyCorners [Random.Range (0, emptyCorners.Count)]; }
		
		return null;
	}
	
	GameObject GetCenter(){
		
		if(gameBoard.aGrid[2,2].GetComponent<SlotScript>().GetPlayerSlotID(2,2) == 0){ Debug.Log ("<color=purple>Center</color>"); return gameBoard.aGrid[2,2]; }
		return null; 
	}
	
	GameObject WinOrBlock(){
		
		gameBoard.BlockChance = new List<GameObject>();
		gameBoard.WinChance = new List<GameObject>();
		for(int i = 0; i < 5; i++){
			
			CheckFor3InARow (new Vector2[] {new Vector2 (i,0), new Vector2 (i,1),new Vector2 (i,2), new Vector2(i,3)},"col");
			CheckFor3InARow (new Vector2[] {new Vector2 (i,4), new Vector2 (i,1),new Vector2 (i,2), new Vector2(i,3)},"col");
		}
		for(int i = 0; i < 5; i++){
			
			CheckFor3InARow (new Vector2[] {new Vector2 (0,i), new Vector2 (1,i),new Vector2 (2,i), new Vector2(3,i)},"row");
			CheckFor3InARow (new Vector2[] {new Vector2 (4,i), new Vector2 (1,i),new Vector2 (2,i), new Vector2(3,i)},"row");
		}
		//Diag Set 1
		CheckFor3InARow (new Vector2[] {new Vector2 (3,0), new Vector2 (2,1),new Vector2 (1,2), new Vector2(0,3)},"Diag 1-1");
		CheckFor3InARow (new Vector2[] {new Vector2 (4,0), new Vector2 (3,1),new Vector2 (2,2), new Vector2(1,3)},"Diag 1-2");
		CheckFor3InARow (new Vector2[] {new Vector2 (0,4), new Vector2 (3,1),new Vector2 (2,2), new Vector2(1,3)},"Diag 1-2");
		CheckFor3InARow (new Vector2[] {new Vector2 (4,1), new Vector2 (3,2),new Vector2 (2,3), new Vector2(1,4)},"Diag 1-3");
		//Diag Set 2
		CheckFor3InARow (new Vector2[] {new Vector2 (1,0), new Vector2 (2,1),new Vector2 (3,2), new Vector2(4,3)},"Diag 2-1");
		CheckFor3InARow (new Vector2[] {new Vector2 (0,0), new Vector2 (1,1),new Vector2 (2,2), new Vector2(3,3)},"Diag 2-2");
		CheckFor3InARow (new Vector2[] {new Vector2 (1,1), new Vector2 (2,2),new Vector2 (3,3), new Vector2(4,4)},"Diag 2-2");
		CheckFor3InARow (new Vector2[] {new Vector2 (0,1), new Vector2 (1,2),new Vector2 (2,3), new Vector2(3,4)},"Diag 2-3");
		
		
		if(gameBoard.WinChance.Count > 0) {Debug.Log ("<color=purple>Win</color>"); return gameBoard.WinChance[Random.Range (0, gameBoard.WinChance.Count)]; }
		if(gameBoard.BlockChance.Count > 0) {Debug.Log ("<color=purple>Block</color>"); return gameBoard.BlockChance[Random.Range (0, gameBoard.BlockChance.Count)]; }
		//no chance to block/win return null
		return null; 
	}
	
	void CheckFor3InARow(Vector2[] coords, string debugText){
		
		int p1inRow = 0;
		int p2inRow = 0;
		int player;
		GameObject slot = null;
		Vector2 coord;
		
		for(int i = 0; i < 4; i++){
			coord = coords[i];
			player = gameBoard.aGrid[(int)coord.x, (int)coord.y].GetComponent<SlotScript>().player;
			
			//if slot is empty aka no player assigned
			if(player == 1){
				p1inRow++;
			}
			else if(player == 2){
				p2inRow++;
			}
			else{
				//Store Empty slot for later
				slot = gameBoard.aGrid[(int)coord.x,(int)coord.y];
			}
		}
		
		if(slot != null) {
			
			//we found an empty slot in this row
			if(p2inRow == 3){
				//There are 3 O's in a row with an empty slot
				gameBoard.WinChance.Add (slot);
			}
			else if(p1inRow == 3){
				
				gameBoard.BlockChance.Add (slot);
			}
		}
		//Debug.Log ("<color=red>CheckFor3InARow: </color>" + slot.name + " " + debugText);
	}

	GameObject PreventOrCreateTrap(){
			
		gameBoard.trapSetChance = new List<GameObject> ();
		gameBoard.trapStopChance = new List<GameObject> ();
		gameBoard.openCol = new List<GameObject> ();
		//GameObject[] ColTraps;
		GameObject slot = null;
		int player;

		for(int i = 0; i < 5; i++){
			//Col
			CheckFor2InARow(new Vector2[] {new Vector2(i,1),new Vector2(i,2),new Vector2(i,3),});
			//Row
			CheckFor2InARow(new Vector2[] {new Vector2(1,i),new Vector2(2,i),new Vector2(3,i),});
		}
		//Diag
		CheckFor2InARow(new Vector2[] {new Vector2(3,3),new Vector2(2,2),new Vector2(1,1),});
		CheckFor2InARow(new Vector2[] {new Vector2(3,1),new Vector2(2,2),new Vector2(1,3),});
		//Other Diag Trap
		CheckFor2InARow(new Vector2[] {new Vector2(4,4),new Vector2(3,3),new Vector2(2,2),});
		CheckFor2InARow(new Vector2[] {new Vector2(2,2),new Vector2(1,1),new Vector2(0,0),});
		CheckFor2InARow(new Vector2[] {new Vector2(4,0),new Vector2(3,1),new Vector2(2,2),});
		CheckFor2InARow(new Vector2[] {new Vector2(2,2),new Vector2(1,3),new Vector2(0,4),});
		//SetTrap
		//if(gameBoard.trapSetChance.Count > 0) {
		//	Debug.Log ("<color=red>SetTrap</color>"); return gameBoard.trapSetChance[Random.Range(0, gameBoard.trapSetChance.Count)];}
		//prevent trap
		if(gameBoard.trapStopChance.Count > 0){ 
			Debug.Log ("<color=red>PreventTrap</color>"); return gameBoard.trapStopChance[Random.Range(0, gameBoard.trapStopChance.Count)];}
		
		return null;

	}

	void CheckFor2InARow(Vector2[] coords){
		int p1inRow = 0;
		int p2inRow = 0;
		int player;
		GameObject slot = null;
		Vector2 coord;
		
		for(int i = 0; i < 3; i++){
			coord = coords[i];
			player = gameBoard.aGrid[(int)coord.x, (int)coord.y].GetComponent<SlotScript>().player;
			
			//if slot is empty aka no player assigned
			if(player == 1){
				p1inRow++;
			}
			else if(player == 2){
				p2inRow++;
			}
			else{
				//Store Empty slot for later
				slot = gameBoard.aGrid[(int)coord.x,(int)coord.y];
			}
		}
		
		if(slot != null) {
			
			//we found an empty slot in this row
			if(p2inRow == 2){
				//There are 2 O's in a row with an empty slot
				gameBoard.trapSetChance.Add (slot);
			}
			else if(p1inRow == 2){
				
				gameBoard.trapStopChance.Add (slot);
			}
		}
	}
	void GameOverTie(){
		gameBoard.gameOver = true;
		gameBoard.ShowStaleMatePrompt ();
		//This is how to call IEnumerators to have delayed functions
		StartCoroutine ("TieHighLight");
	}
}
