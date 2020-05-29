using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace wallDodger
{
	class Button
	{
		// Fields

		// Button dimensions
		private const int buttonWidth = 150;
		private const int buttonHeight = 50;

		// A private set allows you to modify the variable's value within the 
		//		class's code, but nowhere else. Without it, the variable cannot
		//		be modified at all after it is initialised.
		public bool IsClicked { get; private set; }
		
		// Button texture and button text font
		private Texture2D buttonTexture;
		private SpriteFont verdana12;

		// Button and button text position
		private Vector2 buttonPosition;
		private Vector2 textPosition;

		// Rectangle object to help with drawing and cursor detection
		public Rectangle ButtonRectangle { get; }

		// Button colours
		public Color DefaultButtonColour { get; }
		public Color HoverButtonColour { get; }
		public Color ClickButtonColour { get; }

		// Variable to track the current button colour
		public Color CurrentButtonColour { get; set; }
		
		public Button(
			Texture2D buttonTexture, 
			SpriteFont verdana12, 
			int buttonXPos, 
			int buttonYPos,
			int buttonTextXPos,
			int buttonTextYPos)
		{
			this.buttonTexture = buttonTexture;
			this.verdana12 = verdana12;
			DefaultButtonColour = Color.Blue;
			HoverButtonColour = Color.DarkBlue;
			ClickButtonColour = Color.DarkGray;
			CurrentButtonColour = DefaultButtonColour;
			buttonPosition = new Vector2(buttonXPos, buttonYPos);
			textPosition = new Vector2(buttonTextXPos, buttonTextYPos);
			ButtonRectangle = new Rectangle((int)buttonPosition.X, (int)buttonPosition.Y, buttonWidth, buttonHeight);
			IsClicked = false;
		}

		/// <summary>
		/// Updates the button's colour based on the cursor's position.
		/// </summary>
		/// <param name="mState">
		/// The MouseState variable whose input is to be queried.
		/// </param>
		/// /// <param name="prevMState">
		/// The previous mouse state. Used to query for single clicks.
		/// </param>
		/// mState used as a parameter because it is not needed elsewhere in the
		///		class. This can then be passed in within Game1, and other
		///		classes which use Buttons then don't need to handle mouse input.
		public void Update(MouseState mState, MouseState prevMState)
		{
			// Button colour FSM
			// Uses if-statements because multiple conditions are checked for.
			// Contains can either lead to hovering or clicking...
			if (ButtonRectangle.Contains(mState.Position))
			{
				// ...so nested ifs are used to address all conditions

				// Click and hold: click colour is shown ONLY.
				if (mState.LeftButton == ButtonState.Pressed)
				{
					CurrentButtonColour = ClickButtonColour;
				}
				// Release after click: button registers as clicked.
				else if (mState.LeftButton == ButtonState.Released
					&& prevMState.LeftButton == ButtonState.Pressed)
				{
					IsClicked = true;
				}
				// If neither of the above take place, then the user is hovering.
				else
				{
					CurrentButtonColour = HoverButtonColour;
					IsClicked = false;
				}

				// Above was written as a single conditional, and not broken up
				//		into multiple ones because events are mutually exclusive.
				// Therefore, only split conditionals into multiple if's when 
				//		events are NOT mututally exclusive i.e. they can occur 
				//		simultaneously.
			}
			// If Contains returns false, the user is neither hovering or 
			//		clicking on the button
			else
			{
				CurrentButtonColour = DefaultButtonColour;
				IsClicked = false;
			}
		}

		/// <summary>
		/// Draws the button object to the screen.
		/// </summary>
		/// <param name="spriteBatch">
		/// The SpriteBatch object used to draw with.
		/// </param>
		/// <param name="buttonText">
		/// The text to be displayed on the button.
		/// </param>
		public void Draw(SpriteBatch spriteBatch, string buttonText)
		{
			spriteBatch.Draw(
				buttonTexture,
				ButtonRectangle,
				CurrentButtonColour);
			spriteBatch.DrawString(
				verdana12,
				buttonText,
				textPosition,
				Color.White);
		}
	}
}
