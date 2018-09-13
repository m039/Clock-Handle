using System;
using Microsoft.Xna.Framework;
using MonoGame.Extended;

namespace ClockHandle
{
	public class HandleAnimationComponent : DrawableGameComponent
	{

		#region Variables

		IGame mainGame;

		#endregion

		public HandleAnimationComponent(IGame mainGame) : base(mainGame.MonoGame)
		{
			this.mainGame = mainGame;
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
		}

		public override void Draw(GameTime gameTime)
		{
			base.Draw(gameTime);

			var size = mainGame.ViewportSize;
			var handleSize = Math.Min(size.Width, size.Height) / 8f;
			var xCenter = size.Width / 2f;
			var yCenter = size.Height / 2f;

			mainGame.SpriteBatch.DrawLine(
				xCenter, yCenter,
				xCenter, yCenter + handleSize,
				Color.White
			);
		}

	}
}
