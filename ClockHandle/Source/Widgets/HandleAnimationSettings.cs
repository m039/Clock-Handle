using System;
namespace ClockHandle.Widgets
{
	public class HandleAnimationSettings : HandleAnimation.ISettings
	{
		int numberOfLines;

		public int NumberOfLines { get => numberOfLines; set => numberOfLines = value; }

		float lineSize;

		public float LineSize { get => lineSize; set => lineSize = value; }

		float lineOffset;

		public float LineOffset { get => lineOffset; set => lineOffset = value; }
	}
}
