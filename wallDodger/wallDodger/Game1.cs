using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace wallDodger
{
	enum GameStates
	{
		StartScreen,
		Pregame,
		Playing,
		GameOver
	}

	enum PlayerStates
	{
		Idle,
		Active
	}
	
	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	public class Game1 : Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

		// Variables for the window width and height
		// Public constants are globally accessible within the solution.
		public const int WindowHeight = 650;
		public const int WindowWidth = 500;

		// Variables to keep track of player and game state
		GameStates gameState = GameStates.StartScreen;
		PlayerStates playerState = PlayerStates.Idle;

		// Variables to store texture and font assets
		Texture2D wall;
		Texture2D playerArrow;
		Texture2D whiteSquare;
		SpriteFont verdanaBold20;
		SpriteFont verdana12;
		SpriteFont verdanaSmall;

		// Variables to store instances of classes created
		WallManager wallManager;
		Player player;
		StartScreen startScreen;
		PregameScreen pregameScreen;
		GameOverScreen gameOverScreen;
		ScoreCounter scoreCounter;
		LevelCounter levelCounter;
		
		// Variables to track mouse and keyboard input
		KeyboardState kbState;
		MouseState mState;
		MouseState prevMState;

		public Game1()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize()
		{
			// TODO: Add your initialization logic here

			// Setting window size up
			graphics.PreferredBackBufferWidth = WindowWidth;
			graphics.PreferredBackBufferHeight = WindowHeight;
			graphics.ApplyChanges();

			wallManager = WallManager.WallManagerInstance;

			this.IsMouseVisible = true;

			base.Initialize();
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			// TODO: use this.Content to load your game content here

			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch(GraphicsDevice);

			wall = Content.Load<Texture2D>("greySquare");
			playerArrow = Content.Load<Texture2D>("greySquare");
			whiteSquare = Content.Load<Texture2D>("whiteSquare");
			verdanaBold20 = Content.Load<SpriteFont>("verdanaBold20");
			verdana12 = Content.Load<SpriteFont>("verdana12");
			verdanaSmall = Content.Load<SpriteFont>("verdanaSmall");

			player = new Player(playerArrow);
			startScreen = new StartScreen(verdanaBold20, verdana12, verdanaSmall, whiteSquare, whiteSquare);
			pregameScreen = new PregameScreen(verdana12, wall);
			gameOverScreen = new GameOverScreen(verdanaBold20, verdana12, wall, whiteSquare);
			scoreCounter = new ScoreCounter(verdana12);
			levelCounter = new LevelCounter(verdana12);
		}

		/// <summary>
		/// UnloadContent will be called once per game and is the place to unload
		/// game-specific content.
		/// </summary>
		protected override void UnloadContent()
		{
			// TODO: Unload any non ContentManager content here
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime)
		{
			//if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
			//	Exit();

			// TODO: Add your update logic here

			kbState = Keyboard.GetState();
			mState = Mouse.GetState();

			#region Game States FSM - Update() portion
			switch (gameState)
			{
				case (GameStates.StartScreen):
					{
						// "Deactivate" the player.
						playerState = PlayerStates.Idle;

						startScreen.StartButton.Update(mState, prevMState);
						startScreen.QuitButton.Update(mState, prevMState);

						// Giving the buttons on the start screen their functionality
						if (startScreen.StartButton.IsClicked)
						{
							gameState = GameStates.Pregame;
						}

						if (startScreen.QuitButton.IsClicked)
						{
							Exit();
						}

						break;
					}
				case (GameStates.Pregame):
					{
						// Perform necessary data resets.
						wallManager.Reset(wall);
						player.Reset();
						scoreCounter.Reset();
						levelCounter.Reset();
						
						// Pressing any key starts the game.
						// GetPressedKeys() returns an array of all keys that
						//		are currently being pressed. By checking the 
						//		array's length, we can effectively see if any 
						//		key was pressed at all in the previous frame.
						if (kbState.GetPressedKeys().Length > 0)
						{
							gameState = GameStates.Playing;
						}

						break;
					}
				case (GameStates.Playing):
					{
						// "Activate" the player.
						playerState = PlayerStates.Active;

						break;
					}
				case (GameStates.GameOver):
					{
						playerState = PlayerStates.Idle;

						gameOverScreen.returnToStartScreen.Update(mState, prevMState);

						// Pressing Enter restarts the game.
						if (kbState.IsKeyDown(Keys.Enter))
						{
							// Perform necessary data resets.
							wallManager.Reset(wall);
							player.Reset();
							scoreCounter.Reset();
							levelCounter.Reset();

							gameState = GameStates.Playing;
						}

						// Giving the button on the game over screen its 
						//		functionality
						if (gameOverScreen.returnToStartScreen.IsClicked)
						{
							gameState = GameStates.StartScreen;
						}

						break;
					}
			}
			#endregion

			#region Player States FSM
			switch (playerState)
			{
				case (PlayerStates.Idle):
					{
						// Do nothing - player character is idle.
						break;
					}
				case (PlayerStates.Active):
					{
						// Player is now active and has access to movement.
						if (kbState.IsKeyDown(Keys.Left))
						{
							player.Position -= player.StrafeVelocity;
							player.UpdateTracker();
						}

						if (kbState.IsKeyDown(Keys.Right))
						{
							player.Position += player.StrafeVelocity;
							player.UpdateTracker();
						}

						//// Continuous scrolling
						//wallManager.Scroll();

						// Controlled scrolling
						if (kbState.IsKeyDown(Keys.Up))
						{
							wallManager.Scroll();
						}
						//wallManager.Scroll();

						// Why is Update() called before SpawnWallPair() and 
						//		DespawnWallPair(), and not after?
						// HINT: Variables initialised to 0 before Update()
						wallManager.Update(gameTime);

						// Manage memory by spawning and despawning walls as necessary.
						wallManager.SpawnWallPair(wall);
						wallManager.DespawnWallPair();
						
						// Update the player's score, level, and strafe speed.
						scoreCounter.Update(gameTime, levelCounter.Value);
						levelCounter.LevelUp(gameTime, wallManager, player);

						// Touching a wall results in a game over.
						foreach (Wall wall in wallManager.LeftWalls)
						{
							if (player.Collided(wall))
							{
								gameState = GameStates.GameOver;
							}
						}

						foreach (Wall wall in wallManager.RightWalls)
						{
							if (player.Collided(wall))
							{
								gameState = GameStates.GameOver;
							}
						}

						break;
					}
			}
			#endregion

			#region Movement debugging code
			//if (kbState.IsKeyDown(Keys.Left))
			//{
			//	player.Position -= player.StrafeVelocity;
			//	player.UpdateTracker();
			//}
			//if (kbState.IsKeyDown(Keys.Right))
			//{
			//	player.Position += player.StrafeVelocity;
			//	player.UpdateTracker();
			//}

			//foreach (Wall wall in wallManager.LeftWalls)
			//{
			//	if (player.Collided(wall))
			//	{
			//		System.Console.WriteLine("LEFT WALL COLLISION");
			//	}
			//}

			//foreach (Wall wall in wallManager.RightWalls)
			//{
			//	if (player.Collided(wall))
			//	{
			//		System.Console.WriteLine("RIGHT WALL COLLISION");
			//	}
			//}

			//// Controlled scrolling
			//if (kbState.IsKeyDown(Keys.Up))
			//{
			//	wallManager.Scroll();
			//}
			////wallManager.Scroll();

			//// Manage memory by spawning and despawning walls as necessary.
			//wallManager.SpawnWallPair(wall);
			//wallManager.DespawnWallPair();
			#endregion

			prevMState = mState;

			base.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			// TODO: Add your drawing code here

			spriteBatch.Begin();

			#region Game States FSM - Draw() portion
			switch (gameState)
			{
				case (GameStates.StartScreen):
					{
						startScreen.Draw(spriteBatch);
						break;
					}
				case (GameStates.Pregame):
					{
						// Draw a static image of the game's initial state first...
						wallManager.DrawAll(spriteBatch);
						player.Draw(spriteBatch);
						scoreCounter.Draw(spriteBatch);
						levelCounter.Draw(spriteBatch);

						// ...then overlay the pregame screen on top.
						pregameScreen.Draw(spriteBatch);
						break;
					}
				case (GameStates.Playing):
					{
						wallManager.DrawAll(spriteBatch);
						player.Draw(spriteBatch);
						scoreCounter.Draw(spriteBatch);
						levelCounter.Draw(spriteBatch);

						break;
					}
				case (GameStates.GameOver):
					{
						// Draw a static image of the game's current state first...
						wallManager.DrawAll(spriteBatch);
						player.Draw(spriteBatch);
						scoreCounter.Draw(spriteBatch);
						levelCounter.Draw(spriteBatch);

						// ...then overlay the game over screen on top.
						gameOverScreen.Draw(spriteBatch);
						gameOverScreen.Draw(spriteBatch, scoreCounter, levelCounter);
						break;
					}
			}
			#endregion
					
			spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
