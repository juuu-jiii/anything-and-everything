using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO; // FILE IO WAHOOOOO

namespace wallDodger
{
	class Leaderboard
	{
		// File IO fields
		private FileStream writeStream;
		private FileStream readStream;
		private BinaryWriter writer;
		private BinaryReader reader;

		// Array to keep track of high scores stored as a 2-tuple, where the 
		//		1st entry = initials and 2nd entry = score
		// Properties can be used here, because unlike lists, arrays do not 
		//		have methods like Remove() and Clear() which could cause data
		//		corruption.
		public Tuple<string, int>[] HiScores { get; private set; }

		// Text field in which the save file location is stored.
		private string saveFile;

		public Leaderboard()
		{
			// Game stores up to 5 local high scores
			HiScores = new Tuple<string, int>[] {
			new Tuple<string, int>("???", 0),
			new Tuple<string, int>("???", 0),
			new Tuple<string, int>("???", 0),
			new Tuple<string, int>("???", 0),
			new Tuple<string, int>("???", 0)
			};

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
						// New save file. All scores set to 0.
						writer.Write(0);
				}
				catch (Exception e)
				{
					Console.WriteLine(e.Message);
				}
				finally
				{
					if (writer != null)
						writer.Close();
				}
			}
		}

		/// <summary>
		/// Loads local leaderboard from a save file.
		/// </summary>
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
					string playerInitials = reader.ReadString();
					int playerScore = reader.ReadInt32();

					// Tuples in C# are immutable (like strings and structs), and so must
					//		be completely reassigned.
					HiScores[i] = new Tuple<string, int>(playerInitials, playerScore);

				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
			finally
			{
				if (reader != null)
					reader.Close();
			}
		}

		/// <summary>
		/// Saves local leaderboard to a file on the computer.
		/// </summary>
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
					writer.Write(HiScores[i].Item1);
					writer.Write(HiScores[i].Item2);
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
			finally
			{
				if (writer != null)
					writer.Close();
			}
		}

		/// <summary>
		/// Checks the current score against the leaderboard to determine if a 
		///		new record has been set.
		/// </summary>
		/// <param name="currentScore">
		/// The score earned from the current run.
		/// </param>
		/// <returns>
		/// Returns the position on the leaderboard the high score occupies, as
		///		an array index, if the current score is a high score. Otherwise,
		///		-1 is returned.
		/// </returns>
		public int IsHighScore(int currentScore)
		{
			// Loop through HiScores. Check if past scores have been beaten.
			// Must check through scores in descending order, to prevent the
			//		leaderboard from being read from and written to in the 
			//		wrong order.
			for (int i = 0; i < HiScores.Length; i++)
			{
				// If a past score has been beaten...
				if (currentScore > HiScores[i].Item2)
				{
					// ...exit the loop, returning the current index.
					return i;
				}
			}

			// If this line of code is reached, no high score was set - return -1.
			return -1;
		}

		/// <summary>
		/// Updates HiScores with new high score data.
		/// </summary>
		/// <param name="index">
		/// The position on the leaderboard the high score occupies.
		/// </param>
		/// <param name="playerInitials">
		/// The player's input initials.
		/// </param>
		/// <param name="currentScore">
		/// The score earned from the current run.
		/// </param>
		public void UpdateScores(int index, string playerInitials, int currentScore)
		{
			// On its own this should be a sufficient replacement for a sorting
			//		algorithm of any sort.

			// Shift remaining scores in HiScores down a position.
			for (int j = HiScores.Length - 2; j >= index; j--)
			{
				HiScores[j + 1] = HiScores[j];
			}

			// Insert the high score into the correct location in the array.
			HiScores[index] = new Tuple<string, int>(playerInitials, currentScore);
		}
	}
}
