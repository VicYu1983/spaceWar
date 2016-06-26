using System;

namespace SpaceWar.Model
{
	public interface IGameListener
	{
		void OnGameStateChange(GameState old, GameState newstate);
	}
}

