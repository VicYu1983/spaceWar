using System;
using UnityEngine;
using System.Collections.Generic;

namespace ProjectV.Model
{
	public class Model : IModel
	{
		Board board = new Board();
		public void StartGame (int level){
			board.Pieces = new Piece[][] {
				new Piece[]{new Piece(PieceShape.Circle), new Piece(PieceShape.Circle),new Piece(PieceShape.Rect)},
				new Piece[]{new Piece(PieceShape.Triangle), new Piece(PieceShape.Triangle),new Piece(PieceShape.Circle)},
				new Piece[]{new Piece(PieceShape.Circle), new Piece(PieceShape.Circle),new Piece(PieceShape.Circle)}
			};
		}
		public void DestroyGame(){
			
		}
		public Piece[][] Pieces{ get { return board.Pieces; } }
		public CheckPathResult CheckPath(List<Vector2> path){
			CheckPathResult result = new CheckPathResult();
			Alg.CheckPath (0, board, path, out result.shape, out result.path, out result.neighbors);
			return result;
		}
	}
}

