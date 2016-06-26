using System;

namespace ProjectV.Model
{
	public enum PieceShape {
		Unknown, Rect, Circle, Triangle, RRect, RCircle, RTriangle
	}

	public class Piece{
		PieceShape shape;
		public PieceShape Shape { 
			get{ return shape; }
			set{ shape = value; }
		}
		public Piece(PieceShape shape){
			this.shape = shape;
		}
	}
}

