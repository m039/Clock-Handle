﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;

namespace ClockHandle.Desktop
{
	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	public class ClockHandleGame : Game, IGame
	{

		#region Variables

		GraphicsDeviceManager graphics;

		SpriteBatch spriteBatch;

		HandleAnimationComponent handleAnimationComponent;

		#endregion

		public ClockHandleGame()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			handleAnimationComponent = new HandleAnimationComponent(this);

			graphics.PreferredBackBufferWidth = 500;
			graphics.PreferredBackBufferHeight = 500;
			graphics.ApplyChanges();
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize()
		{
			#region Initialization logic

			handleAnimationComponent.Initialize();

			#endregion

			base.Initialize();
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch(GraphicsDevice);

			// TODO: use this.Content to load your game content here
		}

		/// <summary>
		/// UnloadContent will be called once per game and is the place to unload
		/// game-specific content.
		/// </summary>
		protected override void UnloadContent()
		{
			// TODO: Unload any non ContentManager content here
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime)
		{
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();

			#region Update logic

			handleAnimationComponent.Update(gameTime);

			#endregion

			base.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.Black);

			spriteBatch.Begin();

			base.Draw(gameTime);

			#region Draw logic

			handleAnimationComponent.Draw(gameTime);

			#endregion

			spriteBatch.End();
		}

		#region IGame

		public SpriteBatch SpriteBatch { get => spriteBatch; }

		public Game MonoGame => this;

		public Rectangle ViewportSize => GraphicsDevice.Viewport.Bounds;

		#endregion
	}
}