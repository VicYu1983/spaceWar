using UnityEngine;
using System.Collections;

namespace Han.Util
{
	public interface ITagObject
	{
		int SeqID{ get; set; }
		string Tag{ get; set; }
		GameObject Belong{ get; }
	}
}