using ClockHandle.Game;
using Microsoft.Xna.Framework;
using Myra.Graphics2D.Text;

namespace ClockHandle.Widgets
{
	public class Sliders : WidgetComponent
	{
		FormattedText _formattedText;

		public Sliders(IGame mainGame) : base(mainGame)
		{
		}

		public override void Draw(GameTime gameTime)
		{
			base.Draw(gameTime);

			_formattedText.Draw(MainGame.SpriteBatch, Point.Zero, Color.LightBlue);
		}

		protected override void LoadContent()
		{
			base.LoadContent();

			_formattedText = new FormattedText
			{
				Font = MainGame.DefaultFont,
				Text = "Hello",
				Width = 500
			};
		}
	}
}
