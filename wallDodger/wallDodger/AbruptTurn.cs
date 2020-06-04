 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wallDodger
{
	class AbruptTurn : Terrain
	{
		// Private enum because it does not need to be accessed elsewhere.
		private enum Directions
		{
			Left,
			Right
		}

		// Variables controlling size of offset
		private int offsetLowerBound;
		private int offsetUpperBound;

		// Variable tracking the direction in which the next wall pair will be offset
		private Directions currentDirection;

		// Constructor
		public AbruptTurn()
		{
			offsetLowerBound = 80;
			offsetUpperBound = 101;
		}

		/// <summary>
		/// Helper method that returns a positive offset value, keeping in mind 
		///		the position of the offset wall pair in relation to the window.
		/// </summary>
		/// <param name="previousLeftWallX">
		/// x-coordinate of the previous left wall's top left corner.
		/// </param>
		/// <param name="previousRightWallX">
		/// x-coordinate of the previous right wall's top left corner.
		/// </param>
		/// <returns>
		/// Returns the positive offset as a floating-point number.
		/// </returns>
		private float GenerateRightOffset(float previousLeftWallX, float previousRightWallX)
		{
			float offset;

			// Generate a random offset until a suitable value is obtained.
			do
			{
				offset = generator.Next(offsetLowerBound, offsetUpperBound);
			}
			while (previousLeftWallX + Wall.WallWidth + offset < 0
			|| previousRightWallX + offset > Game1.WindowWidth);

			// +ve offset = rightward movement
			return offset;
		}

		/// <summary>
		/// Helper method that returns a negative offset value, keeping in mind 
		///		the position of the offset wall pair in relation to the window.
		/// </summary>
		/// <param name="previousLeftWallX">
		/// x-coordinate of the previous left wall's top left corner.
		/// </param>
		/// <param name="previousRightWallX">
		/// x-coordinate of the previous right wall's top left corner.
		/// </param>
		/// <returns>
		/// Returns the negative offset as a floating-point number.
		/// </returns>
		private float GenerateLeftOffset(float previousLeftWallX, float previousRightWallX)
		{
			float offset;

			// Generate a random offset until a suitable value is obtained.
			do
			{
				offset = generator.Next(offsetLowerBound, offsetUpperBound);
			}
			while (previousLeftWallX + Wall.WallWidth - offset < 0
			|| previousRightWallX - offset > Game1.WindowWidth);

			// -offset = leftward movement
			return -offset;
		}

		/// <summary>
		/// Generates a random offset based on the location of the previous 
		///		Wall pair in the WallManager object. Does not generate values 
		///		that result in the next Wall pair being drawn off-screen.
		/// </summary>
		/// <param name="previousLeftWallX">
		/// x-coordinate of the previous left wall's top left corner.
		/// </param>
		/// <param name="previousRightWallX">
		/// x-coordinate of the previous right wall's top left corner.
		/// </param>
		/// <returns>
		/// Returns an int offset value.
		/// </returns>
		public override float GenerateNext(float previousLeftWallX, float previousRightWallX)
		{
			float offset;

			// Check to see whether the left wall, if affected, will spawn off screen
			if (previousLeftWallX + Wall.WallWidth - offsetLowerBound < 7) // + 7 to help optimise generation
			{
				// Move right.
				offset = GenerateRightOffset(previousLeftWallX, previousRightWallX);
			}
			// This block will run if the left wall is likely to spawn off-screen.
			//		Same thing as above - check the right wall now.
			else if (previousRightWallX + offsetLowerBound > Game1.WindowWidth - 7) // - 7 to help optimise generation
			{
				// Move left.
				offset = GenerateLeftOffset(previousLeftWallX, previousRightWallX);
			}
			// This block runs if neither of the walls will spawn off-screen
			else
			{
				// Randomly choose to go left or right.
				currentDirection = (Directions)generator.Next(2);

				switch (currentDirection)
				{
					// Left movement
					case (Directions.Left):
						{
							offset = GenerateLeftOffset(previousLeftWallX, previousRightWallX);
							break;
						}
					// Right movement
					case (Directions.Right):
						{
							offset = GenerateRightOffset(previousLeftWallX, previousRightWallX);
							break;
						}
					// Default is needed here so the compiler can be sure offset is assigned a 
					//		value in all cases within this method.
					default:
						{
							Console.WriteLine("This shouldn't happen.");
							offset = -500;
							break;
						}
				}
			}

			// Return the obtained suitable value.
			return offset;
		}
	}
}
