using Microsoft.Xna.Framework;

namespace ClockHandle.Widgets
{
	public class WidgetComponent : DrawableGameComponent
	{
		protected Game.IGame MainGame;

		public WidgetComponent(Game.IGame mainGame) : base(mainGame.MonoGame)
		{
			MainGame = mainGame;
		}

	}
}
