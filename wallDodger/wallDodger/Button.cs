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
		private const int buttonWidth = 150;
		private const int buttonHeight = 50;
		private Texture2D buttonTexture;
		private SpriteFont verdana12;
		private Vector2 buttonPosition;
		private Vector2 textPosition;
		public MouseState MState { get; }
		public Rectangle ButtonRectangle { get; }
		public Color DefaultButtonColour { get; }
		public Color HoverButtonColour { get; }
		public Color ClickButtonColour { get; }
		public Color CurrentButtonColour { get; set; }
		
		public Button(
			Texture2D buttonTexture, 
			SpriteFont verdana12, 
			int buttonXPos, 
			int buttonYPos,
			int buttonTextXPos,
			int buttonTextYPos,
			MouseState MState)
		{
			this.buttonTexture = buttonTexture;
			this.verdana12 = verdana12;
			DefaultButtonColour = Color.Navy;
			HoverButtonColour = Color.DarkBlue;
			ClickButtonColour = Color.DarkGray;
			CurrentButtonColour = DefaultButtonColour;
			buttonPosition = new Vector2(buttonXPos, buttonYPos);
			textPosition = new Vector2(buttonTextXPos, buttonTextYPos);
			ButtonRectangle = new Rectangle((int)buttonPosition.X, (int)buttonPosition.Y, buttonWidth, buttonHeight);
			this.MState = MState;
		}

		/// <summary>
		/// Updates the button's colour based on the cursor's position.
		/// </summary>
		/// <param name="gameTime">
		/// Provides a snapshot of timing values.
		/// </param>
		public void Update(GameTime gameTime)
		{
			// Button colour FSM
			// Uses if-statements because multiple conditions are checked for.
			if (ButtonRectangle.Contains(MState.Position))
			{
				CurrentButtonColour = HoverButtonColour;
			}
			else if (ButtonRectangle.Contains(MState.Position)
				&& MState.LeftButton == ButtonState.Pressed)
			{
				CurrentButtonColour = ClickButtonColour;
			}
			else
			{
				CurrentButtonColour = DefaultButtonColour;
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
