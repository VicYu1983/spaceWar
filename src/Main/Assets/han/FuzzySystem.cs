using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace Model
{
	public class FuzzySystem : MonoBehaviour, ITagManagerListener
	{
		List<GameObject> objs = new List<GameObject> ();
		int searchAction, fireAction, searchHeal;

		void Start (){
			GameContext.single.EventManager.Add(this);
		}
		void OnDestroy(){
			GameContext.single.EventManager.Remove(this);
		}
		void Update(){
			objs.ForEach ((obj) => {
				var fuzzy = obj.GetComponent<Fuzzy>();
				var enemy = obj.GetComponent<Player>();
				if( fuzzy.Target == null ){
					var player = GameContext.single.TagManager.FindObjectsWithTag("player").FirstOrDefault();
					if( player != null ){
						fuzzy.Target = player.Belong.GetComponent<Player>().body;
					}
				}
				var fv = fuzzy.Best();
				// search enemy
				if( fv == searchAction ){
					if( fuzzy.Target != null ){
						enemy.MoveTo(fuzzy.Target.transform.position);
					}
				} else if( fv == fireAction ){
					
				} else if( fv == searchHeal ){
					
				}

				if( Fire(enemy.gameObject)() > 0.5 ){
					enemy.Shoot();
				}
			});
		}

		public void OnManage(ITagObject obj){
			if (obj.Belong.GetComponent<Fuzzy> () != null && obj.Belong.GetComponent<Player>() != null) {
				objs.Add (obj.Belong);
				Fuzzy fuzzy = obj.Belong.GetComponent<Fuzzy> ();
				searchAction = fuzzy.AddFuzzyValue (SearchEnemey(obj.Belong));
				fireAction = fuzzy.AddFuzzyValue (Fire (obj.Belong));
				searchHeal = fuzzy.AddFuzzyValue (SearchHeal (obj.Belong));
			}
		}

		public void OnUnManage(ITagObject obj){
			if (obj.Belong.GetComponent<Fuzzy> () != null && obj.Belong.GetComponent<Player>() != null) {
				objs.Remove (obj.Belong);
			}
		}

		static FuzzyValue SearchEnemey(GameObject obj){
			return FuzzyHP (obj);
		}

		static FuzzyValue Fire(GameObject obj){
			return FuzzyNot (Distance (obj));
		}

		static FuzzyValue SearchHeal(GameObject obj){
			return FuzzyNot (FuzzyHP (obj));
		}

		static FuzzyValue FuzzyNot(FuzzyValue v){
			return () => {
				return 1.0f-v();
			};
		}

		static FuzzyValue FuzzyAnd(FuzzyValue v, FuzzyValue v2){
			return () => {
				return Mathf.Min (v (), v2 ());
			};
		}

		static FuzzyValue FuzzyOr(FuzzyValue v, FuzzyValue v2){
			return () => {
				return Mathf.Max (v (), v2 ());
			};
		}

		static FuzzyValue FuzzyHP(GameObject obj){
			return () => {
				return obj.GetComponent<Player> ().HP / 100.0f;
			};
		}

		static FuzzyValue Distance(GameObject obj){
			return () => {
				var fz = obj.GetComponent<Fuzzy>();
				if( fz.Target == null ){
					return 1;
				}
				var dist = Vector2.Distance(fz.Target.transform.position, obj.GetComponent<Player>().body.transform.position);
				if( dist > 8 ){
					return 1;
				}else if(dist < 3){
					return 0;
				}else{
					return (dist-3)/5.0f;
				}
			};
		}
	}
}

