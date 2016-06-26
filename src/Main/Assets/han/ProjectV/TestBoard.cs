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
		}

		void TestPiece(){
			Board board = new Board ();
			var pos = Alg.PosNeighbors (board.Size, new Vector2 (1, 0)).ToArray ();
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


			pos = Alg.PosNeighbors (board.Size, new Vector2 (2, 0)).ToArray ();
			if (!pos [0].Equals (new Vector2 (3, 0))) {
				Debug.LogError ("X");
			}
			if (!pos [1].Equals (new Vector2 (2, 1))) {
				Debug.LogError ("X");
			}
			if (!pos [2].Equals (new Vector2 (1, 0))) {
				Debug.LogError ("X");
			}


			pos = Alg.PosNeighbors (board.Size, new Vector2 (1, 1)).ToArray ();
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
				print (p);
			}

		}
	}
}

