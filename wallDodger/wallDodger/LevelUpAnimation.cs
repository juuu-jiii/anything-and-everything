using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace wallDodger
{
	class LevelUpAnimation
	{
		// bool flag tracking whether the player has leveled up
		public bool HasLeveledUp { get; set; }
		
		// Vector tracking text position relative to the screen
		public Vector2 TextPosition { get; private set; }
		
		// Vector controlling velocity at which text scrolls across the screen
		private Vector2 textScrollVelocity;

		private string text;
		private SpriteFont textFont;

		// Array storing all possible background colour tints.
		private Color[] bgColourArray;

		// Variable to track the current background colour, in the form of an 
		//		index corresponding to bgColourArray.
		private int currentColour;

		// Constructor
		public LevelUpAnimation(SpriteFont textFont)
		{
			HasLeveledUp = false;
			TextPosition = new Vector2(-100, 100);
			textScrollVelocity = new Vector2(10, 0);
			text = "LEVEL UP";
			this.textFont = textFont;
			currentColour = 0;

			bgColourArray = new Color[]
			{
				Color.FromNonPremultiplied(225, 156, 156, 255),
				Color.FromNonPremultiplied(225, 182, 156, 255),
				Color.FromNonPremultiplied(225, 200, 156, 255),
				Color.FromNonPremultiplied(225, 212, 156, 255),
				Color.FromNonPremultiplied(225, 224, 156, 255),
				Color.FromNonPremultiplied(212, 225, 156, 255),
				Color.FromNonPremultiplied(198, 225, 156, 255),
				Color.FromNonPremultiplied(163, 225, 156, 255),
				Color.FromNonPremultiplied(156, 225, 177, 255),
				Color.FromNonPremultiplied(156, 225, 196, 255),
				Color.FromNonPremultiplied(156, 225, 219, 255),
				Color.FromNonPremultiplied(156, 210, 225, 255),
				Color.FromNonPremultiplied(156, 198, 225, 255),
				Color.FromNonPremultiplied(156, 189, 225, 255),
				Color.FromNonPremultiplied(156, 163, 225, 255),
				Color.FromNonPremultiplied(182, 156, 225, 255),
				Color.FromNonPremultiplied(198, 156, 225, 255),
				Color.FromNonPremultiplied(212, 156, 225, 255),
				Color.FromNonPremultiplied(225, 156, 187, 255),
			};
		}

		/// <summary>
		/// Helper method that resets textPosition to its default value. Called
		///		after text has made a complete scroll across the screen.
		/// </summary>
		public void Reset()
		{
			TextPosition = new Vector2(-100, 100);
		}

		/// <summary>
		/// Wipes the text across the screen.
		/// </summary>
		public void LevelUp()
		{
			// If the text has not completed a full wipe of the screen, 
			//		continue the animation.
			if (TextPosition.X <= Game1.WindowWidth)
			{
				TextPosition += textScrollVelocity;
			}
			// Otherwise, alter the bool flag, and reset textPosition.
			else
			{
				HasLeveledUp = false;
				Reset();
			}
		}

		/// <summary>
		/// Changes the map's background colour.
		/// </summary>
		/// <param name="graphicsDevice">
		/// The GraphicsDevice object.
		/// </param>
		public void ChangeBgColour(GraphicsDevice graphicsDevice)
		{
			// Choose the next available colour in bgColourArray.
			// Modulus allows wraparound.
			currentColour = (currentColour + 1) % bgColourArray.Length;
			graphicsDevice.Clear(bgColourArray[currentColour]);
		}
		
		/// <summary>
		/// Draws the level up text to the screen.
		/// </summary>
		/// <param name="spriteBatch">
		/// The SpriteBatch object used to draw with.
		/// </param>
		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.DrawString(
				textFont,
				text,
				TextPosition,
				Color.White);
		}
	}
}
