using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TouchScript.Gestures;
using Han.Util;
using ProjectV.Model;

namespace ProjectV.View{
	public class Tile : MonoBehaviour, IEventSenderVerifyProxyDelegate
	{
		TransformGesture tg;
		EventSenderVerifyProxy proxy;

		public GameObject meshShape;
		public GameObject mask;
		public GameObject mesh;
		public GameObject border;

		public List<Texture> textures;

		void Awake(){
			proxy = new EventSenderVerifyProxy (this);
			EventManager.Singleton.Add (proxy);
		}
		void OnDestroy(){
			EventManager.Singleton.Remove (proxy);
		}

		private void OnEnable()
		{
			tg = GetComponent<TransformGesture> ();
			tg.TransformStarted += Tg_TransformStarted;
			tg.TransformCompleted += Tg_TransformCompleted;
		}
		
		private void OnDisable()
		{
			tg.TransformStarted -= Tg_TransformStarted;
			tg.TransformCompleted -= Tg_TransformCompleted;
		}

		void Tg_TransformStarted (object sender, System.EventArgs e)
		{
			foreach (ITileListener obj in proxy.Receivers) {
				obj.StartTouch ();
			}
		}

		void Tg_TransformCompleted (object sender, System.EventArgs e)
		{
			foreach (ITileListener obj in proxy.Receivers) {
				obj.EndTouch ();
			}
		}

		public void SetUsed( bool used ){
			if (used) {
				mask.SetActive (true);
			} else {
				mask.SetActive (false);
			}
		}

		public void SetEnable( bool enable ){
			if (enable) {
				border.SetActive (true);
			} else {
				border.SetActive (false);
			}
		}

		public void SetShape( PieceShape shape ){
			switch ( shape ) {
			case PieceShape.Circle:
				meshShape.GetComponent<TextMesh> ().text = "Cir";
				mesh.GetComponent<MeshRenderer>().material.mainTexture = textures[0];
				break;
			case PieceShape.RCircle:
				meshShape.GetComponent<TextMesh> ().text = "Cir_Inv";
				mesh.GetComponent<MeshRenderer>().material.mainTexture = textures[1];
				break;
			case PieceShape.Rect:
				meshShape.GetComponent<TextMesh> ().text = "Rect";
				mesh.GetComponent<MeshRenderer>().material.mainTexture = textures[2];
				break;
			case PieceShape.RRect:
				meshShape.GetComponent<TextMesh> ().text = "Rect_Inv";
				mesh.GetComponent<MeshRenderer>().material.mainTexture = textures[3];
				break;
			case PieceShape.Triangle:
				meshShape.GetComponent<TextMesh> ().text = "Tri";
				mesh.GetComponent<MeshRenderer>().material.mainTexture = textures[4];
				break;
			case PieceShape.RTriangle:
				meshShape.GetComponent<TextMesh> ().text = "Tri_Inv";
				mesh.GetComponent<MeshRenderer>().material.mainTexture = textures[5];
				break;
			case PieceShape.Unknown:
				meshShape.GetComponent<TextMesh> ().text = "No";
				mesh.GetComponent<MeshRenderer>().material.mainTexture = textures[6];
				break;
			default:
				meshShape.GetComponent<TextMesh> ().text = "No";
				mesh.GetComponent<MeshRenderer>().material.mainTexture = textures[6];
				break;
			}
		}

		public bool VerifyReceiverDelegate(object receiver){
			return receiver is ITileListener;
		}
		public void OnAddReceiver(object receiver){
			
		}
		public void OnRemoveReceiver(object receiver){

		}
	}
}
