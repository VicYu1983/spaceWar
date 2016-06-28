using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ProjectV.Model;
using System;
using Han.Util;

namespace ProjectV.View{
	public class Game : MonoBehaviour, IModelListener, ITileListener {

		public GameObject tilesContainer;
		public GameObject tileMesh;
		public Vector2 tileSize;
		public List<string> trackingCubes = new List<string> ();

		bool isTrack = false;
		Hashtable cubes = new Hashtable();
		CheckPathResult resultPath;
		GameObject selfTile;

		IModel model;
		public IModel Model {
			set{ model = value; }
		}

		public void OnGameStart(){
			Piece[,] pieces = model.Pieces;
			CreateTiles (pieces);
			CreateSelf ();
		}

		public void StartTouch(){
			isTrack = true;
		}

		public void EndTouch(){
			isTrack = false;
			ResetTile ();
			trackingCubes.Clear ();
		}

		void CreateSelf(){
			selfTile = Instantiate (tileMesh);
			selfTile.name = "self";
			selfTile.transform.parent = this.transform;
			selfTile.transform.localPosition = new Vector3 (-7, 6);
			selfTile.transform.localRotation = Quaternion.Euler (0, 0, 330);

			selfTile.GetComponent<Tile> ().SetUsed (false);
			selfTile.GetComponent<Tile> ().SetEnable (false);
		}

		void CreateTiles( Piece[,] piece){
			for (int j = 0; j < piece.GetLength(0); ++j) {
				for (int i = 0; i < piece.GetLength(1); ++i) {
					GameObject tile = Instantiate (tileMesh);
					tile.transform.parent = tilesContainer.transform;
					tile.name = i + "_" + j + "_tile";
					float width = tileSize.x;
					float height = tileSize.y;
					float offset = 0;
					if (i % 2 == 1) {
						offset = -height / 2;
					}
					tile.transform.localPosition = new Vector3 (width * i, 0, -height * j + offset);
					tile.transform.localRotation = Quaternion.Euler (new Vector3 (90, 90, 60));

					tile.GetComponent<Tile>().SetShape( piece [j,i].Shape );
					tile.GetComponent<Tile>().SetEnable( false );
					tile.GetComponent<Tile>().SetUsed( false );

					//Cubes.Add (tile);
					cubes.Add( tile.name, tile );
				}
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
					/*
					if (AddToTrack (Physics.RaycastAll (ray) [0].transform.name)) {
						resultPath = model.CheckPath (TrackToModel (trackingCubes));
						UpdateTile ( resultPath );
					} 
*/

					switch (AddToTrack (Physics.RaycastAll (ray) [0].transform.name)) {
					case "new":
						resultPath = model.CheckPath (TrackToModel (trackingCubes));
						UpdateTile ( resultPath );
						break;
					case "back":
						resultPath = model.CheckPath (TrackToModel (trackingCubes));
						UpdateTile ( resultPath );
						break;
					case "none":
						break;
					default:
						break;
					}
				}
			}
		}

		void ResetTile(){
			foreach (string name in cubes.Keys)
			{
				( cubes[name] as GameObject ).GetComponent<Tile> ().SetEnable (false);
				( cubes[name] as GameObject ).GetComponent<Tile> ().SetUsed (false);
			}
		}

		void UpdateTile( CheckPathResult result ){
			
			ResetTile ();

			foreach (Vector2 pos in result.neighbors) {
				GameObject tile = GetTileByPosition (pos);
				tile.GetComponent<Tile> ().SetEnable (true);
			}

			foreach (Vector2 pos in result.path) {
				GameObject tile = GetTileByPosition (pos);
				tile.GetComponent<Tile> ().SetUsed (true);
			}

			selfTile.GetComponent<Tile> ().SetShape (result.shape);
		}

		GameObject GetTileByPosition( Vector2 pos ){
			string name = pos.x + "_" + pos.y + "_tile";
			return cubes [name] as GameObject;
		}

		string AddToTrack( string name ){
			if (isTrack) {
				int posid = trackingCubes.IndexOf (name);
				if ( posid == -1) {
					trackingCubes.Add (name);
					return "new";
				} else {
					if (posid != trackingCubes.Count - 1) {
						trackingCubes = trackingCubes.GetRange (0, posid );
						return "back";
					}
					return "none";
				}
			}
			return "none";
		}

		/*
		bool AddToTrack( string name ){
			if (isTrack) {
				if (trackingCubes.IndexOf (name) == -1) {
					trackingCubes.Add (name);
					return true;
				}
			}
			return false;
		}
*/
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
