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

		// The string that is displayed to the user.
		public string liveString { get; set; }

		// An array to hold keys pressed during the current frame.
		private Keys[] liveStringArray;

		// List to restrict keys that may be used during name input.
		private List<Keys> permittedKeys;

		// Button to submit the player's typed initials.
		public Button Submit { get; }

		private Texture2D textField;

		public HiScoreNameEntryScreen(
			SpriteFont verdana12,
			Texture2D backdrop,
			Texture2D buttonTexture,
			Texture2D textField) : base(
				backdrop,
				new Vector2(Game1.WindowWidth / 2, Game1.WindowHeight / 2),
				new Vector2(Game1.WindowWidth / 2, Game1.WindowHeight / 2 + 35),
				verdana12,
				null)
		{
			// Initialise liveString to an empty string to avoid a NullReferenceException 
			//		when it is accessed later on.
			liveString = "";

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

			Submit = new Button(buttonTexture, verdana12, 250, 250, 260, 263);

			this.textField = textField;
		}

		public void Update(KeyboardState kbState, KeyboardState prevKBState)
		{
			// Assign the returned array of pressed keys for that frame to a variable.
			liveStringArray = kbState.GetPressedKeys();



			//Need:
			//draw method.Look at Game1s draw fsm for things to copy paste.
			//to draw a pop up window first.Pop up because it is smaller than the rest of the window, so it looks
			//like it has been overlaid.Need to also determine when to show the pop up.
			//Button to submit the name.Also add functionality for enter key possibly.

			// TEXT INPUT TESTER CODE





			// For all intents and purposes, the following code works sufficiently well.
			// Mixing GetPressedKeys() with IsKeyUp/Down()
			// Loop through GetPressedKeys()'s returned array to see if the keys are pressed
			// If they are, do not add them to liveStringArray - only add those that are not pressed.

			// Loop through the above returned array.
			for (int i = 0; i < liveStringArray.Length; i++)
			{
				// Detect single presses of keys in the array. Only proceed if the current key
				//		being checked is in permittedKeys AND was up in the previous frame. 
				//		Otherwise skip and check the next key in the array. This effectively 
				//		allows for regular typing, save for the character repeat aspect. This  
				//		is because a few keys are pressed simultaneously while typing normally,  
				//		and this solution accommodates such a scenario.
				if (permittedKeys.Contains(liveStringArray[i])
					&& kbState.IsKeyDown(liveStringArray[i])
					&& prevKBState.IsKeyUp(liveStringArray[i]))
				{
					// If Backspace is pressed...
					if (liveStringArray[i] == Keys.Back)
					{
						// ...and if the string is empty, do nothing.
						if (liveString.Length == 0)
						{
							// Do nothing.
							// This prevents an index out of range error from occurring.
						}
						// ...otherwise, add characters up to a max length of 3.
						else if (liveString.Length > 0
							|| liveString.Length <= 3)
						{
							// Strings are immutable, and so must be reassigned should they be modified,
							//		similarly with how structs behave.
							liveString = liveString.Remove(liveString.Length - 1);
						}
					}
					// If Space is pressed...
					else if (liveStringArray[i] == Keys.Space)
					{
						// ...add space up to a max length of 3.
						if (liveString.Length < 3)
						{
							liveString += " ";
						}
					}
					// For all other Keys, concatenate them in the order they were pressed i.e. the
					//		order in which they appear in liveStringArray up to a max length of 3.
					else
					{
						if (liveString.Length < 3)
						{
							liveString += liveStringArray[i];
						}
					}
				}
			}
		}
						
		public override void Draw(SpriteBatch spriteBatch)
		{
			// Draw the pop-up window.
			spriteBatch.Draw(
				backdrop,
				new Rectangle(100, 100, 200, 100),
				Color.Gray);

			// Draw the instructional text.
			spriteBatch.DrawString(
				textFont,
				"ENTER YOUR INITIALS",
				new Vector2(130, 115),
				Color.Black);

			// Draw the text field.
			spriteBatch.Draw(
				textField,
				new Rectangle(Game1.WindowWidth / 2, 200, 50, 15),
				Color.White);

			// Draw the Submit button.
			Submit.Draw(
				spriteBatch,
				"Continue");

			// Draw the player's typed initials.
			spriteBatch.DrawString(
				textFont,
				liveString,
				new Vector2(Game1.WindowWidth / 2 + 10, 210),
				Color.Black);
		}
	}
}
