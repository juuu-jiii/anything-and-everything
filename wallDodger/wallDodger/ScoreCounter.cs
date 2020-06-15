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

		// Array to keep track of high scores
		// Properties can be used here, because unlike lists, arrays do not 
		//		have methods like Remove() and Clear() which could cause data
		//		corruption.
		public int[] HiScores { get; private set; }

		private string saveFile;

		// Constructor
		public ScoreCounter(SpriteFont textFont) : base (textFont, new Vector2(20, 20))
		{
			Value = 0;

			// Game stores up to 5 local high scores
			HiScores = new int[] { 0, 0, 0, 0, 0 };

			saveFile = "save.txt";

			InitialiseSave();
		}

		/// <summary>
		/// Helper method that creates and initialises a local save file if 
		///		one does not already exist.
		/// </summary>
		private void InitialiseSave()
		{
			if (!File.Exists(saveFile))
			{
				File.Create(saveFile);

				// Initialise the save file with 5 score "slots".
				try
				{
					writeStream = File.OpenWrite(saveFile);

					writer = new BinaryWriter(writeStream);

					for (int i = 0; i < 5; i++)
					{
						// New save file. All scores set to 0.
						writer.Write(0);
					}
				}
				catch (Exception e)
				{
					Console.WriteLine(e.Message);
				}
				finally
				{
					if (writer != null)
					{
						writer.Close();
					}
				}
			}
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

		// ********************************************************************
		// ---FILE I/O METHODS---
		// ********************************************************************

		public void LoadScores()
		{
			try
			{
				// Open the file. Store the open file in a FileStream variable.
				readStream = File.OpenRead(saveFile);

				// BinaryReader is responsible for reading from the open file passed in (contained in readStream).
				reader = new BinaryReader(readStream);

				// Read all 5 scores in the save file and store them in the array.
				for (int i = 0; i < HiScores.Length; i++)
				{
					HiScores[i] = reader.ReadInt32();
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
			finally
			{
				if (reader != null)
				{
					reader.Close();
				}
			}
		}

		public void SaveScores()
		{
			try
			{
				// Open the file. Store the open file in a FileStream variable.
				writeStream = File.OpenWrite(saveFile);

				// BinaryWriter is responsible for writing to the open file passed in (contained in writeStream).
				writer = new BinaryWriter(writeStream);

				// Write all values stored in HiScores to the saveFile.
				for (int i = 0; i < HiScores.Length; i++)
				{
					writer.Write(HiScores[i]);
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
			finally
			{
				if (writer != null)
				{
					writer.Close();
				}
			}
		}

		public void UpdateScores(int currentScore)
		{
			// On its own this should be a sufficient replacement for a sorting
			//		algorithm of any sort.
			
			// Loop through HiScores. Check if past scores have been beaten.
			// Must check through scores in descending order, otherwise lower
			//		scores that have been beaten will also be overwritten.
			for (int i = 0; i < HiScores.Length; i++)
			{
				// If a past score has been beaten...
				if (currentScore > HiScores[i])
				{
					// ...shift remaining scores in HiScores down a position...
					for (int j = HiScores.Length - 2; j >= i; j--)
					{
						HiScores[j + 1] = HiScores[j];
					}

					// ...replace the proper position with the new score...
					HiScores[i] = currentScore;

					// ...and break out of the loop early if applicable.
					break;
				}
			}
		}
	}
}
