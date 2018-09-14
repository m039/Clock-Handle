using ClockHandle.Game;
using Microsoft.Xna.Framework;

namespace ClockHandle.Widgets
{
	public class Slider : WidgetComponent
	{
		public Slider(IGame mainGame) : base(mainGame)
		{
		}

		public override void Draw(GameTime gameTime)
		{
			base.Draw(gameTime);

			MainGame.SpriteBatch.DrawString(MainGame.DefaultFont, "Hello from Slider", new Vector2(0, 0), Color.White);
		}
	}
}
