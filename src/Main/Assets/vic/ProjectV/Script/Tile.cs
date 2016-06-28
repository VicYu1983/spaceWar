using UnityEngine;
using System.Collections;
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
				mesh.GetComponent<MeshRenderer> ().material.color = new Color (.5f, 0, 0);
				break;
			case PieceShape.RCircle:
				meshShape.GetComponent<TextMesh> ().text = "Cir_Inv";
				mesh.GetComponent<MeshRenderer> ().material.color = new Color (.5f, 0, 0);
				break;
			case PieceShape.Rect:
				meshShape.GetComponent<TextMesh> ().text = "Rect";
				mesh.GetComponent<MeshRenderer> ().material.color = new Color (0, .5f, 0);
				break;
			case PieceShape.RRect:
				meshShape.GetComponent<TextMesh> ().text = "Rect_Inv";
				mesh.GetComponent<MeshRenderer> ().material.color = new Color (0, .5f, 0);
				break;
			case PieceShape.Triangle:
				meshShape.GetComponent<TextMesh> ().text = "Tri";
				mesh.GetComponent<MeshRenderer> ().material.color = new Color (0, 0, .5f);
				break;
			case PieceShape.RTriangle:
				meshShape.GetComponent<TextMesh> ().text = "Tri_Inv";
				mesh.GetComponent<MeshRenderer> ().material.color = new Color (0, 0, .5f);
				break;
			case PieceShape.Unknown:
				meshShape.GetComponent<TextMesh> ().text = "No";
				mesh.GetComponent<MeshRenderer> ().material.color = new Color (0, 0, 0);
				break;
			default:
				meshShape.GetComponent<TextMesh> ().text = "No";
				mesh.GetComponent<MeshRenderer> ().material.color = new Color (0, 0, 0);
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
