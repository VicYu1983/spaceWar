using System;
using UnityEngine;
using System.Collections.Generic;
using Han.Util;

namespace ProjectV.Model 
{
	public class Model : MonoBehaviour, IModel, IEventSenderVerifyProxyDelegate
	{
		EventSenderVerifyProxy proxy;
		void Awake(){
			proxy = new EventSenderVerifyProxy (this);
			EventManager.Singleton.Add (proxy);
		}
		void OnDestroy(){
			EventManager.Singleton.Remove (proxy);
		}

		public int rule;
		Board board = new Board();
		public void StartGame (int level){
			board.Pieces = new Piece[][] {
				new Piece[]{new Piece(PieceShape.Circle), new Piece(PieceShape.Circle),new Piece(PieceShape.Rect)},
				new Piece[]{new Piece(PieceShape.Triangle), new Piece(PieceShape.Triangle),new Piece(PieceShape.Circle)},
				new Piece[]{new Piece(PieceShape.Circle), new Piece(PieceShape.Circle),new Piece(PieceShape.Circle)}
			};
			foreach (object obj in proxy.Receivers) {
				(obj as IModelListener).OnGameStart ();
			}
		}
		public void DestroyGame(){
			
		}
		public Piece[][] Pieces{ get { return board.Pieces; } }
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

