using System;
using UnityEngine;
using System.Collections.Generic;

namespace ProjectV.Model
{
	public struct CheckPathResult{
		public PieceShape shape;
		public List<Vector2> path;
		public List<Vector2> neighbors;
	}

	public interface IModel
	{
		void StartGame (int level);
		void DestroyGame();
		Piece[,] Pieces{ get; }
		CheckPathResult CheckPath(List<Vector2> path);
	}
}

