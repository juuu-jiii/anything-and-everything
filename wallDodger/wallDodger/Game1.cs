using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace wallDodger
{

	// Delegate for methods that are called when a new game is started.
	delegate void OnResetDelegate();

	enum GameStates
	{
		StartScreen,
		Leaderboard,
		Pregame,
		Playing,
		HiScore,
		GameOver
	}

	enum PlayerStates
	{
		Idle,
		Active
	}

	enum ScrollingStates
	{
		NotScrolling,
		IdleScrolling,
		Scrolling
	}
	
	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	public class Game1 : Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

		// Create an event to handle methods matching the delegate's signatures.
		event OnResetDelegate ResetAction;

		// Variables for the window width and height
		// Public constants are globally accessible within the solution.
		public const int WindowHeight = 650;
		public const int WindowWidth = 500;

		// Variables to keep track of player, game, and scrolling state
		GameStates gameState = GameStates.StartScreen;
		PlayerStates playerState = PlayerStates.Idle;
		ScrollingStates scrollingState = ScrollingStates.IdleScrolling; // Game idly scrolls on startup screen.

		// Variables to store texture and font assets
		Texture2D wall;
		Texture2D playerArrow;
		Texture2D whiteSquare;
		SpriteFont verdanaBold20;
		SpriteFont verdanaBold16;
		SpriteFont verdana12;
		SpriteFont verdanaSmall;

		// Variables to store instances of classes created
		WallManager wallManager;
		Player player;
		StartScreen startScreen;
		PregameScreen pregameScreen;
		HiScoreNameEntryScreen hiScoreNameEntryScreen;
		GameOverScreen gameOverScreen;
		LeaderboardScreen leaderboardScreen;
		ScoreCounter scoreCounter;
		LevelCounter levelCounter;
		Leaderboard leaderboard;
		
		// Variables to track mouse and keyboard input
		KeyboardState kbState;
		KeyboardState prevKBState;
		MouseState mState;
		MouseState prevMState;

		// Variable to track a high score's location in the leaderboard array
		int leaderboardPosition;

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
			verdanaBold16 = Content.Load<SpriteFont>("verdanaBold16");
			verdana12 = Content.Load<SpriteFont>("verdana12");
			verdanaSmall = Content.Load<SpriteFont>("verdanaSmall");

			player = new Player(playerArrow);
			startScreen = new StartScreen(verdanaBold20, verdana12, verdanaSmall, whiteSquare, whiteSquare);
			pregameScreen = new PregameScreen(verdana12, wall);
			hiScoreNameEntryScreen = new HiScoreNameEntryScreen(verdanaBold16, verdana12, whiteSquare, whiteSquare, whiteSquare);
			gameOverScreen = new GameOverScreen(verdanaBold20, verdana12, wall, whiteSquare);
			leaderboardScreen = new LeaderboardScreen(verdanaBold20, verdanaBold16, verdana12, whiteSquare, whiteSquare);
			scoreCounter = new ScoreCounter(verdana12);
			levelCounter = new LevelCounter(verdana12);
			leaderboard = new Leaderboard();

			// Subscribing applicable methods to both event handlers
			levelCounter.LevelUpAction += player.LevelUp;
			levelCounter.LevelUpAction += wallManager.LevelUp;
			ResetAction += levelCounter.Reset;
			ResetAction += scoreCounter.Reset;
			ResetAction += player.Reset;

			// Initialise leaderboard array.
			leaderboard.LoadScores();

			// Initialise the start screen's "live" background.
			// This must be done here to avoid an exception being thrown, 
			//		because the start screen is loaded upon startup.
			wallManager.ResetMenu(wall);
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

						// Use idle scrolling.
						scrollingState = ScrollingStates.IdleScrolling;

						startScreen.StartButton.Update(mState, prevMState);
						startScreen.LeaderboardButton.Update(mState, prevMState);
						startScreen.QuitButton.Update(mState, prevMState);

						// Giving the buttons on the start screen their functionality
						if (startScreen.StartButton.IsClicked)
						{
							// Perform necessary data resets here, instead of on the 
							//		pregame screen itself so the code only runs once.
							if (ResetAction != null)
							{
								ResetAction();
							}

							// Initialise the map for the current run.
							// This Reset() method's signature does not match that 
							//		of the event handler's
							wallManager.ResetGame(wall);

							gameState = GameStates.Pregame;
						}

						if (startScreen.LeaderboardButton.IsClicked)
						{
							gameState = GameStates.Leaderboard;
						}

						if (startScreen.QuitButton.IsClicked)
						{
							Exit();
						}

						break;
					}
				case (GameStates.Leaderboard):
					{
						// Should not need to do anything other than check for 
						//		button press, since the start screen must be
						//		accessed prior to the leaderboard.
						leaderboardScreen.BackButton.Update(mState, prevMState);

						// Giving the back button its functionality
						if (leaderboardScreen.BackButton.IsClicked)
						{
							gameState = GameStates.StartScreen;
						}

						break;
					}
				case (GameStates.Pregame):
					{
						// Do not scroll.
						scrollingState = ScrollingStates.NotScrolling;

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

						// Scroll normally.
						scrollingState = ScrollingStates.Scrolling;

						break;
					}
				case (GameStates.HiScore):
					{
						// "Deactivate" the player.
						playerState = PlayerStates.Idle;

						// Do not scroll.
						scrollingState = ScrollingStates.NotScrolling;

						hiScoreNameEntryScreen.Submit.Update(mState, prevMState);
						hiScoreNameEntryScreen.Update(kbState, prevKBState);

						// Either clicking the Submit button or hitting enter saves scores 
						//		and changes the game's state.
						if (hiScoreNameEntryScreen.Submit.IsClicked
							|| kbState.IsKeyDown(Keys.Enter))
						{
							// Update and save leaderboard data.
							leaderboard.UpdateScores(
								leaderboardPosition,
								hiScoreNameEntryScreen.LiveString,
								scoreCounter.Value);
							leaderboard.SaveScores();
							gameState = GameStates.GameOver;
						}

						break;
					}
				case (GameStates.GameOver):
					{
						// Explicitly deactivate the player and stop scrolling here too, in 
						//		case no new high score was set.
						playerState = PlayerStates.Idle;
						scrollingState = ScrollingStates.NotScrolling;

						gameOverScreen.ReturnToStartScreen.Update(mState, prevMState);

						// Pressing Enter restarts the game.
						// Check for single key press so Enter input from high score screen 
						//		does not interfere with current.
						if (kbState.IsKeyDown(Keys.Enter)
							&& prevKBState.IsKeyUp(Keys.Enter))
						{
							// Perform necessary data resets.
							if (ResetAction != null)
							{
								ResetAction();
							}

							// This Reset() method's signature does not match that 
							//		of the event handler's.
							wallManager.ResetGame(wall);

							gameState = GameStates.Playing;
						}

						// Giving the button on the game over screen its 
						//		functionality
						if (gameOverScreen.ReturnToStartScreen.IsClicked)
						{
							// Reset and initialise the map to prepare for idle 
							//		scrolling on the start screen.
							wallManager.ResetMenu(wall);

							gameState = GameStates.StartScreen;
						}

						break;
					}
			}
			#endregion

			#region Player States FSM
			// This FSM tracks all player-related functions: movement and
			//		collision detection.
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

						// Touching a wall results in a game over.
						foreach (Wall wall in wallManager.LeftWalls)
						{
							if (player.Collided(wall))
							{
								// Generate a leaderboard position, if applicable.
								leaderboardPosition = leaderboard.IsHighScore(scoreCounter.Value);
								
								// Check whether the score earned this run is a high score.
								// If it is not, proceed directly to the game over screen.
								if (leaderboardPosition == -1)
								{
									gameState = GameStates.GameOver;
								}
								// Display the high score screen if the player earns a
								//		score that alters the leaderboard.
								else
								{
									gameState = GameStates.HiScore;
								}
							}
						}

						foreach (Wall wall in wallManager.RightWalls)
						{
							if (player.Collided(wall))
							{
								// Generate a leaderboard position, if applicable.
								leaderboardPosition = leaderboard.IsHighScore(scoreCounter.Value);

								// Check whether the score earned this run is a high score.
								// If it is not, proceed directly to the game over screen.
								if (leaderboardPosition == -1)
								{
									gameState = GameStates.GameOver;
								}
								// Display the high score screen if the player earns a
								//		score that alters the leaderboard.
								else
								{
									gameState = GameStates.HiScore;
								}
							}
						}

						break;
					}
			}
			#endregion

			#region Scrolling States FSM
			// This FSM tracks and controls all scrolling possibilities for
			//		the map.
			switch (scrollingState)
			{
				// Idle scrolling is used on the start and leaderboard screens.
				case (ScrollingStates.IdleScrolling):
					{
						// Continuous scrolling
						wallManager.Scroll();

						// Why is Update() called before SpawnWallPair() and 
						//		DespawnWallPair(), and not after?
						// HINT: Variables initialised to 0 before Update()
						wallManager.UpdateMenu(gameTime);

						// Manage memory by spawning and despawning walls as necessary.
						wallManager.SpawnWallPair(wall);
						wallManager.DespawnWallPair();

						break;
					}
				// The game does not scroll during the pregame, high score entry, and game over screens.
				case (ScrollingStates.NotScrolling):
					{
						// Do nothing - no scrolling takes place.

						break;
					}
				// Regular scrolling is used during actual gameplay.
				case (ScrollingStates.Scrolling):
					{
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
						wallManager.UpdateGame(gameTime);

						// Manage memory by spawning and despawning walls as necessary.
						wallManager.SpawnWallPair(wall);
						wallManager.DespawnWallPair();

						// Update the player's score, level, and strafe speed.
						scoreCounter.Update(gameTime, levelCounter.Value);

						// This method invokes the event, calling all the other
						//		level up methods along with it.
						levelCounter.LevelUp(gameTime);

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

			prevKBState = kbState;
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
						// Draw a "live" background of the game.
						wallManager.DrawAll(spriteBatch);

						// Overlay the start screen's components above the live background.
						startScreen.Draw(spriteBatch);
						break;
					}
				case (GameStates.Leaderboard):
					{
						// Draw a "live" background of the game.
						wallManager.DrawAll(spriteBatch);

						// Overlay the leaderboard screen's components above the live background.
						leaderboardScreen.Draw(spriteBatch, leaderboard.HiScores);
						break;
					}
				case (GameStates.Pregame):
					{
						// Draw a static image of the game's initial state.
						wallManager.DrawAll(spriteBatch);
						player.Draw(spriteBatch);
						scoreCounter.Draw(spriteBatch);
						levelCounter.Draw(spriteBatch);
						
						// Overlay the pregame screen on top.
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
				case (GameStates.HiScore):
					{
						// Draw a static image of the game's current state.
						wallManager.DrawAll(spriteBatch);
						player.Draw(spriteBatch);
						scoreCounter.Draw(spriteBatch);
						levelCounter.Draw(spriteBatch);

						// Overlay the high score screen above the current game state.
						hiScoreNameEntryScreen.Draw(spriteBatch);

						break;
					}
				case (GameStates.GameOver):
					{
						// Draw a static image of the game's current state.
						wallManager.DrawAll(spriteBatch);
						player.Draw(spriteBatch);
						scoreCounter.Draw(spriteBatch);
						levelCounter.Draw(spriteBatch);

						// Overlay the game over screen on top.
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
