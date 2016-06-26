using System;
using UnityEngine;
using Han.Util;

namespace SpaceWar.Model
{
	public enum PlayerState{
		Normal,
		Destroy
	}

	public class Player : MonoBehaviour
	{
		public float addHotByShoot = 6;
		public GameObject body;
		public GameObject shield;
		public int hp = 100;
		public float hot = 0;
		public int dir = 1;

		public int Dir{
			get { return dir; }
			set { dir = value; }
		}

		public int HP{ get{ return hp; } }

		public void AddHP(int v){
			hp += v;
			if (hp > 100) {
				hp = 100;
			}
		}

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
			shield.GetComponent<Rigidbody2D> ().AddTorque (60000);
		}

		public void Forward(float force){
			body.GetComponent<Rigidbody2D> ().AddRelativeForce (new Vector2 (0, dir* force));
		}

		public void Rotate(float force){
			body.GetComponent<Rigidbody2D> ().AddTorque (dir* force);
		}

		public void RotateTo(Vector3 pos, float rotForce){
			var heading = Util.NormalizeAngle(body.transform.eulerAngles.z * Mathf.PI / 180);
			var targetDir = pos - body.transform.position;
			var target = Mathf.Atan2 (-targetDir.x, targetDir.y);
			var bearing = Util.NormalizeAngle(target - heading);
			Rotate (dir* bearing*rotForce);
		}

		public void MoveTo(Vector3 pos, float force, float rotForce){
			RotateTo (pos, rotForce);
			var dis = Mathf.Min(Vector2.Distance (pos, body.transform.position), 10);
			Forward (force*dis/10.0f);
		}

		public bool Shoot(){
			if (hot > 0) {
				return false;
			}

			var bullet = GameContext.single.ObjectFactory.CreateObject (ObjectType.Bullet);
			bullet.GetComponent<Bullet> ().body.transform.localPosition = body.transform.position + new Vector3 ((float)Math.Sin(body.transform.eulerAngles.z*3.14/180)*-3, (float)Math.Cos(body.transform.eulerAngles.z*3.14/180)*3);
			bullet.GetComponent<Bullet> ().body.transform.localRotation = body.transform.localRotation;
			bullet.GetComponent<Bullet>().body.GetComponent<Rigidbody2D> ().AddRelativeForce (new Vector2 (0,1000));

			hot += addHotByShoot;
			return true;
		}

		void Update(){
			if (hot > 0) {
				hot -= Time.deltaTime * 10;
			} else {
				hot = 0;
			}
		}
	}
}

