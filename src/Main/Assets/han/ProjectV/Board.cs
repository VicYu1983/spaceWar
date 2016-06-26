using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace ProjectV.Model
{
	public class Board
	{
		Piece[][] pieces;

		public Piece[][] Pieces {
			get{ return pieces; }
			set{ pieces = value; }
		}

		public Vector2 Size {
			get{ return new Vector2 (10, 10); }
		}
	}
}

