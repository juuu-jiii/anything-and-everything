using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wallDodger
{
	class DefaultRandom : Terrain
	{

		// Variables controlling size of offset
		private int lowerOffsetBound;
		private int upperOffsetBound;

		public DefaultRandom()
		{
			lowerOffsetBound = -50;
			upperOffsetBound = 51;
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
