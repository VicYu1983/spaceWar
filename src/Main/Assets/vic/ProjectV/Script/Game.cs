using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ProjectV.Model;
using System;
using Han.Util;

namespace ProjectV.View{
	public class Game : MonoBehaviour, IModelListener, ITileListener {

		public GameObject TilesContainer;
		public GameObject TileMesh;

		bool isTrack = false;
		public List<string> trackingCubes = new List<string> ();

		IModel model;
		public IModel Model {
			set{ model = value; }
		}

		public void OnGameStart(){
			Piece[][] pieces = model.Pieces;
			for (int i = 0; i < pieces.Length; ++i) {
				CreateTiles (pieces [i], i);
			}
		}

		public void StartTouch(){
			isTrack = true;
		}

		public void EndTouch(){
			isTrack = false;
			trackingCubes.Clear ();
		}

		void CreateTiles( Piece[] piece, int col ){
			for (int i = 0; i < piece.Length; ++i) {
				GameObject tile = Instantiate (TileMesh);
				tile.transform.parent = TilesContainer.transform;
				tile.name = i + "_" + col + "_tile";
				float width = tile.GetComponent<MeshFilter> ().mesh.bounds.size.x;
				float height = tile.GetComponent<MeshFilter> ().mesh.bounds.size.y;
				float offset = 0;
				if (i % 2 == 1) {
					offset = -height / 2;
				}
				tile.transform.localPosition = new Vector3 (width * i, 0, -height * col + offset);
				tile.transform.localRotation = Quaternion.Euler (new Vector3 (-90, 0, 30));

				tile.GetComponent<MeshRenderer> ().material.color = GetColorByShape (piece [i].Shape);
			}
		}

		Color GetColorByShape( PieceShape shape ){
			switch ( shape ) {
			case PieceShape.Circle:
				return new Color (1, 0, 0);
			case PieceShape.RCircle:
				return new Color (1, .3f, .3f);
			case PieceShape.Rect:
				return new Color (0, 1, 0);
			case PieceShape.RRect:
				return new Color (.3f, 1, .3f);
			case PieceShape.Triangle:
				return new Color (0, 0, 1);
			case PieceShape.RTriangle:
				return new Color (.3f, .3f, 1);
			case PieceShape.Unknown:
				return new Color (0, 0, 0);
			default:
				return new Color (0, 0, 0);
			}
		}

		// Use this for initialization
		void Start () {
			
		}
		
		// Update is called once per frame
		void Update () {
			if (isTrack) {
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

				if (Physics.RaycastAll (ray).Length > 0) {
					if (AddToTrack (Physics.RaycastAll (ray) [0].transform.name)) {
						UpdateTile( model.CheckPath (TrackToModel (trackingCubes)) );
					}
				}
			}
		}

		void UpdateTile( CheckPathResult result ){
			print (result.path.Count);
			print (result.neighbors.Count);
		}

		bool AddToTrack( string name ){
			if (isTrack) {
				if (trackingCubes.IndexOf (name) == -1) {
					trackingCubes.Add (name);
					return true;
				}
			}
			return false;
		}

		List<Vector2> TrackToModel( List<string> tc ){
			List<Vector2> retList = new List<Vector2> ();
			foreach (string name in tc) {
				string[] arystr = name.Split (new char[]{ '_' }, 3);
				retList.Add (new Vector2 (Int32.Parse(arystr [0]), Int32.Parse( arystr [1] )));
			}
			return retList;
		}
			
		void Awake(){
			EventManager.Singleton.Add (this);
		}

		void OnDestroy(){
			EventManager.Singleton.Remove (this);
		}
	}
}
