using UnityEngine;
using System.Collections;

namespace Model
{
	public interface ITagManager
	{
		void Manage(ITagObject player);
		void Unmanage(ITagObject player);
	}
}