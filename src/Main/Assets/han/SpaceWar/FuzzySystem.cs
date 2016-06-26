using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Han.Util;

namespace SpaceWar.Model
{
	public class FuzzySystem : MonoBehaviour, ITagManagerListener
	{
		System.Random random = new System.Random ();
		List<GameObject> objs = new List<GameObject> ();
		int searchAction, fireAction, searchHeal, backAction;

		void Start (){
			EventManager.Singleton.Add(this);
		}
		void OnDestroy(){
			EventManager.Singleton.Remove(this);
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
						enemy.MoveTo(fuzzy.Target.transform.position, 10000, 2000);
					}
					if( random.NextDouble()<0.01 ){
						enemy.Dir = enemy.Dir == 1 ? -1 : 1;
					}
				} else if( fv == fireAction ){
					enemy.RotateTo(fuzzy.Target.transform.position, 2000);
				} else if( fv == searchHeal ){
					var item = GameContext.single.TagManager.FindObjectsWithComponent<Item>().FirstOrDefault();
					if( item != null ){
						enemy.MoveTo(item.Belong.transform.position, 10000, 2000);
					}
				} else if( fv == backAction ){
					if( fuzzy.Target != null ){
						enemy.Dir = -1;
						enemy.MoveTo(fuzzy.Target.transform.position, 10000, 2000);
					}
				}

				if( Fire(enemy.gameObject)() > 0.5 ){
					enemy.Shoot();
				}
			});
		}
		ITagManager tagmgr;
		public ITagManager TagManager{set{ tagmgr = value; }}

		public void OnManage(ITagObject obj){
			if (obj.Belong.GetComponent<Fuzzy> () != null && obj.Belong.GetComponent<Player>() != null) {
				objs.Add (obj.Belong);
				Fuzzy fuzzy = obj.Belong.GetComponent<Fuzzy> ();
				searchAction = fuzzy.AddFuzzyValue (SearchEnemey(obj.Belong));
				fireAction = fuzzy.AddFuzzyValue (Fire (obj.Belong));
				searchHeal = fuzzy.AddFuzzyValue (SearchHeal (obj.Belong));
				backAction = fuzzy.AddFuzzyValue (Back (obj.Belong));
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
			return FuzzyNot (Distance (obj, 7, 20));
		}

		static FuzzyValue Back(GameObject obj){
			return FuzzyAnd(FuzzyNot(HasItemHeal()), FuzzyNot (FuzzyHP (obj)));
		}

		static FuzzyValue SearchHeal(GameObject obj){
			return FuzzyAnd(HasItemHeal(), FuzzyNot (FuzzyHP (obj)));
		}

		static FuzzyValue FuzzyNot(FuzzyValue v){
			return () => {
				return 1.0f-v();
			};
		}

		static FuzzyValue HasItemHeal(){
			return ()=>{
				var item = GameContext.single.TagManager.FindObjectsWithComponent<Item>().FirstOrDefault();
				if( item != null ){
					return 1;
				} else {
					return 0;
				}
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

		static FuzzyValue Distance(GameObject obj, float near, float far){
			return () => {
				var fz = obj.GetComponent<Fuzzy>();
				if( fz.Target == null ){
					return 1;
				}
				var dist = Vector2.Distance(fz.Target.transform.position, obj.GetComponent<Player>().body.transform.position);
				if( dist > far ){
					return 1;
				}else if(dist < near){
					return 0;
				}else{
					return (dist-near)/(far-near);
				}
			};
		}
	}
}

