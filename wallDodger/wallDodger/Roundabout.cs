using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace wallDodger
{
	class Roundabout : Terrain
	{
		private Vector2 deviationSize;
		private int gapSizeReductionFactor;

		public Roundabout()
		{
			TotalWallPairsInTerrain = 26;
			deviationSize = new Vector2(10, 0);
			gapSizeReductionFactor = 30;
		}

		// Unnecessary method; included because overriding is required
		public override float GenerateNext(float previousLeftWallX, float previousRightWallX)
		{
			throw new NotImplementedException();
		}

		public Tuple<float, int> GenerateNext(
			float previousLeftWallX,
			int currentWallInTerrain,
			int gapSize)
		{
			// make use of the horizontal symmetry
			if (currentWallInTerrain < 5 || currentWallInTerrain > 21)
			{
				return new Tuple<float, int>(previousLeftWallX, gapSize);
			}
			else if (currentWallInTerrain < 10)
			{
				// decreasing width (linear function)
				return new Tuple<float, int>(
					previousLeftWallX + deviationSize.X,
					gapSize - gapSizeReductionFactor);
			}
			else if (currentWallInTerrain >= 10)
			{
				// negative sqrt function + obstacle
				// + OBSTACLE
				return new Tuple<float, int>(
					(float)-Math.Sqrt(previousLeftWallX),
					gapSize + (int)Math.Sqrt(previousLeftWallX) * 2);
			}
			else if (currentWallInTerrain <= 16)
			{
				// positive sqrt function + obstacle
				// + OBSTACLE
				return new Tuple<float, int>(
					(float)Math.Sqrt(previousLeftWallX),
					gapSize - (int)Math.Sqrt(previousLeftWallX) * 2);
			}
			else //else if (currentWallInTerrain > 16) // or just do else so all code paths return a value
			{
				// increasing width (linear function)
				return new Tuple<float, int>(
					previousLeftWallX - deviationSize.X,
					gapSize + gapSizeReductionFactor);
			}

		}
	}
}
