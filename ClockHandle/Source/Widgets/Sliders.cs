using ClockHandle.Game;
using Microsoft.Xna.Framework;
using Myra.Graphics2D.UI;

namespace ClockHandle.Widgets
{
	public class Sliders : WidgetComponent
	{
		HandleAnimationSettings settings;

		Myra.Graphics2D.UI.Desktop host;

		const float MaxLineOffset = 40f;

		const float MaxLineSize = 40f;

		public Sliders(IGame mainGame, HandleAnimationSettings settings) : base(mainGame)
		{
			this.settings = settings;
		}

		protected override void LoadContent()
		{
			base.LoadContent();

			var grid = new Grid
			{
				RowSpacing = 8,
				ColumnSpacing = 8
			};

			const float bottomMargin = 10;
			const float leftRightMargin = 20;
			const float betweenMargin = 10;

			grid.ColumnsProportions.Add(new Grid.Proportion(Grid.ProportionType.Pixels, leftRightMargin));
			grid.ColumnsProportions.Add(new Grid.Proportion(Grid.ProportionType.Auto));
			grid.ColumnsProportions.Add(new Grid.Proportion(Grid.ProportionType.Pixels, betweenMargin));
			grid.ColumnsProportions.Add(new Grid.Proportion(Grid.ProportionType.Fill));
			grid.ColumnsProportions.Add(new Grid.Proportion(Grid.ProportionType.Pixels, leftRightMargin));
			grid.RowsProportions.Add(new Grid.Proportion(Grid.ProportionType.Fill));
			grid.RowsProportions.Add(new Grid.Proportion(Grid.ProportionType.Auto));
			grid.RowsProportions.Add(new Grid.Proportion(Grid.ProportionType.Auto));
			grid.RowsProportions.Add(new Grid.Proportion(Grid.ProportionType.Auto));
			grid.RowsProportions.Add(new Grid.Proportion(Grid.ProportionType.Pixels, bottomMargin));

			// Labels

			grid.Widgets.Add(new TextBlock
			{
				Text = "Number of Lines",
				GridPositionX = 1,
				GridPositionY = 1
			});
			grid.Widgets.Add(new TextBlock
			{
				Text = "Line Size",
				GridPositionX = 1,
				GridPositionY = 2
			});
			grid.Widgets.Add(new TextBlock
			{
				Text = "Line Offset",
				GridPositionX = 1,
				GridPositionY = 3
			});

			// Sliders

			HorizontalSlider numberOfLinesSlider;
			HorizontalSlider lineSizeSlider;
			HorizontalSlider lineOffsetSlider;

			// Create and add sliders

			grid.Widgets.Add(numberOfLinesSlider = new HorizontalSlider
			{
				GridPositionX = 3,
				GridPositionY = 1,
				Value = (float)settings.NumberOfLines / (float)HandleAnimation.MaxHandleLines * 100
			});
			grid.Widgets.Add(lineSizeSlider = new HorizontalSlider
			{
				GridPositionX = 3,
				GridPositionY = 2,
				Value = settings.LineSize / MaxLineOffset * 100
			});
			grid.Widgets.Add(lineOffsetSlider = new HorizontalSlider
			{
				GridPositionX = 3,
				GridPositionY = 3,
				Value = settings.LineOffset / MaxLineSize * 100
			});

			// Attach events to sliders

			numberOfLinesSlider.ValueChanged += (sender, args) =>
			{
				settings.NumberOfLines = (int)(numberOfLinesSlider.Value / 100f * HandleAnimation.MaxHandleLines);
			};

			lineSizeSlider.ValueChanged += (sender, args) =>
			{
				settings.LineSize = (int)(lineSizeSlider.Value / 100f * MaxLineOffset);
			};

			lineOffsetSlider.ValueChanged += (sender, args) =>
			{
				settings.LineOffset = (int)(lineOffsetSlider.Value / 100f * MaxLineSize);
			};

			host = new Myra.Graphics2D.UI.Desktop();
			host.Widgets.Add(grid);
		}

		public override void Draw(GameTime gameTime)
		{
			base.Draw(gameTime);

			host.Bounds = new Rectangle(0, 0, GraphicsDevice.PresentationParameters.BackBufferWidth, GraphicsDevice.PresentationParameters.BackBufferHeight);
			host.Render();
		}
	}
}
