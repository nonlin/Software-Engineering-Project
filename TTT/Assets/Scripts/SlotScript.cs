using UnityEngine;
using System.Collections;
using UnityEngine.UI;//allows to reach the UI image 
using UnityEngine.EventSystems;

public class SlotScript : MonoBehaviour, IPointerClickHandler {

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
			CheckForWin ();
			gamePiece = gameBoard.GamePieceList[slotNumber];
			gamePieceImage.enabled = true;
			gamePieceImage.sprite = gameBoard.GamePieceList[slotNumber].gamePieceIcon;
		}
		else{
		
			gamePieceImage.enabled = false;
		}
	}

	public void OnPointerClick(PointerEventData data){

		//Debug.Log (int.Parse(transform.name).ToString());
		//Change Player Turn
		if (gameBoard.GamePieceList[slotNumber].gamePieceName == null) {
			slotNum = int.Parse(transform.name);
			Debug.Log (gameBoard.currentPlayer);

			if(gameBoard.currentPlayer == 1){
		
				gameBoard.AddPiece (0,slotNum);

			}
			else{
				gameBoard.AddPiece (1,slotNum);

			}
			/*if(CheckForWin()){
				Debug.Log ("Game Over");
				return;
			}*/

			gameBoard.currentPlayer++;
			if(gameBoard.currentPlayer > 2)
				gameBoard.currentPlayer = 1;

			//CheckForWin();
			//Debug.Log ("Player"+gameBoard.currentPlayer +"'s turn.");
			//Debug.Log (x+","+y);


		}
	}

	//Game Logic
	public bool CheckForWin(){
		
		//SlotScript slotCS = slots.GetComponent<SlotScript> ();
		//Debug.Log (slotCS.x + "," + slotCS.y);
		//Debug.Log (x + "," + y);
		if ((GetPlayerSlotID(x,0) == (gameBoard.currentPlayer -1) && GetPlayerSlotID(x,1) == (gameBoard.currentPlayer -1)) &&
		     GetPlayerSlotID(x,2) == (gameBoard.currentPlayer -1) && GetPlayerSlotID(x,3) == (gameBoard.currentPlayer -1)){

			Debug.Log ("TRUE!");
			return true; 
		}
		
		return false; 
	}

	public int GetPlayerSlotID(int x, int y){

		return gameBoard.aGrid [x, y].GetComponent<SlotScript> ().gamePiece.gamePieceID;
	}
}
