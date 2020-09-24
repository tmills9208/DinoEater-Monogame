/* AboutState.cs
 *  Displays name of creator, along with credits for artowkr and sounds used
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
    public class AboutState : GameState
    {
        private SpriteFont font;
        private Color regularColor;

        private Vector2 position;
        private KeyboardState oldState;
        
        public AboutState(Game game) : base(game)
        {
            font = parent.font;
            regularColor = Color.White;

            position = new Vector2(
                100,
                200
            );
            oldState = Keyboard.GetState();
        }
        /// <summary>
        /// Checks if the user wants to go back to the main menu
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();
            if (ks.IsKeyDown(Keys.Escape) && oldState.IsKeyUp(Keys.Escape))
            {
                parent.Notify(this, "");
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
            parent.Sprite.DrawString(font, "Made by: Tyler Mills", tempPos, regularColor);
            tempPos.Y += font.LineSpacing;
            parent.Sprite.DrawString(font, "Dinos: by Arks on Itch.io", tempPos, regularColor);
            tempPos.Y += font.LineSpacing;
            parent.Sprite.DrawString(font, "Music: Jungle Troubles by Runescape", tempPos, regularColor);
            tempPos.Y += font.LineSpacing;
            parent.Sprite.DrawString(font, "Dino sound: Yoshi lick", tempPos, regularColor);
            parent.Sprite.End();
            base.Draw(gameTime);
        }
    }
}
