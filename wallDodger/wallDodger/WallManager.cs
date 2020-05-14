using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace wallDodger
{
	class WallManager
	{
		// Private static field to hold one instance of this class - singleton
		private static WallManager wallManagerInstance;

		private LinkedList<Wall> rightWalls;
		private LinkedList<Wall> leftWalls;
		private Random generator;
		private Point spawner;
		private Point despawner;
		private int ScrollSpeed { get; set; }
		private int GapSize { get; set; }

		// Singleton pattern: private constructor
		private WallManager(int windowHeight)
		{
			rightWalls = new LinkedList<Wall>();
			leftWalls = new LinkedList<Wall>();
			generator = new Random();

			// Spawner and despawner are above and below screen, respectively.
			spawner = new Point(0, -50);
			despawner = new Point(0, windowHeight + 50);

			ScrollSpeed = 20;
			GapSize = 200;
		}

		// Public static get property that returns an instance of the class, 
		//		instantiating an object if not already done.
		public static WallManager WallManagerInstance
		{
			get
			{
				if (wallManagerInstance == null)
				{
					wallManagerInstance = new WallManager(Game1.WindowHeight);
					return wallManagerInstance;
				}
				else
				{
					return wallManagerInstance;
				}
			}
		}

		public void SpawnDespawnWallPair()
	}
}
