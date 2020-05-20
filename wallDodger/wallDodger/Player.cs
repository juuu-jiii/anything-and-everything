using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace wallDodger
{
	class Player
	{
		private Texture2D playerAsset;
		private Vector2 initialPosition;
		private const int PlayerWidth = 20;
		private const int PlayerHeight = PlayerWidth;

		// Using Pythagorean Theorem to obtain length of the square's diagonal
		private double playerDiagonal = Math.Sqrt(Math.Pow(PlayerWidth, 2) + Math.Pow(PlayerHeight, 2));
		public Vector2 Position { get; set; }
		public Rectangle PlayerTracker { get; set; }
		public Vector2 StrafeVelocity { get; }

		public Player(Texture2D playerAsset)
		{
			this.playerAsset = playerAsset;
			initialPosition = new Vector2(Game1.WindowWidth / 2 - (float)(playerDiagonal / 2) + 15, Game1.WindowHeight - 50);
			Position = initialPosition;
			PlayerTracker = new Rectangle(
				(int)initialPosition.X,
				(int)initialPosition.Y,
				PlayerWidth,
				PlayerHeight);
			StrafeVelocity = new Vector2(5, 0);
		}

		/// <summary>
		/// Draws the player sprite to the screen.
		/// </summary>
		/// <param name="spriteBatch">
		/// The SpriteBatch object used to draw with.
		/// </param>
		public void Draw(SpriteBatch spriteBatch, Vector2 position)
		{
			//spriteBatch.Draw(
			//	playerAsset,
			//	position,
			//	null,
			//	Color.White,
			//	(float)(Math.PI / 4),
			//	new Vector2(PlayerWidth / 2, PlayerHeight / 2),
			//	0.25f,
			//	SpriteEffects.None,
			//	0);
			spriteBatch.Draw(playerAsset, PlayerTracker, Color.Blue);
		}

		/// <summary>
		/// Checks for collisions between Player and Wall objects.
		/// </summary>
		/// <param name="wall">
		/// The Wall object whose collision with the Player object is to be 
		///		checked for.
		/// </param>
		/// <returns>
		/// Returns true if a collision is detected, and false otherwise.
		/// </returns>
		public bool Collided(Wall wall)
		{
			if (PlayerTracker.Intersects(wall.WallTracker))
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// Updates this Player object's Rectangle with its current position.
		/// </summary>
		public void UpdateTracker()
		{
			PlayerTracker = new Rectangle(
				(int)Position.X,
				(int)Position.Y,
				PlayerWidth,
				PlayerHeight);
		}
	}
}
