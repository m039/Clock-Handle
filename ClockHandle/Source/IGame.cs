using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace ClockHandle
{
	public interface IGame
	{
		Microsoft.Xna.Framework.Game MonoGame { get; }

		SpriteBatch SpriteBatch { get; }

		Rectangle ViewportSize { get; }
	}
}
