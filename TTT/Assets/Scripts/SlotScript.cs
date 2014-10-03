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
 
	// Use this for initialization
	void Start () {
	
		gameBoard = GameObject.FindGameObjectWithTag ("GameBoard").GetComponent<CreateGameBoard>();
		gamePieceImage = gameObject.transform.GetChild (0).GetComponent<Image> ();
		gameBoard.currentPlayer = 1;
	}
	
	// Update is called once per frame
	void Update () {
	
		if (gameBoard.GamePieceList[slotNumber].gamePieceName != null) {

			gamePiece = gameBoard.GamePieceList[slotNumber];
			gamePieceImage.enabled = true;
			gamePieceImage.sprite = gameBoard.GamePieceList[slotNumber].gamePieceIcon;
		}
		else{
		
			gamePieceImage.enabled = false;
		}
	}

	public void OnPointerClick(PointerEventData data){

		for (int i = 0; i < gameBoard.GamePieceList.Count; i++) {
		
			//if(gameBoard.GamePieceList[i].gamePieceName != null)
				//Debug.Log (gameBoard.GamePieceList[i].gamePieceName);
		}
		//Debug.Log (int.Parse(transform.name).ToString());
		if (gameBoard.GamePieceList[slotNumber].gamePieceName == null) {
			slotNum = int.Parse(transform.name);
			if(gameBoard.currentPlayer == 1){
		
				gameBoard.AddPiece (0,slotNum);
			}
			else
				gameBoard.AddPiece (1,slotNum);
			gameBoard.currentPlayer++;
			if(gameBoard.currentPlayer > 2)
				gameBoard.currentPlayer = 1;
		
			Debug.Log ("Player"+gameBoard.currentPlayer +"'s turn.");

		}
	}
}
