using System;
using ClockHandle.Widgets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using Myra;
using Myra.Graphics2D.Text;

namespace ClockHandle.Desktop
{
	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	public class ClockHandleGame : Microsoft.Xna.Framework.Game, ClockHandle.Game.IGame
	{
		#region Variables

		GraphicsDeviceManager graphics;

		HandleAnimationSettings settings;

		SpriteBatch spriteBatch;

		SpriteFont font;

		#endregion

		public ClockHandleGame()
		{
			settings = new HandleAnimationSettings()
			{
				LineSize = 10f,
				LineOffset = 5f,
				NumberOfLines = 8,
			};

			Components.Add(new HandleAnimationComponent(this, settings));
			Components.Add(new Sliders(this, settings));

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
			MyraEnvironment.Game = this;

			base.Initialize();
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			LoadFont();
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch(GraphicsDevice);

			base.LoadContent();
		}

		void LoadFont()
		{
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

			base.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.Black);

			#region Draw logic

			spriteBatch.Begin();

			base.Draw(gameTime);

			spriteBatch.End();

			#endregion
		}

		#region IGame

		public SpriteBatch SpriteBatch => spriteBatch;

		public Microsoft.Xna.Framework.Game MonoGame => this;

		public Rectangle ViewportSize => GraphicsDevice.Viewport.Bounds;

		public SpriteFont DefaultFont => font;

		public float UnitSize
		{
			get
			{
				var viewportSize = ViewportSize;
				return Math.Min(viewportSize.Width, viewportSize.Height) / 80f;
			}
		}

		#endregion
	}
}
