using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Han.Util
{
	public interface ITagManager
	{
		void Manage(ITagObject player);
		void Unmanage(ITagObject player);
		IEnumerable<ITagObject> FindObjectsWithTag (string tag);
		ITagObject FindObjectWithTagAndSeqID (string tag, int seqid);
		IEnumerable<ITagObject> FindObjectsWithComponent<T> ();
	}
}