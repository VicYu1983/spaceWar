using System;

namespace Model
{
	public interface IGameListener
	{
		void OnGameStateChange(GameState old, GameState newstate);
	}
}

