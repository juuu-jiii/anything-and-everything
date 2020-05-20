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
		Texture2D wallAsset;

		// Public constants are globally accessible within the solution.
		public const int WallWidth = 500;
		public const int WallHeight = 35;
		public Vector2 Position { get; set; }

		// Rectangle object to detect Player object collision.
		public Rectangle WallTracker { get; set; }

		// Parameterised constructor
		public Wall(Texture2D wallAsset, float x, float y)
		{
			this.wallAsset = wallAsset;
			Position = new Vector2(x, y);
			WallTracker = new Rectangle(
				(int)x, 
				(int)y, 
				WallWidth, 
				WallHeight);
		}

		/// <summary>
		/// Draws the Wall object to the screen.
		/// </summary>
		/// <param name="spriteBatch">
		/// The SpriteBatch object used to draw with.
		/// </param>
		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(
				wallAsset,
				WallTracker,
				Color.White);
		}

		/// <summary>
		/// Updates this Wall object's Rectangle with its current position.
		/// </summary>
		public void UpdateTracker()
		{
			WallTracker = new Rectangle(
				(int)Position.X,
				(int)Position.Y,
				WallWidth,
				WallHeight);
		}
	}
}
