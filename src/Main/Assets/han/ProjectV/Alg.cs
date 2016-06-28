using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ProjectV.Model
{
	public class Alg
	{
		public static void CheckPath(int rule, Board board, List<Vector2> path, out PieceShape shape, out List<Vector2> finalpath, out List<Vector2> finalneighbors){
			
			Nullable<Vector2> prev = null;
			PieceShape prevShape = PieceShape.Unknown;
			Vector2 curr;
			List<Vector2> neighbors = new List<Vector2>();
			List<Vector2> newpath = new List<Vector2> ();

			foreach (Vector2 pos in path) {
				if (isValidPos (board.Size, pos) == false) {
					break;
				}

				if (prev.HasValue) {
					curr = pos;

					Piece prevPiece = board.GetPiece (prev.Value);
					Piece currPiece = board.GetPiece (curr);

					if (CanEat (rule, prevShape, prevPiece, currPiece)) {
						var currShape = Transition (rule, prevShape, currPiece.Shape);
						if (currShape != PieceShape.Unknown) {
							prevShape = currShape;
							newpath.Add (curr);
						} else {
							break;
						}
					} else {
						break;
					}
					prev = curr;

				} else {
					Piece prevPiece = board.GetPiece (pos);
					prevShape = prevPiece.Shape;
					prev = pos;
					newpath.Add (pos);
				}
			}

			if (prev.HasValue) {
				neighbors.Clear ();
				var prevPiece = board.GetPiece (prev.Value);
				foreach (var ns in PosNeighbors(board.Size, prev.Value)) {
					var nextPiece = board.GetPiece (ns);
					if (CanEat (rule, prevShape, prevPiece, nextPiece)) {
						neighbors.Add (ns);
					}
				}
				neighbors = neighbors.Where ((pos) => {
					return !newpath.Exists((pos2)=>{
						return pos.x == pos2.x && pos.y == pos2.y;
					});
				}).ToList();
			}

			shape = prevShape;
			finalpath = newpath;
			finalneighbors = neighbors;
		}

		public static bool CanEat(int rule, PieceShape currShape, Piece p1, Piece p2){
			PieceShape shape = Transition (rule, currShape, p2.Shape);
			if (shape == PieceShape.Unknown) {
				return false;
			}
			return true;
		}

		public static PieceShape Transition(int rule, PieceShape s1, PieceShape s2){
			if (s1 == PieceShape.Rect) {
				switch (s2) {
				case PieceShape.Rect:
					return PieceShape.RRect;
				case PieceShape.Circle:
					return PieceShape.Rect;
				default:
					return PieceShape.Unknown;
				}
			}

			if (s1 == PieceShape.Circle) {
				switch (s2) {
				case PieceShape.Triangle:
					return PieceShape.Circle;
				case PieceShape.Circle:
					return PieceShape.RCircle;
				default:
					return PieceShape.Unknown;
				}
			}

			if (s1 == PieceShape.Triangle) {
				switch (s2) {
				case PieceShape.Triangle:
					return PieceShape.RTriangle;
				case PieceShape.Rect:
					return PieceShape.Triangle;
				default:
					return PieceShape.Unknown;
				}
			}

			if (s1 == PieceShape.RRect) {
				switch (s2) {
				case PieceShape.Rect:
					return PieceShape.Rect;
				case PieceShape.Triangle:
					return PieceShape.RRect;
				default:
					return PieceShape.Unknown;
				}
			}

			if (s1 == PieceShape.RCircle) {
				switch (s2) {
				case PieceShape.Rect:
					return PieceShape.RCircle;
				case PieceShape.Circle:
					return PieceShape.Circle;
				default:
					return PieceShape.Unknown;
				}
			}

			if (s1 == PieceShape.RTriangle) {
				switch (s2) {
				case PieceShape.Circle:
					return PieceShape.RTriangle;
				case PieceShape.Triangle:
					return PieceShape.Triangle;
				default:
					return PieceShape.Unknown;
				}
			}

			return PieceShape.Unknown;
		}
		public static bool isValidPos(Vector2 size, Vector2 pos){
			if(pos.x<0 || pos.x >= size.x || pos.y<0 || pos.y >= size.y ){
				return false;
			} else {
				return true;
			}
		}
		public static IEnumerable<Vector2> PosNeighbors(Vector2 size, Vector2 pos){
			var isEven = pos.x % 2 == 0;
			Vector2 up, down, left1, left2, right1, right2;
			// 單雙數行計算方式不一樣
			if (isEven) {
				up = new Vector2 (pos.x, pos.y - 1);
				down = new Vector2 (pos.x, pos.y + 1);
				left1 = new Vector2 (pos.x - 1, pos.y -1);
				left2 = new Vector2 (pos.x - 1, pos.y );
				right1 = new Vector2 (pos.x + 1, pos.y -1);
				right2 = new Vector2 (pos.x + 1, pos.y);
			} else {
				up = new Vector2 (pos.x, pos.y - 1);
				down = new Vector2 (pos.x, pos.y + 1);
				left1 = new Vector2 (pos.x - 1, pos.y);
				left2 = new Vector2 (pos.x - 1, pos.y + 1);
				right1 = new Vector2 (pos.x + 1, pos.y);
				right2 = new Vector2 (pos.x + 1, pos.y + 1);
			}
			return 
				from p in new Vector2[]{up, right1, right2, down, left2, left1}
				where isValidPos(size, p)
				select p;
		}
	}
}

