using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace wallDodger
{
	class Player : GameObject
	{
		// Fields

		// Variable to store initial player position - useful for when a new 
		//		game is started
		private Vector2 initialPosition;

		// Dimension variables for player sprite
		private int playerWidth;
		private int playerHeight;

		private double playerDiagonal;

		// Variables that control the speed of player movement across the screen
		public Vector2 StrafeVelocity { get; private set; }
		private Vector2 initialStrafeVelocity;
		private Vector2 incrementFactorStrafeVelocity;

		public Player(Texture2D asset) : base (asset)
		{
			playerWidth = 20;
			playerHeight = playerWidth;

			// Using Pythagorean Theorem to obtain length of the square's diagonal
			playerDiagonal = Math.Sqrt(Math.Pow(playerWidth, 2) + Math.Pow(playerHeight, 2));

			this.asset = asset;
			initialPosition = new Vector2(Game1.WindowWidth / 2 - (float)(playerWidth / 2), Game1.WindowHeight - 50);
			Position = initialPosition;
			Tracker = new Rectangle(
				(int)initialPosition.X,
				(int)initialPosition.Y,
				playerWidth,
				playerHeight);

			initialStrafeVelocity = new Vector2(3, 0);
			incrementFactorStrafeVelocity = new Vector2(1, 0);
			StrafeVelocity = initialStrafeVelocity;
		}

		/// <summary>
		/// Draws the player sprite to the screen.
		/// </summary>
		/// <param name="spriteBatch">
		/// The SpriteBatch object used to draw with.
		/// </param>
		public void Draw(SpriteBatch spriteBatch)
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
			spriteBatch.Draw(asset, Tracker, Color.Blue);
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
			if (Tracker.Intersects(wall.Tracker))
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
		public override void UpdateTracker()
		{
			Tracker = new Rectangle(
				(int)Position.X,
				(int)Position.Y,
				playerWidth,
				playerHeight);
		}

		/// <summary>
		/// Resets the player to their initial position. Called whenever a new 
		///		game is started.
		/// </summary>
		public void Reset()
		{
			Position = initialPosition;
			StrafeVelocity = initialStrafeVelocity;
			UpdateTracker();
		}

		/// <summary>
		/// Increments the speed at which the player can strafe across the 
		///		screen.
		/// </summary>
		public void LevelUp(int level)
		{
			// Only increment strafe velocity every 2 levels.
			if (level % 2 == 0)
			{
				StrafeVelocity += incrementFactorStrafeVelocity;
			}
		}
	}
}
