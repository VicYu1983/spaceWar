using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Model
{
	public interface ITagManager
	{
		void Manage(ITagObject player);
		void Unmanage(ITagObject player);
		IEnumerable<ITagObject> FindObjectsWithTag (string tag);
		ITagObject FindObjectWithTagAndSeqID (string tag, int seqid);
	}
}