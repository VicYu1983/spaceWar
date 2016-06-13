using System;
using UnityEngine;

namespace Model
{
	public enum PlayerState{
		Normal,
		Destroy
	}

	public class Player : MonoBehaviour
	{
		public GameObject body;
		public GameObject shield;
		public int hp = 100;

		public PlayerState State{ 
			get{ 
				if (hp <= 0) {
					return PlayerState.Destroy;
				} else {
					return PlayerState.Normal;
				}
			}
		}

		public void Damage(int power){
			hp -= power;
			if (hp < 0) {
				hp = 0;
			}
		}

		public void InvokeShield(){
			shield.GetComponent<Rigidbody2D> ().AddTorque (20000);
		}

		public void Forward(float force){
			body.GetComponent<Rigidbody2D> ().AddRelativeForce (new Vector2 (0, force));
		}

		public void Rotate(float force){
			body.GetComponent<Rigidbody2D> ().AddTorque (force);
		}

		public void Shoot(){
			var bullet = GameContext.single.ObjectFactory.CreateObject (ObjectType.Bullet);
			bullet.GetComponent<Bullet> ().body.transform.localPosition = body.transform.position + new Vector3 ((float)Math.Sin(body.transform.eulerAngles.z*3.14/180)*-3, (float)Math.Cos(body.transform.eulerAngles.z*3.14/180)*3);
			bullet.GetComponent<Bullet> ().body.transform.localRotation = body.transform.localRotation;
			bullet.GetComponent<Bullet>().body.GetComponent<Rigidbody2D> ().AddRelativeForce (new Vector2 (0,1000));
		}

		void Update(){
			
		}
	}
}

