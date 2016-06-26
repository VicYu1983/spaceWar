using System;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;

namespace ProjectV.Model
{
	public class TestBoard : MonoBehaviour
	{
		void Start(){
			TestPiece ();
			TestBoardGame ();
		}

		void TestBoardGame(){
			Board board = new Board ();
			board.Pieces = new Piece[][] {
				new Piece[]{new Piece(PieceShape.Circle), new Piece(PieceShape.Circle),new Piece(PieceShape.Circle)},
				new Piece[]{new Piece(PieceShape.Circle), new Piece(PieceShape.Circle),new Piece(PieceShape.Circle)},
				new Piece[]{new Piece(PieceShape.Circle), new Piece(PieceShape.Circle),new Piece(PieceShape.Circle)}
			};

			PieceShape shape;
			List<Vector2> path;
			List<Vector2> neighbors;

			Alg.CheckPath (
				board,
				new Vector2[]{ 
					new Vector2(0,0), new Vector2(1,0)
				}.ToList(),
				out shape,
				out path,
				out neighbors
			);

			print (shape);
			foreach (var pos in neighbors) {
				print (pos);
			}
		}

		void TestPiece(){
			var size = new Vector2 (10, 10);
			var pos = Alg.PosNeighbors (size, new Vector2 (1, 0)).ToArray ();
			if (!pos [0].Equals (new Vector2 (2, 0))) {
				Debug.LogError ("X");
			}
			if (!pos [1].Equals (new Vector2 (2, 1))) {
				Debug.LogError ("X");
			}
			if (!pos [2].Equals (new Vector2 (1, 1))) {
				Debug.LogError ("X");
			}
			if (!pos [3].Equals (new Vector2 (0, 1))) {
				Debug.LogError ("X");
			}
			if (!pos [4].Equals (new Vector2 (0, 0))) {
				Debug.LogError ("X");
			}


			pos = Alg.PosNeighbors (size, new Vector2 (2, 0)).ToArray ();
			if (!pos [0].Equals (new Vector2 (3, 0))) {
				Debug.LogError ("X");
			}
			if (!pos [1].Equals (new Vector2 (2, 1))) {
				Debug.LogError ("X");
			}
			if (!pos [2].Equals (new Vector2 (1, 0))) {
				Debug.LogError ("X");
			}


			pos = Alg.PosNeighbors (size, new Vector2 (1, 1)).ToArray ();
			if (!pos [0].Equals (new Vector2 (1, 0))) {
				Debug.LogError ("X");
			}
			if (!pos [1].Equals (new Vector2 (2, 1))) {
				Debug.LogError ("X");
			}
			if (!pos [2].Equals (new Vector2 (2, 2))) {
				Debug.LogError ("X");
			}
			if (!pos [3].Equals (new Vector2 (1, 2))) {
				Debug.LogError ("X");
			}
			if (!pos [4].Equals (new Vector2 (0, 2))) {
				Debug.LogError ("X");
			}
			if (!pos [5].Equals (new Vector2 (0, 1))) {
				Debug.LogError ("X");
			}

			foreach (Vector2 p in pos) {
				//print (p);
			}

		}
	}
}

