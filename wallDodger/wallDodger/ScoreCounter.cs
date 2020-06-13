using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO; // FILE IO WAHOOOOO

namespace wallDodger
{
	class ScoreCounter : ProgressCounter
	{
		// File IO fields
		private FileStream writeStream;
		private FileStream readStream;
		private BinaryWriter writer;
		private BinaryReader reader;
		
		// Constructor
		public ScoreCounter(SpriteFont textFont) : base (textFont, new Vector2(20, 20))
		{
			Value = 0;
		}

		/// <summary>
		/// Updates the player's score by adding points based on the elapsed 
		///		game time and their current level.
		/// </summary>
		/// <param name="gameTime">
		/// Provides a snapshot of timing values.
		/// </param>
		/// <param name="level">
		/// The player's current level.
		/// </param>
		public void Update(GameTime gameTime, int level)
		{
			// The rate at which points are awarded is playerLevel * 1 pts/s.
			// Rearrange this formula to get the amount of time between each
			//		score incrementation.
			// The higher the level, the higher the rate at which points are
			//		awarded, and the lower timePerPoint gets.
			// Note 1.0 is used here to avoid int division, which results in 
			//		undefined behaviour, since time should not be measured in
			//		ints.
			timePerUnit = 1.0 / level;

			// Time passing
			timeCounter += gameTime.ElapsedGameTime.TotalSeconds;

			// If enough time has passed to award a point...
			if (timeCounter >= timePerUnit)
			{
				// ...add a point to the player's current score...
				Value += 1;

				// ...and reset timeCounter for future score incrementations.
				timeCounter = 0;
			}

			// The way this is written ensures that the rate at which the 
			//		counter updates is dependent on the player level. If a 
			//		generic score += level is used here, the score updates at a
			//		constant rate instead. Moreover, with the latter method, 
			//		the score does not "count" upwards by 1 each time, but 
			//		rather adds a number to the total.
		}

		/// <summary>
		/// Draws the player's score to the screen.
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
		/// Resets the score and other applicable fields to default values. 
		///		Called each time a new game is started.
		/// </summary>
		public override void Reset()
		{
			Value = 0;

			// Not resetting this causes a small discrepancy in time, thus
			//		impacting score calculations.
			timeCounter = 0;
		}

		public void SaveScore(string filename)
		{
			try
			{
				// Open the file. Store the open file in a FileStream variable.
				writeStream = File.OpenWrite(filename);

				// BinaryWriter is responsible for writing to the open file passed in (contained in writeStream).
				writer = new BinaryWriter(writeStream);
			}
		}

		public void LoadScores(string filename)
		{

		}
	}
}
