using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace wallDodger
{
	class Wall
	{
		// Fields
		public const int WallWidth = 500;
		public const int WallHeight = 35;
		public Vector2 Position { get; set; } // do we even need this?

		// Rectangle object to track player collision
		public Rectangle RectangleTracker { get; set; }

		// Parameterised constructor
		public Wall(float x, float y)
		{
			Position = new Vector2(x, y);
			RectangleTracker = new Rectangle(
				(int)Position.X, 
				(int)Position.Y, 
				WallWidth, 
				WallHeight);
		}

		/// <summary>
		/// Updates this Wall object's Rectangle with its current position.
		/// </summary>
		public void UpdateRectangle()
		{
			RectangleTracker = new Rectangle(
				(int)Position.X,
				(int)Position.Y,
				WallWidth,
				WallHeight);
		}
	}
}
