using System;
using UnityEngine;
using System.Collections.Generic;
using Han.Util;

namespace ProjectV.Model 
{
	public class Model : MonoBehaviour, IModel, IEventSenderVerifyProxyDelegate
	{
		public bool randomPiece = false;
		public int pieceWidth = 5, pieceHeight = 5;
		public List<PieceShape> pieces;
		public int rule;

		EventSenderVerifyProxy proxy;
		System.Random rand = new System.Random();

		void Awake(){
			proxy = new EventSenderVerifyProxy (this);
			EventManager.Singleton.Add (proxy);
		}
		void OnDestroy(){
			EventManager.Singleton.Remove (proxy);
		}


		Board board = new Board();
		public void StartGame (int level){
			/*
			board.Pieces = new Piece[,] {
				{new Piece(PieceShape.Unknown), new Piece(PieceShape.Circle),new Piece(PieceShape.Unknown)},
				{new Piece(PieceShape.Triangle), new Piece(PieceShape.Circle),new Piece(PieceShape.Circle)},
				{new Piece(PieceShape.Rect), new Piece(PieceShape.Triangle),new Piece(PieceShape.Triangle)}
			};
			*/
			board.Pieces = new Piece[pieceHeight,pieceWidth];
			for (var i = 0; i < board.Pieces.GetLength(0); ++i) {
				for (var j = 0; j < board.Pieces.GetLength(1); ++j) {
					Piece piece = null;
					if (randomPiece) {
						var r = rand.Next (3);
						switch (r) {
						default:
						case 0:
							piece = new Piece (PieceShape.Circle);
							break;
						case 1:
							piece = new Piece (PieceShape.Rect);
							break;
						case 2:
							piece = new Piece (PieceShape.Triangle);
							break;
						}
					} else {
						var idx = j + i * pieceWidth;
						piece = new Piece(pieces [idx]);
					}
					board.Pieces [i,j] = piece;
				}
			}

			foreach (object obj in proxy.Receivers) {
				(obj as IModelListener).OnGameStart ();
			}
		}
		public void DestroyGame(){
			
		}
		public Piece[,] Pieces{ get { return board.Pieces; } }
		public CheckPathResult CheckPath(List<Vector2> path){
			CheckPathResult result = new CheckPathResult();
			Alg.CheckPath (rule, board, path, out result.shape, out result.path, out result.neighbors);
			return result;
		}
		public bool VerifyReceiverDelegate(object receiver){
			return receiver is IModelListener;
		}
		public void OnAddReceiver(object receiver){
			(receiver as IModelListener).Model = this;
		}
		public void OnRemoveReceiver(object receiver){
			
		}
	}
}

