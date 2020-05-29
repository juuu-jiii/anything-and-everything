using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

// Defines a component during gameplay that tracks and notifies players of 
//		their progress.

namespace wallDodger
{
	abstract class ProgressCounter
	{
		public int Value { get; set; }
		protected SpriteFont textFont;
		protected Vector2 textPosition;
		protected double timePerUnit;
		protected double timeCounter;

		public ProgressCounter(SpriteFont textFont, Vector2 textPosition)
		{
			Value = 0;
			this.textFont = textFont;
			this.textPosition = textPosition;
		}

		public abstract void Draw(SpriteBatch spriteBatch);
		public abstract void Reset();
	}
}
