using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

// Defines a menu screen displayed to the user during program runtime

namespace wallDodger
{
	abstract class MenuScreen
	{
		// Variables to store texture and font assets
		protected Texture2D backdrop;
		protected SpriteFont textFont;
		protected SpriteFont subtextFont;

		// Variables to store text position
		protected Vector2 textPosition;
		protected Vector2 subtextPosition;		

		public MenuScreen(
			Texture2D backdrop, 
			Vector2 textPosition, 
			Vector2 subtextPosition, 
			SpriteFont textFont, 
			SpriteFont subtextFont)
		{
			this.backdrop = backdrop;
			this.textPosition = textPosition;
			this.subtextPosition = subtextPosition;
			this.textFont = textFont;
			this.subtextFont = subtextFont;
		}

		public abstract void Draw(SpriteBatch spriteBatch);
	}
}
