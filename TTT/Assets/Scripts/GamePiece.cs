using UnityEngine;
using System.Collections;
using System.Collections;

public class GamePiece : MonoBehaviour {

	public string gamePiece;
	public int gamePieceID;
	public Sprite gamePieceIcon;
	public GameObject gamePieceModel;
	public PieceType pieceType;
	// Use this for initialization
	public enum PieceType{

		X,O
	}
	public GamePiece(string name, int ID, PieceType type){

		gamePiece = name;
		gamePieceID = ID;
		pieceType = type;
		gamePieceIcon = Resources.Load<Sprite> ("" + name);
	}
}
