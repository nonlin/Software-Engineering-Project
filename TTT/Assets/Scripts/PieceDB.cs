﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PieceDB : MonoBehaviour {

	public List<GamePiece> gamePiece = new List<GamePiece>();
	private GameMaster GM;
	public int First;
	// Use this for initialization
	void Awake() {
		//DontDestroyOnLoad(this.gameObject);
	}
	void Start () {
		GM = GameObject.FindGameObjectWithTag ("GameMaster").GetComponent<GameMaster>();
		First = GM.First;
		if(GM.AIFirst == false){
			if(First == 1){
				gamePiece.Add (new GamePiece("Player1", 0, GamePiece.PieceType.X));
				gamePiece.Add (new GamePiece("Player2", 1, GamePiece.PieceType.O));
				gamePiece.Add (new GamePiece("NPC", 2, GamePiece.PieceType.O));
			}
			else if (First == 2){
				gamePiece.Add (new GamePiece("Player2", 0, GamePiece.PieceType.O));
				gamePiece.Add (new GamePiece("Player1", 1, GamePiece.PieceType.X));
				gamePiece.Add (new GamePiece("NPC", 2, GamePiece.PieceType.O));
			}
		}
		else {//sawap because if AI goes first Ai becomes Player 1 and throws off the assignment of the pieces because code always things AI is player 2, by swapping we correct it. 
			if(First == 1){
				gamePiece.Add (new GamePiece("Player2", 0, GamePiece.PieceType.O));
				gamePiece.Add (new GamePiece("Player1", 1, GamePiece.PieceType.X));
				gamePiece.Add (new GamePiece("NPC", 2, GamePiece.PieceType.O));
			}
			else if (First == 2){
				gamePiece.Add (new GamePiece("Player1", 0, GamePiece.PieceType.X));
				gamePiece.Add (new GamePiece("Player2", 1, GamePiece.PieceType.O));
				gamePiece.Add (new GamePiece("NPC", 2, GamePiece.PieceType.O));
			}
		}
		Debug.Log (First);

	}
	
	// Update is called once per frame
	void Update () {
	
	}



}
