using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MonoGame.Extended;

namespace ClockHandle.Widgets
{
	public class HandleAnimationComponent : WidgetComponent
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

		public interface ISettings
		{
			int NumberOfLines { get; }

			float LineSize { get; }

			float LineOffset { get; }
		}

		public class DefaultSettings : ISettings
		{
			public int NumberOfLines => 20;

			public float LineSize => 10f;

			public float LineOffset => 5f;
		}

		const float WholeCirlceTime = 2.5f;

		#region Variables

		public const int MaxHandleLines = 200;

		float angle = 0f;

		int timesCircled = -1;

		int numberOfLines;

		readonly HandleLine[] lines = new HandleLine[MaxHandleLines]; // Beforehand allocated array

		readonly ISettings settings;

		#endregion

		public HandleAnimationComponent(Game.IGame mainGame, ISettings settings = null) : base(mainGame)
		{
			this.settings = settings ?? new DefaultSettings();
		}

		/// <summary>
		/// This function is for smoothing (or adding) fade in or fade out animation.
		/// </summary>
		static float Interpolate(float value)
		{
#if false
			value = 1 - value;
			const float pivotValue = 0.0f;

			if (value < pivotValue)
			{
				return MathHelper.SmoothStep(0f, 1f, value / pivotValue);
			}
			else
			{
				return MathHelper.SmoothStep(1f, 0f, (value - pivotValue) / (1 - pivotValue));
			}
#elif true
			return MathHelper.SmoothStep(0f, 1f, value);
#else
			return value;
#endif
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

			CreateLinesIfNeeded();

			// Update lines' model
			for (int i = 0; i < numberOfLines; i++)
			{
				var line = lines[i];

				// Decrease line's time to live
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

		void CreateLinesIfNeeded()
		{
			if (settings.NumberOfLines != numberOfLines &&
				(settings.NumberOfLines > 0 && settings.NumberOfLines <= MaxHandleLines))
			{
				numberOfLines = settings.NumberOfLines;

				// Constructing array of handle lines

				var deltaAngle = (float)(2 * Math.PI) / numberOfLines;
				int i = 0;

				for (float d = 0; d < 2 * Math.PI; d += deltaAngle)
				{
					var handleLine = new HandleLine(d);

					// Mark already visited lines
					if (d < angle)
					{
						handleLine.timesVisited = timesCircled;
					}

					lines[i++] = handleLine;
				}

			}
		}

		public override void Draw(GameTime gameTime)
		{
			base.Draw(gameTime);

			var viewportSize = MainGame.ViewportSize;
			var unitSize = MainGame.UnitSize;
			var handleSize = unitSize * settings.LineSize;
			var handleOffset = unitSize * settings.LineOffset;
			var xCenter = viewportSize.Width / 2f;
			var yCenter = viewportSize.Height / 2f;

			// Draw lines
			for (int i = 0; i < numberOfLines; i++)
			{
				var line = lines[i];

				float colorValue = line.relativeTimeToLive;
				if (colorValue > 0)
				{
					colorValue = Interpolate(colorValue);

					Color color = new Color(colorValue, colorValue, colorValue);

					MainGame.SpriteBatch.DrawLine(
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
