using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace wallDodger
{
	// maybe wait until the generation is finished before starting the timer.
	// make generate terrain return as bool. not generate zigzag. only when generate zigzag finishes will true be returned.
	//		or maybe just set a local default bool to isFinished or something idk.
	delegate void OnTerrainGenerationDelegate(WallManager wallManager, Texture2D wallAsset);

	class TerrainGeneration
	{
		private Random generator;
		private Zigzag zigzag;
		private TerrainTypes currentTerrain;
		private double timePerTerrain;
		private double timeCounter;

		public TerrainGeneration()
		{
			generator = new Random();
			zigzag = new Zigzag();
			timePerTerrain = 15.0; // New terrain generated every 15s.
		}

		public void ChooseTerrainType()
		{
			// No terrain generated
			if (generator.Next(2) == 0)
			{
				currentTerrain = TerrainTypes.DefaultRandom;
			}
			// Terrain generated. Randomly select a type.
			else
			{
				currentTerrain = (TerrainTypes)(generator.Next(1, 1));
			}
		}

		// update method required, else currentTerrain will keep changing
		public void Update(GameTime gameTime)
		{
			timeCounter += gameTime.ElapsedGameTime.TotalSeconds;

			if (timeCounter >= timePerTerrain)
			{
				ChooseTerrainType(); // Chance of new terrain being generated
				timeCounter = 0;
			}
		}

		public bool GenerateTerrain(WallManager wallManager, Texture2D wallAsset)
		{
			bool isFinished = false;

			switch (currentTerrain)
			{
				case (TerrainTypes.DefaultRandom):
					{
						isFinished = true;
						return isFinished;
					}
				case (TerrainTypes.Zigzag):
					{
						//zigzag.GenerateZigzag(wallManager, wallAsset);
						isFinished = true;
						return isFinished;
					}
				default:
					{
						return isFinished;
					}
				//case (TerrainTypes.BigZigzag):
				//	{
				//		// do something
				//		break;
				//	}
				//case (TerrainTypes.GapIrregularity):
				//	{
				//		// do something
				//		break;
				//	}
				//case (TerrainTypes.Obstacle):
				//	{
				//		// do something
				//		break;
				//	}
				//case (TerrainTypes.Straightaway):
				//	{
				//		// do something
				//		break;
				//	}
				//case (TerrainTypes.AbruptTurn):
				//	{
				//		// do something
				//		break;
				//	}
			}
		}
	}
}
