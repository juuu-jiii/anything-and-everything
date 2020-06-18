using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input; // Key

namespace wallDodger
{
	class HiScoreNameEntryScreen : MenuScreen
	{
		// TEXT INPUT TESTER VARIABLES

		private Color backdropColour;

		// The string that is displayed to the user.
		public string LiveString { get; private set; }

		// An array to hold keys pressed during the current frame.
		private Keys[] LiveStringArray;

		// List to restrict keys that may be used during name input.
		private List<Keys> permittedKeys;

		// Button to submit the player's typed initials.
		public Button Submit { get; }

		private Texture2D textField;

		public HiScoreNameEntryScreen(
			SpriteFont verdanaBold16,
			SpriteFont verdana12,
			Texture2D backdrop,
			Texture2D buttonTexture,
			Texture2D textField) : base(
				backdrop,
				new Vector2(153, 240),
				new Vector2(158, 275),
				verdanaBold16,
				verdana12)
		{
			// Create a custom colour using only the alpha channel for 
			//		translucency, so this screen can be used as an overlay.
			backdropColour = Color.FromNonPremultiplied(0, 0, 0, 90);

			// Initialise LiveString to an empty string to avoid a NullReferenceException 
			//		when it is accessed later on.
			LiveString = "";

			permittedKeys = new List<Keys> {
				Keys.Q,
				Keys.W,
				Keys.E,
				Keys.R,
				Keys.T,
				Keys.Y,
				Keys.U,
				Keys.I,
				Keys.O,
				Keys.P,
				Keys.A,
				Keys.S,
				Keys.D,
				Keys.F,
				Keys.G,
				Keys.H,
				Keys.J,
				Keys.K,
				Keys.L,
				Keys.Z,
				Keys.X,
				Keys.C,
				Keys.V,
				Keys.B,
				Keys.N,
				Keys.M,
				Keys.Space,
				Keys.Back
			};

			Submit = new Button(buttonTexture, verdana12, 175, 355, 212, 368);

			this.textField = textField;
		}

		public void Update(KeyboardState kbState, KeyboardState prevKBState)
		{
			// Assign the returned array of pressed keys for that frame to a variable.
			LiveStringArray = kbState.GetPressedKeys();



			//Need:
			//draw method.Look at Game1s draw fsm for things to copy paste.
			//to draw a pop up window first.Pop up because it is smaller than the rest of the window, so it looks
			//like it has been overlaid.Need to also determine when to show the pop up.
			//Button to submit the name.Also add functionality for enter key possibly.

			// TEXT INPUT TESTER CODE





			// For all intents and purposes, the following code works sufficiently well.
			// Mixing GetPressedKeys() with IsKeyUp/Down()
			// Loop through GetPressedKeys()'s returned array to see if the keys are pressed
			// If they are, do not add them to LiveStringArray - only add those that are not pressed.

			// Loop through the above returned array.
			for (int i = 0; i < LiveStringArray.Length; i++)
			{
				// Detect single presses of keys in the array. Only proceed if the current key
				//		being checked is in permittedKeys AND was up in the previous frame. 
				//		Otherwise skip and check the next key in the array. This effectively 
				//		allows for regular typing, save for the character repeat aspect. This  
				//		is because a few keys are pressed simultaneously while typing normally,  
				//		and this solution accommodates such a scenario.
				if (permittedKeys.Contains(LiveStringArray[i])
					&& kbState.IsKeyDown(LiveStringArray[i])
					&& prevKBState.IsKeyUp(LiveStringArray[i]))
				{
					// If Backspace is pressed...
					if (LiveStringArray[i] == Keys.Back)
					{
						// ...and if the string is empty, do nothing.
						if (LiveString.Length == 0)
						{
							// Do nothing.
							// This prevents an index out of range error from occurring.
						}
						// ...otherwise, add characters up to a max length of 3.
						else if (LiveString.Length > 0
							|| LiveString.Length <= 3)
						{
							// Strings are immutable, and so must be reassigned should they be modified,
							//		similarly with how structs behave.
							LiveString = LiveString.Remove(LiveString.Length - 1);
						}
					}
					// If Space is pressed...
					else if (LiveStringArray[i] == Keys.Space)
					{
						// ...add space up to a max length of 3.
						if (LiveString.Length < 3)
						{
							LiveString += " ";
						}
					}
					// For all other Keys, concatenate them in the order they were pressed i.e. the
					//		order in which they appear in LiveStringArray up to a max length of 3.
					else
					{
						if (LiveString.Length < 3)
						{
							LiveString += LiveStringArray[i];
						}
					}
				}
			}
		}
						
		public override void Draw(SpriteBatch spriteBatch)
		{
			// Draw the backdrop.
			spriteBatch.Draw(
				backdrop,
				new Rectangle(0, 0, Game1.WindowWidth, Game1.WindowHeight),
				backdropColour);
			
			// Draw the pop-up window.
			spriteBatch.Draw(
				backdrop,
				new Rectangle(
					Game1.WindowWidth / 2 - 125, 
					Game1.WindowHeight / 2 - 100, 
					250, 
					200),
				Color.LightGray);

			spriteBatch.DrawString(
				textFont,
				"NEW HIGH SCORE",
				textPosition,
				Color.Black);

			// Draw the instructional text.
			spriteBatch.DrawString(
				subtextFont,
				"ENTER YOUR INITIALS",
				subtextPosition,
				Color.Black);

			// Draw the text field.
			spriteBatch.Draw(
				textField,
				new Rectangle(
					Game1.WindowWidth / 2 - 25, 
					Game1.WindowHeight / 2 - 15, 
					50, 
					26),
				Color.White);

			// Draw the Submit button.
			Submit.Draw(
				spriteBatch,
				"Continue");

			// Draw the player's typed initials.
			spriteBatch.DrawString(
				subtextFont,
				LiveString,
				new Vector2(
					Game1.WindowWidth / 2 - 15,
					Game1.WindowHeight / 2 - 12),
				Color.Black);
		}
	}
}
