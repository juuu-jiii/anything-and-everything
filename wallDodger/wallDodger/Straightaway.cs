using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wallDodger
{
	class Straightaway : Terrain
	{
		public Straightaway()
		{
			// Total length of terrain is 10;
			//		9 pairs + 1 starting pair, from which the rest are copied.
			TotalWallPairsInTerrain = 9;
		}

		// Unnecessary method; included because overriding is required
		public override float GenerateNext(float previousLeftWallX, float previousRightWallX)
		{
			throw new NotImplementedException();
		}
	}
}
