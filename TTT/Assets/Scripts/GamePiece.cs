using UnityEngine;
using System.Collections;

[System.Serializable]
public class GamePiece {

	public string gamePieceName;
	public int gamePieceID;
	public Sprite gamePieceIcon;
	public GameObject gamePieceModel;
	public PieceType pieceType;
	// Use this for initialization
	public enum PieceType{

		X,O
	}
	public GamePiece(string name, int ID, PieceType type){

		gamePieceName = name;
		gamePieceID = ID;
		pieceType = type;
		gamePieceIcon = Resources.Load<Sprite> ("" + name);
	}
	public GamePiece(){
		gamePieceID = -1;
	}
}
