using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wallDodger
{

	enum AffectedWall
	{
		Left,
		Right
	}

	class NarrowSpace : Terrain
	{
		// Fields

		// Variable tracking the wall whose position is to be altered.
		// protected set property: inherited and altered by NarrowPath
		public AffectedWall AffectedWall { get; protected set; }

		public NarrowSpace()
		{
			TotalWallPairsInTerrain = 1;

			// Initialise with a random value, ready for the first time the 
			//		terrain is generated.
			AffectedWall = (AffectedWall)generator.Next(2);
		}

		// Unnecessary method; included because overriding is required
		public override float GenerateNext(float previousLeftWallX, float previousRightWallX)
		{
			throw new NotImplementedException();
		}

		// ***GenerateNext overload***
		/// <summary>
		/// Calculates a value by which to offset the next affected wall's 
		///		x-position by.
		/// </summary>
		/// <param name="gapSize">
		/// The current distance between wall pairs.
		/// </param>
		/// <returns>
		/// Returns the aforementioned offset as a float.
		/// </returns>
		public float GenerateNext(int gapSize)
		{
			return gapSize / 2;
		}

		/// <summary>
		/// Resets AffectedWall to another randomly-selected value, in 
		///		preparation for the next time this terrain is generated.
		/// </summary>
		public void Reset()
		{
			AffectedWall = (AffectedWall)generator.Next(2);
		}
	}
}
