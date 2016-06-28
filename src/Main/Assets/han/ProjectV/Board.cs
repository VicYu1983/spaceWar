using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace ProjectV.Model
{
	public class Board
	{
		Piece[,] pieces;

		public Piece[,] Pieces {
			get{ return pieces; }
			set{ pieces = value; }
		}

		public Vector2 Size {
			get{ return new Vector2 (pieces.GetLength(1), pieces.GetLength(0)); }
		}

		public Piece GetPiece(Vector2 pos){
			if( Alg.isValidPos(Size, pos) ){
				return Pieces [(int)pos.y,(int)pos.x];
			}
			return null;
		}
	}
}

