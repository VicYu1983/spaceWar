using UnityEngine;
using System.Collections;

namespace Model
{
	public class TagObject : MonoBehaviour, ITagObject
	{
		public int seqId;
		public string tag;

		public int SeqID{ get { return seqId; } set{ seqId = value; } }
		public string Tag{ get { return tag; } set { tag = value; } }

		void Start ()
		{
			GameContext.single.EventManager.Add(this);
			GameContext.single.TagManager.Manage (this);
		}

		void Destroy(){
			GameContext.single.TagManager.Unmanage (this);
			GameContext.single.EventManager.Remove(this);
		}
		
		// Update is called once per frame
		void Update ()
		{
		
		}
	}
}