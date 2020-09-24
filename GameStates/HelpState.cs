/* HelpState.cs
 *  Displays controls user can use to interact with the game, along with how to play
 * 
 * Revision History
 *      Tyler Mills, 2019.12.08: Created
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DinoEater
{
    public class HelpState : GameState
    {
        private SpriteFont largeFont;
        private SpriteFont font;
        private Color regularColor;

        private Vector2 position;
        private KeyboardState oldState;
        
        public HelpState(Game game) : base(game)
        {
            largeFont = parent.largeFont;
            font = parent.font;
            regularColor = Color.White;

            position = new Vector2(
                parent.Graphics.PreferredBackBufferWidth / 2 - 200,
                parent.Graphics.PreferredBackBufferHeight / 2 - 200
            );
            oldState = Keyboard.GetState();
        }
        /// <summary>
        /// Checks if the user wants to return to the main menu
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();
            if (ks.IsKeyDown(Keys.Escape) && oldState.IsKeyUp(Keys.Escape))
            {
                parent.Notify(this, "Main Menu");
            }
            oldState = ks;

            base.Update(gameTime);
        }
        /// <summary>
        /// Draws the text
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            Vector2 tempPos = position;
            parent.Sprite.Begin();
            parent.Sprite.DrawString(largeFont, "Help", tempPos, regularColor);
            tempPos.Y += largeFont.LineSpacing;
            parent.Sprite.DrawString(font, "W A S D to move your dino", tempPos, regularColor);
            tempPos.Y += font.LineSpacing;
            parent.Sprite.DrawString(font, "Esc to back out to the main menu", tempPos, regularColor);
            tempPos.Y += font.LineSpacing;
            parent.Sprite.DrawString(font, "Move around to eat smaller dinos\nwhile avoiding larger ones", tempPos, regularColor);
            parent.Sprite.End();
            base.Draw(gameTime);
        }
    }
}
