using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace wallDodger
{
	class Wall : GameObject
	{
		// Fields

		// Variables storing Wall object dimensions
		// Public constants are globally accessible within the solution.
		public const int WallWidth = 500;
		public const int WallHeight = 35;

		// Parameterised constructor
		public Wall(Texture2D asset, float x, float y) : base (asset)
		{
			this.asset = asset;
			Position = new Vector2(x, y);
			Tracker = new Rectangle(
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
		public void Draw(SpriteBatch spriteBatch, Color currentWallColour)
		{
			spriteBatch.Draw(
				asset,
				Tracker,
				currentWallColour);
		}

		/// <summary>
		/// Updates this Wall object's Rectangle with its current position.
		/// </summary>
		public override void UpdateTracker()
		{
			Tracker = new Rectangle(
				(int)Position.X,
				(int)Position.Y,
				WallWidth,
				WallHeight);
		}
	}
}
