/* ActionState.cs
 * The main menus is displayed and updated here
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
    class MenuState : GameState
    {
        private SpriteFont largeFont;
        private SpriteFont font;
        private Color regularColor;
        private Color highlightColor;

        private List<string> menuItems;
        public int selectedIndex;

        private Vector2 position;
        private KeyboardState oldState;
        public MenuState(Game game, List<string> menuItems) : base(game)
        {
            largeFont = parent.largeFont;
            font = parent.font;

            regularColor = Color.Black;
            highlightColor = Color.White;

            this.menuItems = menuItems;
            position = new Vector2(
                150,
                100
            );
        }
        /// <summary>
        /// Gets user input and acts upon it
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();
            if (oldState.IsKeyUp(Keys.Down) && ks.IsKeyDown(Keys.Down))
            {
                selectedIndex = MathHelper.Clamp(selectedIndex + 1, 0, menuItems.Count - 1);
            }
            if (oldState.IsKeyUp(Keys.Up) && ks.IsKeyDown(Keys.Up))
            {
                selectedIndex = MathHelper.Clamp(selectedIndex - 1, 0, menuItems.Count - 1);
            }
            if (oldState.IsKeyUp(Keys.Enter) && ks.IsKeyDown(Keys.Enter))
            {
                parent.Notify(this, menuItems[selectedIndex]);
            }
            oldState = ks;
            base.Update(gameTime);
        }
        /// <summary>
        /// Draws the text and highlighted text, based on what the user currently has selected
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            Vector2 tempPos = position;
            Vector2 hiscorePos = new Vector2(
                350, 5 + font.LineSpacing
            );
            parent.Sprite.Begin();
            parent.Sprite.DrawString(font, "High Score: " + parent.hiscore, hiscorePos, highlightColor);
            parent.Sprite.DrawString(largeFont, "Dino Eater", tempPos, highlightColor);
            tempPos.Y += largeFont.LineSpacing;
            for (int i = 0; i < menuItems.Count; i++)
            {
                if (selectedIndex == i)
                {
                    parent.Sprite.DrawString(font, menuItems[i], tempPos, highlightColor);
                    tempPos.Y += font.LineSpacing;
                }
                else
                {
                    parent.Sprite.DrawString(font, menuItems[i], tempPos, regularColor);
                    tempPos.Y += font.LineSpacing;
                }
            }
            parent.Sprite.End();
            base.Draw(gameTime);
        }
    }
}
