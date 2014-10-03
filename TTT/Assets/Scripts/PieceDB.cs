using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PieceDB : MonoBehaviour {

	public List<GamePiece> gamePiece = new List<GamePiece>();
	// Use this for initialization
	void Start () {
	
		gamePiece.Add (new GamePiece("Player1", 0, GamePiece.PieceType.X));
		gamePiece.Add (new GamePiece("Player2", 1, GamePiece.PieceType.O));
		gamePiece.Add (new GamePiece("NPC", 2, GamePiece.PieceType.O));

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
