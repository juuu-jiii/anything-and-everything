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

		// Array to keep track of high scores
		// Properties can be used here, because unlike lists, arrays do not 
		//		have methods like Remove() and Clear() which could cause data
		//		corruption.
		public int[] HiScores { get; private set; }

		// Text field in which the save file location is stored.
		private string saveFile;

		public Leaderboard()
		{
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
					HiScores[i] = reader.ReadInt32();
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
					writer.Write(HiScores[i]);
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
		/// Updates HiScores with new high score data where applicable.
		/// </summary>
		/// <param name="currentScore">
		/// The score earned from the current run.
		/// </param>
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
