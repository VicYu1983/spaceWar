using UnityEngine;
using System.Collections;

namespace ProjectV.View{
	public class Game : MonoBehaviour {

		public GameObject TileMesh;

		void CreateTiles(){
			for (int i = 0; i < 37; ++i) {
				GameObject tile = Instantiate (TileMesh);
				tile.transform.parent = this.transform;
				float width = tile.GetComponent<MeshFilter> ().mesh.bounds.size.x;
				float height = tile.GetComponent<MeshFilter> ().mesh.bounds.size.y;
				int y = i % 10;
				int x = (int)(i * .1);
				float offset = 0;
				if (x % 2 == 0) {
					offset = height / 2;
				}
				tile.transform.localPosition = new Vector3 (width * x, 0, height * y + offset);
				tile.transform.localRotation = Quaternion.Euler (new Vector3 (-90, 0, 30));
			}
		}

		// Use this for initialization
		void Start () {
			CreateTiles ();
		}
		
		// Update is called once per frame
		void Update () {
		
		}
	}
}
