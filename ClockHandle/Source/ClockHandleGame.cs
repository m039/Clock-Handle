using ClockHandle.Widgets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;

namespace ClockHandle.Desktop
{
	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	public class ClockHandleGame : Microsoft.Xna.Framework.Game, ClockHandle.Game.IGame
	{
		class HandleAnimationSettings : HandleAnimationComponent.ISettings
		{
			int numberOfLines;

			public int NumberOfLines { get => numberOfLines; set => numberOfLines = value; }

			float lineSize;

			public float LineSize { get => lineSize; set => lineSize = value; }

			float lineOffset;

			public float LineOffset { get => lineOffset; set => lineOffset = value; }
		}

		#region Variables

		GraphicsDeviceManager graphics;

		HandleAnimationSettings settings;

		float testSettingsThreshold; // For testing purpose!

		SpriteBatch spriteBatch;

		SpriteFont font;

		HandleAnimationComponent handleAnimationComponent;

		#endregion

		public ClockHandleGame()
		{
			handleAnimationComponent = new HandleAnimationComponent(
				this, settings = new HandleAnimationSettings()
				{
					LineSize = 10f,
					LineOffset = 5f,
					NumberOfLines = 80,
				}
			);
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			IsMouseVisible = true;
			Window.AllowUserResizing = true;

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

			font = Content.Load<SpriteFont>("Fonts/Default Font");
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

			UpdateSettings(gameTime.GetElapsedSeconds());

			#region Update logic

			handleAnimationComponent.Update(gameTime);

			#endregion

			base.Update(gameTime);
		}

		/// For testing purpose for now!
		void UpdateSettings(float elapsedSeconds)
		{

			testSettingsThreshold += elapsedSeconds;
			if (testSettingsThreshold > 2)
			{
				if (settings.NumberOfLines != 15)
				{
					settings.NumberOfLines = 15;
					settings.LineSize = 10f;
					settings.LineOffset = 5f;
				}
				else
				{
					settings.NumberOfLines = 40;
					settings.LineSize = 12f;
					settings.LineOffset = 3f;
				}

				testSettingsThreshold = 0;
			}
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.Black);

			spriteBatch.Begin();

			#region Draw logic

			handleAnimationComponent.Draw(gameTime);

			spriteBatch.DrawString(font, "Hello", new Vector2(0, 0), Color.White);

			#endregion

			spriteBatch.End();

			base.Draw(gameTime);
		}

		#region IGame

		public SpriteBatch SpriteBatch => spriteBatch;

		public Microsoft.Xna.Framework.Game MonoGame => this;

		public Rectangle ViewportSize => GraphicsDevice.Viewport.Bounds;

		#endregion
	}
}
