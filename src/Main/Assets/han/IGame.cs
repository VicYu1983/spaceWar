using System;

namespace Model
{
	public enum GameState{
		Pending,
		Win,
		Lose,
		Play
	}

	public interface IGame
	{
		int Level{ get; }
		GameState State{ get; }
		void StartGame(int level);
		void DestroyGame();
	}
}

