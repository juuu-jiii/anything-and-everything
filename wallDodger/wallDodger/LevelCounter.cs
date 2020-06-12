using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace wallDodger
{
	class LevelCounter : ProgressCounter
	{
		// Constructor
		public LevelCounter(SpriteFont textFont) : base(textFont, new Vector2(20, 40))
		{
			Value = 1;

			// The player must last 10s on a level to be promoted.
			timePerUnit = 10.0;
		}

		/// <summary>
		/// Increments the current level if the player survives for long enough.
		/// </summary>
		/// <param name="gameTime">
		/// Provides a snapshot of timing values.
		/// </param>
		/// <param name="wallManager">
		/// The WallManager object.
		/// </param>
		/// <param name="player">
		/// The Player object.
		/// </param>
		public void LevelUp(GameTime gameTime, WallManager wallManager, Player player)
		{
			// Time passing
			timeCounter += gameTime.ElapsedGameTime.TotalSeconds;

			// Increment the level if the player lasts long enough.
			if (timeCounter >= timePerUnit)
			{
				// Same process as with updating score
				Value += 1;
				timeCounter = 0;

				// Adjusting other game components as necessary
				wallManager.LevelUp(Value);
				player.LevelUp(Value);
			}
		}

		/// <summary>
		/// Draws the current level to the screen.
		/// </summary>
		/// <param name="spriteBatch">
		/// The SpriteBatch object used to draw with.
		/// </param>
		public override void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.DrawString(
				textFont,
				Value.ToString(),
				textPosition,
				Color.White);
		}

		/// <summary>
		/// Resets the level and other applicable fields to default values. 
		///		Called each time a new game is started.
		/// </summary>
		public override void Reset()
		{
			Value = 1;

			// Not resetting this causes a small discrepancy in time, thus
			//		impacting future LevelUp() calls.
			timeCounter = 0;
		}
	}
}
