using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace wallDodger
{

	delegate void OnSpawnWallPairDelegate(WallManager wallManager);

	enum TerrainTypes
	{
		DefaultRandom,		// (Default) Randomly-generated offset walls
		Zigzag,				// Zigzag formation
		BigZigzag,			// Longer, more exaggerated zigzag formation
		NarrowSpace,		// Narrower gap size than usual, single wall pair
		NarrowPath,			// Narrower gap size than usual, pathway
		Obstacle,			// Wall object in middle of path
		Straightaway,		// A straight section
		AbruptTurn,			// Bigger offset than usual
		Roundabout			// Rotary
	}

	class WallManager
	{
		// Fields
		
		// Instance variable of the class to enforce singleton design pattern
		private static WallManager wallManagerInstance;
		
		// List variables to store Wall objects
		private List<Wall> rightWalls;
		private List<Wall> leftWalls;

		private Random generator;

		// Variables representing reference points for spawning and despawning
		private Point spawner;
		private Point despawner;

		// Variables controlling the speed at which the world scrolls
		public Vector2 ScrollVelocity { get; set; }
		private Vector2 initialScrollVelocity;
		private Vector2 incrementFactorScrollVelocity;

		public int GapSize { get; set; }

		// x-coordinate of left Wall objects lining start "stretch"
		public const int InitialLeftWallXPosition = -350;

		// Variables storing x- and y-coordinates of the most recently added wall pair.
		private float lastLeftWallX;
		private float lastLeftWallY;
		private float lastRightWallX;

		public Color[] WallColourArray { get; private set; }

		private Color currentWallColour;

		// ---TERRAIN GENERATION VARIABLES---
		private DefaultRandom defaultRandom; 
		private Zigzag zigzag;
		private BigZigzag bigZigzag;
		private NarrowSpace narrowSpace;
		private NarrowPath narrowPath;
		private Obstacle obstacle;
		private Straightaway straightaway;
		private AbruptTurn abruptTurn;
		private Roundabout roundabout;

		public TerrainTypes currentTerrain { get;  private set; }
		private double timePerTerrain;
		private double timeCounter;

		// Variable to track the generated length of the current terrain.
		private int currentWallPairInTerrain;

		// Public static get property for the instance variable that returns 
		//		an instance of the class creating an instance if one does not 
		//		yet exist.
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

		// IEnumerables for both Wall lists - will be iterated through in game1
		//		when checking for collisions with the Player object
		public IEnumerable<Wall> LeftWalls
		{
			get
			{
				return leftWalls.AsEnumerable<Wall>();
			}
		}

		public IEnumerable<Wall> RightWalls
		{
			get
			{
				return rightWalls.AsEnumerable<Wall>();
			}
		}

		// PRIVATE constructor
		private WallManager(int windowHeight)
		{
			rightWalls = new List<Wall>();
			leftWalls = new List<Wall>();
			generator = new Random();

			// Spawner and despawner are above and below screen, respectively.
			spawner = new Point(0, -50);
			despawner = new Point(0, windowHeight + 50);

			initialScrollVelocity = new Vector2(0, 3);
			incrementFactorScrollVelocity = new Vector2(0, 1);
			ScrollVelocity = initialScrollVelocity;

			GapSize = 200;

			WallColourArray = new Color[] {
				Color.White,
				Color.Green,
				Color.Blue,
				Color.Red,
				Color.Yellow,
				Color.Purple,
				Color.Pink,
				Color.Orange,
				Color.Brown,
				Color.Black};

			currentWallColour = Color.White;

			defaultRandom = new DefaultRandom();
			zigzag = new Zigzag();
			bigZigzag = new BigZigzag();
			narrowSpace = new NarrowSpace();
			narrowPath = new NarrowPath();
			obstacle = new Obstacle();
			straightaway = new Straightaway();
			abruptTurn = new AbruptTurn();
			roundabout = new Roundabout();

			timePerTerrain = 5.0; // New terrain generated every 5s.
			currentWallPairInTerrain = 0;
		}

		/// <summary>
		/// Helper method that creates a pair of left and right walls, 
		///		appending them to their respective lists.
		/// </summary>
		/// <param name="wallAsset">
		/// The asset used for the Wall objects.
		/// </param>
		/// <param name="y">
		/// The y-position of the wall being created.
		/// </param>
		private void SpawnWallPair(Texture2D wallAsset, int y)
		{
			leftWalls.Add(new Wall(
				wallAsset, 
				InitialLeftWallXPosition, 
				y));
			rightWalls.Add(new Wall(
				wallAsset, 
				InitialLeftWallXPosition + Wall.WallWidth + GapSize,
				y));
		}

		// ***public version of SpawnWallPair()***
		/// <summary>
		/// Creates a pair of left and right walls, appending them to their 
		///		respective lists. Used to generate walls past the initial ones 
		///		created via the private SpawnWallPair(). The type of terrain
		///		generated is dependent on currentTerrain.
		/// </summary>
		/// <param name="wallAsset">
		/// The asset used for the Wall objects.
		/// </param>
		public void SpawnWallPair(Texture2D wallAsset)
		{
			// The type of terrain generated is dependent on currentTerrain.
			switch (currentTerrain)
			{
				// Randomly-generated offsets (default)
				case (TerrainTypes.DefaultRandom):
					{
						// Fix this and regulate spawning
						if (lastLeftWallY >= spawner.Y + Wall.WallHeight)
						{
							// Generate an offset used to determine the x-location of the next wall pair.
							float offset = defaultRandom.GenerateNext(lastLeftWallX, lastRightWallX);

							// Create a new wall pair using the generated offset.
							// Adding rightWall like this creates a dependency on GapSize.
							// If rightWall is added after leftWall, use leftWalls.Count - 2.
							rightWalls.Add(new Wall(
								wallAsset,
								lastLeftWallX + Wall.WallWidth + GapSize + offset,
								spawner.Y));
							leftWalls.Add(new Wall(
								wallAsset,
								lastLeftWallX + offset,
								spawner.Y));

							// Rewriting the code to add rightWalls like this does NOT create a dependency on
							//		GapSize. This way, reducing GapSize will not be as simple as altering the
							//		variable itself.
							// rightWalls.Add(new Wall(
							//	wallAsset,
							//	lastRightWallX + offset,
							//	spawner.Y));
						}

						break;
					}

				// Zigzag terrain
				case (TerrainTypes.Zigzag):
					{
						// Fix this and regulate spawning
						if (lastLeftWallY >= spawner.Y + Wall.WallHeight)
						{
							// Continue to generate the zigzag until it is long enough.
							if (currentWallPairInTerrain < zigzag.TotalWallPairsInTerrain)
							{
								// Generate a calculated offset used to determine the x-location 
								//		of the next wall pair.
								float offset = zigzag.GenerateNext(lastLeftWallX, lastRightWallX);

								// Create a new wall pair using the generated offset.
								rightWalls.Add(new Wall(
									wallAsset,
									lastLeftWallX + Wall.WallWidth + GapSize + offset,
									spawner.Y));
								leftWalls.Add(new Wall(
									wallAsset,
									lastLeftWallX + offset,
									spawner.Y));

								// Update the length of the terrain generated so far
								currentWallPairInTerrain++;
							}
							// This block executes once the zigzag has finished generating itself.
							else
							{
								// Reset variables controlling terrain generation so they do not
								//		interfere with future generation.
								currentWallPairInTerrain = 0;
								zigzag.Reset();

								// Default to randomly-generated offsets.
								currentTerrain = TerrainTypes.DefaultRandom;
							}
						}

						break;
					}

				// Longer, more exaggerated zigzag terrain
				case (TerrainTypes.BigZigzag):
					{
						// Fix this and regulate spawning
						if (lastLeftWallY >= spawner.Y + Wall.WallHeight)
						{
							// Continue to generate the big zigzag until it is long enough.
							if (currentWallPairInTerrain < bigZigzag.TotalWallPairsInTerrain)
							{
								// Generate a calculated offset used to determine the x-location 
								//		of the next wall pair.
								float offset = bigZigzag.GenerateNext(lastLeftWallX, lastRightWallX);

								// Create a new wall pair using the generated offset.
								rightWalls.Add(new Wall(
									wallAsset,
									lastLeftWallX + Wall.WallWidth + GapSize + offset,
									spawner.Y));
								leftWalls.Add(new Wall(
									wallAsset,
									lastLeftWallX + offset,
									spawner.Y));

								// Update the length of the terrain generated so far
								currentWallPairInTerrain++;
							}
							// This block executes once the big zigzag has finished generating itself.
							else
							{
								// Reset variables controlling terrain generation so they do not
								//		interfere with future generation.
								currentWallPairInTerrain = 0;
								bigZigzag.Reset();

								// Default to randomly-generated offsets.
								currentTerrain = TerrainTypes.DefaultRandom;
							}
						}

						break;
					}

				// Narrower gap size than usual, single wall pair
				case (TerrainTypes.NarrowSpace):
					{
						// Fix this and regulate spawning
						if (lastLeftWallY >= spawner.Y + Wall.WallHeight)
						{
							float offset = narrowSpace.GenerateNext(GapSize);

							// Generate one narrow space.
							if (currentWallPairInTerrain < narrowSpace.TotalWallPairsInTerrain)
							{
								switch (narrowSpace.AffectedWall)
								{
									// Left wall affected
									case (AffectedWall.Left):
										{
											// Create a new wall pair using the generated offset.
											rightWalls.Add(new Wall(
												wallAsset,
												lastLeftWallX + Wall.WallWidth + GapSize,
												spawner.Y));
											// Apply offset to the affected wall.
											leftWalls.Add(new Wall(
												wallAsset,
												lastLeftWallX + (GapSize - offset),
												spawner.Y));

											break;
										}
									// Right wall affected
									case (AffectedWall.Right):
										{
											// Create a new wall pair using the generated offset.
											// Apply offset to the affected wall.
											rightWalls.Add(new Wall(
												wallAsset,
												lastLeftWallX + Wall.WallWidth + offset,
												spawner.Y));
											leftWalls.Add(new Wall(
												wallAsset,
												lastLeftWallX,
												spawner.Y));

											break;
										}
								}

								// Update the length of the terrain generated so far
								currentWallPairInTerrain++;
							}
							else
							{
								// The next wall pair spawned will be a copy of the one just before the terrain began 
								//		generating.
								// Because the position of the right wall is dependent on the left, if the gap is on
								//		the right of the screen AND the left wall is offset, the next left wall will
								//		be spawned based off the affected previous one. There will thus be no 
								//		possible offset to generate for the right wall, causing the game to hang.
								rightWalls.Add(new Wall(
									wallAsset,
									rightWalls[rightWalls.Count - 1 - narrowSpace.TotalWallPairsInTerrain].Position.X,
									spawner.Y));
								leftWalls.Add(new Wall(
									wallAsset,
									leftWalls[leftWalls.Count - 1 - narrowSpace.TotalWallPairsInTerrain].Position.X,
									spawner.Y));

								// Reset variables controlling terrain generation so they do not
								//		interfere with future generation.
								currentWallPairInTerrain = 0;
								narrowSpace.Reset();

								// Default to randomly-generated offsets.
								currentTerrain = TerrainTypes.DefaultRandom;
							}
						}

						break;
					}

				// Narrower gap size than usual, pathway
				case (TerrainTypes.NarrowPath):
					{
						// Fix this and regulate spawning
						if (lastLeftWallY >= spawner.Y + Wall.WallHeight)
						{
							float offset = narrowPath.GenerateNext(GapSize);

							// Continue to generate the narrow path until it is long enough.
							if (currentWallPairInTerrain < narrowPath.TotalWallPairsInTerrain)
							{
								switch (narrowPath.AffectedWall)
								{
									// Left wall affected
									case (AffectedWall.Left):
										{
											// Apply offset to the affected wall.
											leftWalls.Add(new Wall(
												wallAsset,
												lastRightWallX - (GapSize - offset) - Wall.WallWidth,
												spawner.Y));
											rightWalls.Add(new Wall(
												wallAsset,
												lastRightWallX,
												spawner.Y));

											// Add to leftWalls first, basing spawn location on the 
											//		previous right wall.
											// This flip in dependency is necessary to prevent the
											//		wall pairs "moving" to the right from being
											//		based on the left wall pair, whose location is
											//		being modified every time the method is called.

											break;
										}
									// Right wall affected
									case (AffectedWall.Right):
										{
											// Create a new wall pair using the generated offset.
											// Apply offset to the affected wall.
											rightWalls.Add(new Wall(
												wallAsset,
												lastLeftWallX + Wall.WallWidth + offset,
												spawner.Y));
											leftWalls.Add(new Wall(
												wallAsset,
												lastLeftWallX,
												spawner.Y));

											break;
										}
								}

								// Update the length of the terrain generated so far
								currentWallPairInTerrain++;
							}
							else
							{
								// The next wall pair spawned will be a copy of the one just before the terrain began 
								//		generating.
								// Because the position of the right wall is dependent on the left, if the gap is on
								//		the right of the screen AND the left wall is offset, the next left wall will
								//		be spawned based off the affected previous one. There will thus be no 
								//		possible offset to generate for the right wall, causing the game to hang.
								rightWalls.Add(new Wall(
									wallAsset,
									rightWalls[rightWalls.Count - 1 - narrowPath.TotalWallPairsInTerrain].Position.X,
									spawner.Y));
								leftWalls.Add(new Wall(
									wallAsset,
									leftWalls[leftWalls.Count - 1 - narrowPath.TotalWallPairsInTerrain].Position.X,
									spawner.Y));

								// Reset variables controlling terrain generation so they do not
								//		interfere with future generation.
								currentWallPairInTerrain = 0;
								narrowPath.Reset();

								// Default to randomly-generated offsets.
								currentTerrain = TerrainTypes.DefaultRandom;
							}
						}

						break;
					}

				// Wall object in middle of path
				case (TerrainTypes.Obstacle):
					{
						// Fix this and regulate spawning
						if (lastLeftWallY >= spawner.Y + Wall.WallHeight)
						{
							// Generate one obstacle.
							if (currentWallPairInTerrain < obstacle.TotalWallPairsInTerrain)
							{
								// Obstacle mixed in with random offsets.
								float offset = defaultRandom.GenerateNext(lastLeftWallX, lastRightWallX);

								// Offset the next wall pair as with Random.
								rightWalls.Add(new Wall(
									wallAsset,
									lastLeftWallX + Wall.WallWidth + GapSize + offset,
									spawner.Y));
								leftWalls.Add(new Wall(
									wallAsset,
									lastLeftWallX + offset,
									spawner.Y));

								// Manually update, since obstacle generation depends on these values.
								lastLeftWallX = leftWalls[leftWalls.Count - 1].Position.X;
								lastRightWallX = rightWalls[rightWalls.Count - 1].Position.X;

								// Create an obstacle Wall object based on the location of the last wall pair.
								float obstacleLocation = obstacle.GenerateNext(lastLeftWallX, lastRightWallX);

								// Using the Wall constructor overload, and creating the obstacle as a 
								//		right wall object.
								rightWalls.Add(new Wall(
									wallAsset,
									obstacleLocation,
									spawner.Y,
									obstacle.ObstacleWidth));

								// Update the length of the terrain generated so far
								currentWallPairInTerrain++;
							}
							else
							{
								// Randomly offset the next wall pair. rightWalls[rightWalls.Count - 3]
								//		because the wall obstacle is counted as a right wall object.
								float offset = defaultRandom.GenerateNext(
									lastLeftWallX, 
									rightWalls[rightWalls.Count - 3].Position.X);

								// Must explicitly generate the next wall pair randomly in this way because
								//		regular scenarios depend on the most recently added wall pair to 
								//		generate the offset, which in this case would not work here, since 
								//		the most recently added right wall is the obstacle itself.
								rightWalls.Add(new Wall(
									wallAsset,
									lastLeftWallX + Wall.WallWidth + GapSize + offset,
									spawner.Y));
								leftWalls.Add(new Wall(
									wallAsset,
									lastLeftWallX + offset,
									spawner.Y));

								// Reset variables controlling terrain generation so they do not
								//		interfere with future generation.
								currentWallPairInTerrain = 0;

								// Default to randomly-generated offsets.
								currentTerrain = TerrainTypes.DefaultRandom;
							}
						}

						break;
					}

				// A straight section
				case (TerrainTypes.Straightaway):
					{
						// Fix this and regulate spawning
						if (lastLeftWallY >= spawner.Y + Wall.WallHeight)
						{
							// Continue to generate the straightaway until it is long enough.
							if (currentWallPairInTerrain < straightaway.TotalWallPairsInTerrain)
							{
								// Just copy-and-paste walls based on the location of the pair 
								//		before the terrain starts generating.
								rightWalls.Add(new Wall(
									wallAsset,
									lastLeftWallX + Wall.WallWidth + GapSize,
									spawner.Y));
								leftWalls.Add(new Wall(
									wallAsset,
									lastLeftWallX,
									spawner.Y));

								// Update the length of the terrain generated so far
								currentWallPairInTerrain++;
							}
							else
							{
								// Reset variables controlling terrain generation so they do not
								//		interfere with future generation.
								currentWallPairInTerrain = 0;

								// Default to randomly-generated offsets.
								currentTerrain = TerrainTypes.DefaultRandom;
							}
						}
						
						break;
					}

				// Bigger offset than usual
				case (TerrainTypes.AbruptTurn):
					{
						// Fix this and regulate spawning
						if (lastLeftWallY >= spawner.Y + Wall.WallHeight)
						{
							// Generate an offset used to determine the x-location of the next wall pair.
							float offset = abruptTurn.GenerateNext(lastLeftWallX, lastRightWallX);

							// Create a new wall pair using the generated offset.
							// Adding rightWall like this creates a dependency on GapSize.
							// If rightWall is added after leftWall, use leftWalls.Count - 2.
							rightWalls.Add(new Wall(
								wallAsset,
								lastLeftWallX + Wall.WallWidth + GapSize + offset,
								spawner.Y));

							leftWalls.Add(new Wall(
								wallAsset,
								lastLeftWallX + offset,
								spawner.Y));

							// Default to randomly-generated offsets.
							currentTerrain = TerrainTypes.DefaultRandom;
						}

						break;
					}

				// Legit a roundabout lol
				case (TerrainTypes.Roundabout):
					{
						// Fix this and regulate spawning
						if (lastLeftWallY >= spawner.Y + Wall.WallHeight)
						{
							// Continue to generate the roundabout until it is long enough.
							if (currentWallPairInTerrain < roundabout.TotalWallPairsInTerrain)
							{

								// Generate an offset used to determine the x-location of the next wall pair.
								Tuple<float, int, bool> roundaboutNext =
								roundabout.GenerateNext(lastLeftWallX, currentWallPairInTerrain, GapSize);

								// Updated GapSize is represented by the second value stored in the tuple.
								GapSize = roundaboutNext.Item2;

								// Whether an obstacle is present in the middle of the pathway is represented 
								//		by the third value stored in the tuple.
								// Basically if (there is no obstacle)
								if (!roundaboutNext.Item3)
								{
									// Because the method returns the x-position of the next left wall to spawn,
									//		leftWalls must be added to first.
									leftWalls.Add(new Wall(
										wallAsset,
										roundaboutNext.Item1,
										spawner.Y));

									// Update the tracker variable.
									lastLeftWallX = leftWalls[leftWalls.Count - 1].Position.X;

									// Use the updated tracker variable and gap size above to spawn a right wall
									//		 to complete the wall pair appropriately.
									rightWalls.Add(new Wall(
										wallAsset,
										lastLeftWallX + Wall.WallWidth + GapSize,
										spawner.Y));
								}
								// if (there is indeed a massive, annoying, bothersome obstacle in my path)
								else
								{
									// The same ritual as above, really.
									leftWalls.Add(new Wall(
										wallAsset,
										roundaboutNext.Item1,
										spawner.Y));

									lastLeftWallX = leftWalls[leftWalls.Count - 1].Position.X;

									rightWalls.Add(new Wall(
										wallAsset,
										lastLeftWallX + Wall.WallWidth + GapSize,
										spawner.Y));

									// Manually update, since obstacle generation depends on these values.
									lastLeftWallX = leftWalls[leftWalls.Count - 1].Position.X;
									lastRightWallX = rightWalls[rightWalls.Count - 1].Position.X;

									// Create an obstacle Wall object based on the location of the last wall pair.
									// Use the Roundabout GenerateNext() inherited from Obstacle to generate an
									//		x-coordinate.
									float obstacleLocation = roundabout.GenerateNext(lastLeftWallX, lastRightWallX);

									// Using the Wall constructor overload, and creating the obstacle as a 
									//		right wall object.
									// Use the updated ObstacleWidth to control the GapSize going through the 
									//		roundabout.
									rightWalls.Add(new Wall(
										wallAsset,
										obstacleLocation,
										spawner.Y,
										roundabout.ObstacleWidth));
								}

								// Don't forget to increment this!!
								currentWallPairInTerrain++;
							}
							else
							{
								// Reset variables controlling terrain generation so they do not
								//		interfere with future generation.
								currentWallPairInTerrain = 0;

								// Default to randomly-generated offsets.
								currentTerrain = TerrainTypes.DefaultRandom;
							}
						}

						break;
					}
			}
		}

		/// <summary>
		/// Removes the oldest Wall entries in both lists once they reach the 
		///		despawn height (y-coordinate of despawner).
		/// </summary>
		public void DespawnWallPair()
		{
			// Check positions of walls in both lists separately, in the event
			//		that an obstacle spawns. Removing from both lists after 
			//		checking only one of them will not work anymore.
			if (leftWalls[0].Position.Y >= despawner.Y)
			{
				// Remove the oldest wall.
				leftWalls.RemoveAt(0);
			}

			if (rightWalls[0].Position.Y >= despawner.Y)
			{
				rightWalls.RemoveAt(0);
			}
		}

		/// <summary>
		/// Moves all walls downward by looping through leftWalls and rightWalls,
		///		and adjusting each entry's y-position by ScrollSpeed.
		/// </summary>
		public void Scroll()
		{
			foreach (Wall leftWall in leftWalls)
			{
				leftWall.Position += ScrollVelocity;
				leftWall.UpdateTracker();
			}

			foreach (Wall rightWall in rightWalls)
			{
				rightWall.Position += ScrollVelocity;
				rightWall.UpdateTracker();
			}
		}

		/// <summary>
		/// Loops through both lists and draws their contents to the screen.
		/// </summary>
		/// <param name="spriteBatch">
		/// The SpriteBatch object used to draw with.
		/// </param>
		public void DrawAll(SpriteBatch spriteBatch)
		{
			foreach (Wall leftWall in leftWalls)
			{
				leftWall.Draw(spriteBatch, currentWallColour);
			}

			foreach (Wall rightWall in rightWalls)
			{
				rightWall.Draw(spriteBatch, currentWallColour);
			}
		}

		/// <summary>
		/// Clears both lists and initialises the start "stretch". Called 
		///		whenever a new game is started.
		/// </summary>
		/// <param name="wallAsset">
		/// The asset used for the Wall objects.
		/// </param>
		public void Reset(Texture2D wallAsset)
		{
			// Clear both lists.
			leftWalls.Clear();
			rightWalls.Clear();

			// This is necessary, since now GapSize is altered by Roundabout.
			// This must be placed here, because the subsequent private SpawnWallPair() calls
			//		rely on GapSize being set to its default.
			GapSize = 200;

			// Setting up the start "stretch".
			for (int i = Game1.WindowHeight; i >= -20; i -= Wall.WallHeight)
			{
				SpawnWallPair(wallAsset, i);
			}

			// Reset scroll velocity.
			ScrollVelocity = initialScrollVelocity;

			// Reset colours of Wall objects.
			currentWallColour = Color.White;

			// Start with a randomly-selected terrain.
			currentTerrain = ChooseTerrainType();

			// Reset applicable terrain generation variables.
			zigzag.Reset();
			bigZigzag.Reset();
			narrowSpace.Reset();
			narrowPath.Reset();

			// Prevent this from interfering new games.
			currentWallPairInTerrain = 0;
		}

		/// <summary>
		/// Changes the colours of Wall objects based on the current level.
		/// </summary>
		/// <param name="level">
		/// The current level.
		/// </param>
		public void ChangeWallColour(int level)
		{
			currentWallColour = WallColourArray[(level - 1) % 10];
		}
																								
		/// <summary>																			
		/// Speeds the game up and changes Wall colours. Called when the player					
		///		levels up.																		
		/// </summary>																			
		/// <param name="level">																
		/// The current level.																	
		/// </param>																			
		public void LevelUp(int level)															
		{																						
			ChangeWallColour(level);
			
			// Only increase scroll velocity every 2 levels.
			if (level % 2 == 0)
			{
				ScrollVelocity += incrementFactorScrollVelocity;
			}				
		}

		// ********************************************************************
		// ---TERRAIN GENERATION METHODS---
		// ********************************************************************
		/// <summary>
		/// Randomly selects a terrain type from the TerrainTypes enum. Set so 
		///		there is a 50% chance that a terrain other than DefaultRandom 
		///		will be selected. Of these other terrain types, all have an 
		///		equal probability of spawning.
		/// </summary>
		/// <returns>
		/// Returns a value from the TerrainTypes enum.
		/// </returns>
		public TerrainTypes ChooseTerrainType()
		{
			// No terrain generated
			if (generator.Next(1,2) == 0)
			{
				return TerrainTypes.DefaultRandom;
			}
			// Terrain generated. Randomly select a type. (only zigzag exists for now)
			else
			{
				return (TerrainTypes)(generator.Next(8,8));
			}
		}

		/// <summary>
		/// Updates variables with necessary data for proper terrain generation,
		///		and controls the frequency at which new terrain is generated.
		/// </summary>
		/// <param name="gameTime"></param>
		public void Update(GameTime gameTime)
		{
			// Update variables with the newly added wall pair above.
			// Besides the fact that the function of these lines of code is, as
			//		already stated above, to UPDATE, why call them here and 
			//		not in methods after each time a Wall pair gets appended?
			// HINT: Update() is called every frame, but Wall pairs do not get
			//		appended every frame.
			lastLeftWallX = leftWalls[leftWalls.Count - 1].Position.X;
			lastLeftWallY = leftWalls[leftWalls.Count - 1].Position.Y;
			lastRightWallX = rightWalls[rightWalls.Count - 1].Position.X;

			// Only start timer if current terrain type is DefaultRandom
			if (currentTerrain == TerrainTypes.DefaultRandom)
			{
				timeCounter += gameTime.ElapsedGameTime.TotalSeconds;
			}

			// Chance of new terrain being generated once enough time passes
			if (timeCounter >= timePerTerrain)
			{
				currentTerrain = ChooseTerrainType(); 
				timeCounter = 0;
			}
		}
	}
}