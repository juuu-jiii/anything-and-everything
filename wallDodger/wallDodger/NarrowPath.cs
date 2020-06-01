using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wallDodger
{
	// Inherits everything from NarrowSpace, and thus, Terrain
	class NarrowPath : NarrowSpace
	{
		// No need to write ": base()" - same reason as with BigZigzag.
		public NarrowPath()
		{
			TotalWallPairsInTerrain = 7;
		}

		// Inherits everything else from the parent. Only a change of values is
		//		required.
	}
}
