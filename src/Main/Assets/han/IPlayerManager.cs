using UnityEngine;
using System.Collections;

namespace Model
{
	
public interface IPlayerManager
{
	void Manage(IPlayer player);
	void Unmanage(IPlayer player);
}

}