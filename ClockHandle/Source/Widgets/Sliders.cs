using ClockHandle.Game;
using Microsoft.Xna.Framework;
using Myra.Graphics2D.UI;

namespace ClockHandle.Widgets
{
	public class Sliders : WidgetComponent
	{
		Myra.Graphics2D.UI.Desktop _host;

		public Sliders(IGame mainGame) : base(mainGame)
		{
		}

		protected override void LoadContent()
		{
			base.LoadContent();

			var grid = new Grid
			{
				RowSpacing = 8,
				ColumnSpacing = 8
			};

			grid.ColumnsProportions.Add(new Grid.Proportion(Grid.ProportionType.Auto));
			grid.RowsProportions.Add(new Grid.Proportion(Grid.ProportionType.Auto));
			grid.RowsProportions.Add(new Grid.Proportion(Grid.ProportionType.Auto));

			var helloWorld = new TextBlock
			{
				Text = "Hello, World!"
			};
			grid.Widgets.Add(helloWorld);
			grid.Widgets.Add(helloWorld);
			grid.Widgets.Add(helloWorld);

			_host = new Myra.Graphics2D.UI.Desktop();
			_host.Widgets.Add(grid);
		}

		public override void Draw(GameTime gameTime)
		{
			base.Draw(gameTime);

			_host.Bounds = new Rectangle(0, 0, GraphicsDevice.PresentationParameters.BackBufferWidth, GraphicsDevice.PresentationParameters.BackBufferHeight);
			_host.Render();
		}
	}
}
