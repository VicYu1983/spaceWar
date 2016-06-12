using UnityEngine;
using System.Collections;

namespace Model
{
	public interface ITagObject
	{
		int SeqID{ get; set; }
		string Tag{ get; set; }
	}
}