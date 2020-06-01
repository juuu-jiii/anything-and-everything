using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Defines a class controlling terrain generation for one of the 8 types.

namespace wallDodger
{
	abstract class Terrain
	{
		// Fields
		protected Random generator;

		// Variable tracking the total length of the terrain
		// protected set property: inherited and altered by child classes
		public int TotalWallPairsInTerrain { get; protected set; }

		// Constructor
		public Terrain()
		{
			generator = new Random();
		}

		/// <summary>
		/// Calculates data required to generate the next wall pair.
		/// </summary>
		/// <param name="previousLeftWallX">
		/// The x-location of the most-recently added left wall.
		/// </param>
		/// <param name="previousRightWallX">
		/// The x-location of the most-recently added right wall.
		/// </param>
		/// <returns>
		/// Returns calculations as a float.
		/// </returns>
		public abstract float GenerateNext(float previousLeftWallX, float previousRightWallX);
	}
}
