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
		private Texture2D wallAsset;
		private const int WallWidth = 100;
		private const int WallHeight = 35;
		private Vector2 Position { get; set; }

		// Parameterised constructor
		public Wall(Texture2D wallAsset, int x, int y)
		{
			this.wallAsset = wallAsset;
			Position = new Vector2(x, y);
		}

		/// <summary>
		/// Draws Wall objects to the screen.
		/// </summary>
		/// <param name="spriteBatch">
		/// The SpriteBatch object used to draw with.
		/// </param>
		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw
				(
				wallAsset,
				new Rectangle((int)Position.X, (int)Position.Y, WallWidth, WallHeight),
				Color.White
				);
		}
	}
}
