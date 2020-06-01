 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wallDodger
{
	class AbruptTurn : Terrain
	{

		// Variables controlling size of offset
		private int lowerLowerOffsetBound;
		private int upperLowerOffsetBound;
		private int lowerUpperOffsetBound;
		private int upperUpperOffsetBound;

		public AbruptTurn()
		{
			lowerLowerOffsetBound = -75;
			upperLowerOffsetBound = -59;
			lowerUpperOffsetBound = 60;
			upperUpperOffsetBound = 76;
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
			// Check to see whether the left wall, if affected, will spawn off screen
			if (previousLeftWallX + Wall.WallWidth < Math.Abs(upperLowerOffsetBound) + 7) // + 7 to help optimise generation
			{
				// move right
			}
			// This block will run if the left wall will spawn off-screen
			//		Same thing as above - check the right wall now.
			else if (Game1.WindowWidth - previousRightWallX < lowerUpperOffsetBound - 7) // - 7 to help optimise generation
			{
				// move left
			}
			// This block runs if neither of the walls will spawn off-screen
			else
			{
				// randomly choose to go left or right
			}
			int offset = generator.Next(lowerOffsetBound, upperOffsetBound);

			// Generate a random offset until a suitable value is obtained.
			while (previousLeftWallX + Wall.WallWidth + offset < 0
				|| previousRightWallX + offset > Game1.WindowWidth)
			{
				offset = generator.Next(lowerOffsetBound, upperOffsetBound);
			}

			// Return the obtained suitable value.
			return offset;
		}
	}
}
