using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;//allows to reach the UI image 

public class CreateGameBoard : MonoBehaviour {

	public List<GamePiece> slotsO = new List<GamePiece>();
	public List<GamePiece> GamePieceList = new List<GamePiece> ();
	public List<GameObject> BlockChance;// = new List<GameObject>();
	public List<GameObject> WinChance; //= new List<GameObject>();

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
	public bool gameOver = false;
	public int difficultyX;
	public int totalScore;
	public int currentScore;
	public int highScore;
	public int[] highScores = new int[10];
	public string highScorekey;
	public Text scoreText;

	// Use this for initialization
	void Start () {

		//Set Max Score Potential//Each Block worth 225 points/Worth more the harder the diff
		totalScore = 5 * 25 * 225;

		currentPlayer = 1;
		slotAmount = 0;
		pieceDB = GameObject.FindGameObjectWithTag ("PieceDB").GetComponent<PieceDB> ();
		typerCS = GameObject.FindGameObjectWithTag ("Prompt").GetComponent<Typer> ();
		for (int x = 0; x < 5; x++) {
		
			for(int y = 0; y <5; y++){
				//We do this to have each slot made as gameobject under the gameobject this script is attachted too.
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

	public void ShowWinnerPrompt(){
		 
		if(currentPlayer == 1)
		{
			//prompt = "X gets 4 in a row. Player 1 wins!";
			typerCS.startCR("X gets 4 in a row. Player 1 wins!");
		} else {
			typerCS.startCR("O gets 4 in a row. Player 2 wins!");
			//prompt = "O gets 4 in a row. Player 2 wins!";
		}
		SaveScore ();
	}

	public void ShowStaleMatePrompt(){

		typerCS.startCR("Its A DRAW!!");
		SaveScore ();
	}

	public void Quit(){

		//Return to main menu
		Destroy (GameObject.FindGameObjectWithTag("GameMaster"));
		Application.LoadLevel(0);//load main menu
		
	}

	void OnApplicationQuit() {

	}

	void SaveScore(){
		//Save Winners Score
		PlayerPrefs.SetString ("Score", totalScore.ToString());
		PlayerPrefs.Save ();
		//Debug.Log ("<color=white> SCORETEXT </color>" + totalScore);
		scoreText.text = totalScore.ToString ();
	}
}
