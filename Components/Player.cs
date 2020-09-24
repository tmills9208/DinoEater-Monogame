/* Player.cs
 * A dino that is controlled by the character
 * 
 * Revision History
 *      Tyler Mills, 2019.12.08: Created
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DinoEater
{
    class Player : Dino
    {
        Game1 parent;
        KeyboardState oldState;

        public Player(Game game, string textureLocation) : base(game, textureLocation)
        {
            parent = (Game1)game;
            Position = new Vector2(
                parent.Graphics.PreferredBackBufferWidth / 2 - Size / 2,
                parent.Graphics.PreferredBackBufferWidth / 2 - Size / 2
            );
        }
        /// <summary>
        /// Gets player input and moves the player based on received information
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            Vector2 oldPosition = Position;
            KeyboardState ks = Keyboard.GetState();

            // Handle movement
            if (ks.IsKeyDown(Keys.A))
            {
                this.Position = new Vector2(Position.X - this.speed, Position.Y);
            }
            else if (ks.IsKeyDown(Keys.D))
            {
                Position = new Vector2(Position.X + this.speed, Position.Y);
            }
            if (ks.IsKeyDown(Keys.W))
            {
                Position = new Vector2(Position.X, Position.Y - this.speed);
            }
            else if (ks.IsKeyDown(Keys.S))
            {
                Position = new Vector2(Position.X, Position.Y + this.speed);
            }

            base.Update(gameTime);
        }
    }
}
