using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MonoGame.Extended;

namespace ClockHandle
{
	public class HandleAnimationComponent : DrawableGameComponent
	{

		struct HandleLine
		{
			public const float TimeToLive = 1.1f;

			public float elapsedTimeToLive;

			public float relativeTimeToLive => elapsedTimeToLive > 0 ? elapsedTimeToLive / TimeToLive : 0;

			public float angle;

			public int timesVisited;

			public HandleLine(float angle)
			{
				this.angle = angle;
				elapsedTimeToLive = 0;
				timesVisited = 0;
			}
		}

		const float WholeCirlceTime = 2.5f;

		const int NumberOfLines = 10;

		const float DeltaAngle = 7f / 180f * (float)Math.PI; // todo: move this value into GUI

		#region Variables

		IGame mainGame;

		float angle = 0f;

		int timesCircled = -1;

		HandleLine[] lines;

		#endregion

		public HandleAnimationComponent(IGame mainGame) : base(mainGame.MonoGame)
		{
			this.mainGame = mainGame;

			// Constructing array of handle lines

			List<HandleLine> tempLines = new List<HandleLine>();

			for (float d = 0; d < 2 * Math.PI; d += DeltaAngle)
			{
				tempLines.Add(new HandleLine(d));
			}

			lines = tempLines.ToArray();
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);

			var elapsedSeconds = gameTime.GetElapsedSeconds();

			angle += 2f / WholeCirlceTime * (float)Math.PI * elapsedSeconds;

			if (angle > (float)(2 * Math.PI))
			{
				timesCircled += 1;
				angle %= (float)(2 * Math.PI);
			}

			for (int i = 0; i < lines.Length; i++)
			{
				var line = lines[i];

				// Decrease line's timte to live
				if (line.elapsedTimeToLive > 0)
				{
					line.elapsedTimeToLive -= elapsedSeconds;
				}

				// Fire up a next line if the condition is true
				if (line.timesVisited != timesCircled && line.angle < angle && line.elapsedTimeToLive <= 0)
				{
					line.elapsedTimeToLive = HandleLine.TimeToLive;
					line.timesVisited = timesCircled;
				}

				lines[i] = line;
			}
		}

		public override void Draw(GameTime gameTime)
		{
			base.Draw(gameTime);

			var viewportSize = mainGame.ViewportSize;
			var unitSize = Math.Min(viewportSize.Width, viewportSize.Height) / 80f;
			var handleSize = unitSize * 10f; // todo: move this value into GUI
			var handleOffset = unitSize * 5f; // todo: move this value into GUI
			var xCenter = viewportSize.Width / 2f;
			var yCenter = viewportSize.Height / 2f;

			// Draw lines all together
			foreach (var line in lines)
			{
				float colorValue = line.relativeTimeToLive;
				if (colorValue > 0)
				{
					Color color = new Color(colorValue, colorValue, colorValue);

					mainGame.SpriteBatch.DrawLine(
						xCenter + handleOffset * (float)Math.Cos(line.angle),
						yCenter + handleOffset * (float)Math.Sin(line.angle),
						xCenter + (handleSize + handleOffset) * (float)Math.Cos(line.angle),
						yCenter + (handleSize + handleOffset) * (float)Math.Sin(line.angle),
						color
					);
				}
			}
		}
	}
}
