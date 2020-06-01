using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace wallDodger
{
	// Inherits everything from Zigzag, and thus, Terrain
	class BigZigzag : Zigzag
	{
		// No need to write ": base()" - Zigzag uses the default constructor,  
		//		and so the compiler calls it regardless of whether or not it  
		//		is explicitly instructed to. Only if the parent class utilises
		//		a parameterised constructor is including ": base(params)" 
		//		imperative.
		public BigZigzag()
		{
			// After at most 7 wall pairs deviation direction changes.
			maxDeviationLength = 7;

			currentDeviationWall = 0;

			// -1 represents left movement, 1 right movement
			deviationDirections = new int[] { -1, 1 };

			// Total zigzag length = 35 wall pairs
			TotalWallPairsInTerrain = 35;

			// Each wall pair is offset 47px from the previous
			deviationSize = new Vector2(47, 0);

			// Random starting deviation direction
			currentDeviationDirection = deviationDirections[generator.Next(2)];
		}

		// Inherits everything else from the parent. Only a change of values is
		//		required.
	}
}
